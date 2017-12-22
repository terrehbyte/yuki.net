using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace YukiSCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int maxWindowCount = 20;
        const float timeBetweenWindows = 0.5f;

        DateTime startTime;
        DateTime lastTime;

        List<FakeCmd> windows = new List<FakeCmd>();

        float nextWindowSpawnTime = 0.0f;

        public MainWindow()
        {
            InitializeComponent();

            // hide the main window
            Visibility = Visibility.Hidden;

            // bookkeeping
            startTime = DateTime.Now;
            lastTime = startTime;

            // begin ticking
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            
            DateTime currentTime = DateTime.Now;
            float deltaTime = (float)((currentTime - lastTime).TotalSeconds);
            float totalTime = (float)(currentTime - startTime).TotalSeconds;
            lastTime = currentTime;

            // do we need to spawn a new window?
            if (totalTime > nextWindowSpawnTime && windows.Count < maxWindowCount)
            {
                var babyWindow = new FakeCmd();
                babyWindow.Closed += BabyWindow_Closed;

                babyWindow.printcher = new Printcher();
                babyWindow.printcher.wordsPerMinute = 1.0f;
                babyWindow.printcher.text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\nDonec interdum magna neque, vel porta lorem pellentesque fermentum.\nIn placerat volutpat nunc.";

                babyWindow.BeginTicking();
                babyWindow.Show();

                var screenRect = new Rect(new System.Windows.Point(SystemInformation.VirtualScreen.Left, SystemInformation.VirtualScreen.Top),
                                          new System.Windows.Point(SystemInformation.VirtualScreen.Right, SystemInformation.VirtualScreen.Bottom));

                Random rng = new Random();
                babyWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                babyWindow.Left = rng.Next(0, SystemInformation.VirtualScreen.Right);
                babyWindow.Top = rng.Next(0, SystemInformation.VirtualScreen.Bottom);

                windows.Add(babyWindow);
                nextWindowSpawnTime = totalTime + timeBetweenWindows;
            }
        }

        private void BabyWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                windows.Remove(sender as FakeCmd);
            }
            catch
            {

            }
        }
    }
}
