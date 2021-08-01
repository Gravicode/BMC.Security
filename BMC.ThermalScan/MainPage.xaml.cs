using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BMC.ThermalScan
{
    /// <summary>
    /// Sample app that reads data over I2C from an attached ADXL345 accelerometer
    /// MSのサンプルをもとに作成
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Mlx90640 ThermCam = new Mlx90640();

        public double UpperLimit
        {
            get => ThermCam.UpperLimit;
            set
            {
                if (value > LowerLimit)
                {
                    // サーマル画像の上限温度を設定
                    ThermCam.UpperLimit = value;
                }
            }
        }

        public double LowerLimit
        {
            get => ThermCam.LowerLimit;
            set
            {
                if (value < UpperLimit)
                {
                    // サーマル画像の下限温度を設定
                    ThermCam.LowerLimit = value;
                }
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = this;

            // アプリ終了時のクリーンアップ処理のハンドラを登録
            Unloaded += MainPage_Unloaded;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
        async void dispatcherTimer_Tick(object sender, object e)
        {
            if (dataTemp == null) return;
            if (client == null) client = new HttpClient();
            var json = JsonConvert.SerializeObject(dataTemp);
            var res = await client.PostAsync(UrlPbi, new StringContent(json));
            if ((res.IsSuccessStatusCode))
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    TxtStatus.Text = $"{DateTime.Now} - Sukses kirim data ke PBI";
                });
            }
            else
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    TxtStatus.Text = $"{DateTime.Now} - Gagal kirim data ke PBI";
                });
            }
        }
        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ThermCam.InitThermalCamera();
            //set timer 

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
            var t = Task.Run(async () =>
            {
                while (true)
                {
                    // 手順１ サーマルカメラから温度データを取得(時間がかかるのでUIスレッドでやらないこと)
                    var result = ThermCam.GetTemperatureData();

                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        // 手順２ サーマルカメラから温度画像を取得(GetWriteableBitmap()を直前に実行している必要あり)
                        var bmp = await ThermCam.GetWriteableBitmap();

                        // 中心3マス分の値をとる
                        var center = result.Where((res, i) =>
                        {
                            int columnCenter = 32 / 2;
                            int rowCenter = 24 / 2;

                            int column = i % 32;
                            int row = i / 32;

                            return ((column >= columnCenter - 1) && (column <= columnCenter + 1))
                                    && ((row >= rowCenter - 1) && (row <= rowCenter + 1));
                        });

                        // 手順３ 採った温度データと画像をUIにセット
                        imageMain.Source = bmp;
                        tbTemparature.Text = center.Average().ToString("F2") + " ℃";
                        if (dataTemp == null)
                        {
                            dataTemp = new SensorData();
                            dataTemp.Sensor = "MLX90640";
                        }
                        dataTemp.Suhu = (float)center.Average();
                        dataTemp.Waktu = DateTime.Now;
                    });

                    // 手順４ 二週目の前にとりあえずDelay(1)
                    await Task.Delay(1);
                }
            });
        }
        string UrlPbi = "https://api.powerbi.com/beta/e4a5cd36-e58f-4f98-8a1a-7a8e545fc65a/datasets/079c3613-7862-447e-ad17-6a93a62eeb6f/rows?key=QrN9qUl0VHnneWX2Kw%2BUfE4Bk4f0MJ1eihKYfSWpy9WxlscUmPzYUw7%2FunPVW1PXjt7AjN%2Fstb0LdRH0JFkN%2Fg%3D%3D";
        static DispatcherTimer dispatcherTimer;
        static SensorData dataTemp;
        static HttpClient client;
        private void MainPage_Unloaded(object sender, object args)
        {
            ThermCam.Dispose();
            ThermCam = null;
        }
    }


   

    public class SensorData
    {
        public DateTime Waktu { get; set; }
        public float Suhu { get; set; }
        public string Sensor { get; set; }
    }

}
