﻿//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

// <code>
using System.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.CognitiveServices.Speech;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Speech.Synthesis;
namespace BMC.Sarah
{
    class Program
    {
        static bool IsActive { set; get; } = false;
        static List<DeviceData> Devices = DeviceData.GetAllDevices();
        static SpeechSynthesizer synth;
        static MqttService iot = new MqttService();
        public static async Task RecognizeSpeechAsync()
        {
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = SpeechConfig.FromSubscription("6cc36fa4db2a413989da529a8800975f", "southeastasia");
            synth = new SpeechSynthesizer();
            foreach (var v in synth.GetInstalledVoices().Select(v => v.VoiceInfo))
            {
                Console.WriteLine("Name:{0}, Gender:{1}, Age:{2}",
                  v.Description, v.Gender, v.Age);
            }

            // select male senior (if it exists)
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);

            // select audio device
            synth.SetOutputToDefaultAudioDevice();



            // Creates a speech recognizer.
            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Say something...");

                // Starts speech recognition, and returns after a single utterance is recognized. The end of a
                // single utterance is determined by listening for silence at the end or until a maximum of 15
                // seconds of audio is processed.  The task returns the recognition text as result. 
                // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
                // shot recognition like command or query. 
                // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
                recognizer.Recognized += Recognizer_Recognized;
                Console.WriteLine("press any key to stop...");
                synth.Speak("Sarah is ready to serve");

                await recognizer.StartContinuousRecognitionAsync();
                Console.ReadLine();
                synth.Dispose();
                await recognizer.StopKeywordRecognitionAsync();
            }
        }

        private static void Recognizer_Recognized(object sender, SpeechRecognitionEventArgs e)
        {
            var result = e.Result;
            if (e.Result==null || e.Result.Text == null) return;
            // Checks result.
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine($"We recognized: {result.Text}");
                MakeRequest(result.Text);
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
                }
            }
        }

        static async void MakeRequest(string QueryText)
        {
            if (QueryText == null) return;
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // This app ID is for a public sample app that recognizes requests to turn on and turn off lights
            var luisAppId = "36e195ea-3d25-4d11-b509-910d5ee15aa9";
            var endpointKey = "52a0ca570af84ed5a7e770244223d4be";

            // The request header contains your subscription key
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endpointKey);

            // The "q" parameter contains the utterance to send to LUIS
            queryString["q"] = QueryText;

            // These optional request parameters are set to their default values
            queryString["timezoneOffset"] = "0";
            queryString["verbose"] = "false";
            queryString["spellCheck"] = "false";
            queryString["staging"] = "false";

            var endpointUri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?" + queryString;
            var response = await client.GetAsync(endpointUri);

            var strResponseContent = await response.Content.ReadAsStringAsync();
            var luisObject = JsonConvert.DeserializeObject<LuisResult>(strResponseContent);
            Process(luisObject);
            // Display the JSON result from LUIS
            Console.WriteLine(strResponseContent.ToString());
        }

        async static void Process(LuisResult result)
        {
            if (result != null)
            {
                switch (result.topScoringIntent.intent)
                {
                    case "Activate":
                        IsActive = true;
                        synth.Speak($"yes boss");
                        Console.WriteLine("Sarah is Active");
                        break;
                    case "TurnOn":
                        if (IsActive && result.entities != null && result.entities.Length>0)
                        {
                            if (result.entities[0].type == "Device")
                            {
                                var IP = Devices.Where(a => a.Name.ToLower() == result.entities[0].entity).ToList().FirstOrDefault().IP;
                                string URL = $"http://{IP}/cm?cmnd=Power%20On";
                                synth.Speak($"turn on {result.entities[0].entity}");
                                Console.WriteLine("call :"+URL);
                                await iot.InvokeMethod("BMCSecurityBot", "OpenURL", new string[] { URL });
                                IsActive = false;
                            }
                        }
                        break;
                    case "TurnOff":
                        if (IsActive && result.entities != null && result.entities.Length > 0)
                        {
                            if (result.entities[0].type == "Device")
                            {
                                var IP = Devices.Where(a => a.Name.ToLower() == result.entities[0].entity).ToList().FirstOrDefault().IP;
                                string URL = $"http://{IP}/cm?cmnd=Power%20Off";
                                Console.WriteLine("call :" + URL);
                                synth.Speak($"turn off {result.entities[0].entity}");

                                await iot.InvokeMethod("BMCSecurityBot", "OpenURL", new string[] { URL });
                                IsActive = false;

                            }
                        }
                        break;
                    case "Thanks":
                        //synth.Speak($"you're welcome");
                        break;
                }
            }
        }

        static void Main()
        {
            RecognizeSpeechAsync().Wait();
        }
    }

    public class LuisResult
    {
        public string query { get; set; }
        public Topscoringintent topScoringIntent { get; set; }
        public Entity[] entities { get; set; }
    }

    public class Topscoringintent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public Resolution resolution { get; set; }
    }

    public class Resolution
    {
        public string[] values { get; set; }
    }

}
// </code>