using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Android.App;

namespace MyMobilProject.Views
{
    public partial class AboutPage : ContentPage
    {

        public float num1 = 0.8f;
        public float num2 = 0.9f;
        public float num3 = 0.2f;
        public float num4 = 0.1f;

        public float Aceler_dataX = 0.0f;
        public float Aceler_dataY = 0.0f;
        public float Aceler_dataZ = 0.0f;

        public bool c;
        public bool d;

        private int counter;
        private int result;

        public AboutPage()
        {
            InitializeComponent();
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }


        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            result = counter / 2;
            Aceler_dataX = data.Acceleration.X;
            Aceler_dataY = data.Acceleration.Y;
            Aceler_dataZ = data.Acceleration.Z;
            Result_Condition();

            Rakaat_1.Text = Convert.ToString(result);

            if (counter / 2 == 3)
            {
                 Vibtate_Controll(1);
            }

            else if (counter / 2 == 6) 
            {
            Vibtate_Controll(2);
            }

            else if (counter / 2 == 9)
            {
                Vibtate_Controll(3);
            }

        }

        public bool ResultCounter_Y_1(bool condition) // checking the condition Y 1 - 0.8 to 0.9
        {
            if (Aceler_dataY >= num1 && Aceler_dataY <= num2)
            {
                condition = true;
            }
            else
            {
                condition = false;
            }
            return condition;
        }

        public bool ResultCounter_Y_2(bool condition) // checking the condition Y 2 - 0.2 to 0.1
        {
            if (Aceler_dataY <= num3 && Aceler_dataY >= num4)
            {
                condition = true;
            }
            else
            {
                condition = false;
            }
            return condition;
        }

        public void Result_Condition() //  Checking the fulfillment of conditions with Accelerometers and output counter +1 in the case of true true
        {

            if (ResultCounter_Y_1(false) == true) { c = true; }
            if (ResultCounter_Y_2(false) == true) { d = true; }

            if ( c == true && d == true)
            {
                c = false;
                d = false;
                counter += 1;
            }
        }

            public void Click_Start(object sender, System.EventArgs e) //Start button
        {
            Accelerometer.Start(SensorSpeed.UI);
            DependencyService.Get<IAndroidService>().StartService();
            }


            public void Click_Stop(object sender, System.EventArgs e) //Stop button
        {
            Accelerometer.Stop();
            DependencyService.Get<IAndroidService>().StopService();
            counter=0;
            }



        private async void Vibtate_Controll(int count_vibr) //Vibration control
        {
         var durition = TimeSpan.FromSeconds(0.3);
              
            for (int i = 0; i < count_vibr; i++)
            {
                    Vibration.Vibrate(durition);
                    await Task.Delay(700);
            }
        }

    }
}