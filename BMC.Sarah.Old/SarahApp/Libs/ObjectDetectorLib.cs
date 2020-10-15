using Microsoft.AI.Skills.Vision.ObjectDetectorPreview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;

namespace SarahApp.Libs
{
    public class ObjectDetectorLib
    {
        public ObjectDetectorLib()
        {
            Setup();
        }
        ObjectDetectorSkill skill;
        ObjectDetectorBinding binding;
        async void Setup()
        {
            ObjectDetectorDescriptor descriptor = new ObjectDetectorDescriptor();
            var m_availableExecutionDevices = await descriptor.GetSupportedExecutionDevicesAsync();

            skill = await descriptor.CreateSkillAsync() as ObjectDetectorSkill; // If you don't specify an ISkillExecutionDevice, a default will be automatically selected
            binding = await skill.CreateSkillBindingAsync() as ObjectDetectorBinding;
        }

        public async Task<(int person, int vehicle)> DetectObject(VideoFrame frame)
        {
            await binding.SetInputImageAsync(frame);  // frame is a Windows.Media.VideoFrame
            await skill.EvaluateAsync(binding);
            // Results are saved to binding object
            IReadOnlyList<ObjectDetectorResult> detections = binding.DetectedObjects;
            //foreach (ObjectDetectorResult detection in detections)
            //{
            //    Windows.Foundation.Rect boundingRect = detection.Rect;
            //    ObjectKind objectKind = detection.Kind; // This enum is defined in the ObjectDetector namespace
            //                                            // Use results as desired
            //}
            HashSet<ObjectKind> objectKindsOfInterest = new HashSet<ObjectKind> { ObjectKind.Motorbike, ObjectKind.Car };
           var  vehicleCount = binding.DetectedObjects.Where(
                    detection => objectKindsOfInterest.Contains(detection.Kind)
                ).Count();
            objectKindsOfInterest = new HashSet<ObjectKind> { ObjectKind.Person };
            var personCount = binding.DetectedObjects.Where(
                   detection => objectKindsOfInterest.Contains(detection.Kind)
               ).Count();
            return (personCount,vehicleCount);

        }
    }
}
