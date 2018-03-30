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


        List<FakeCmd> windows = new List<FakeCmd>();
        List<string> texts = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            // hide the main window
            Visibility = Visibility.Hidden;

            // load text data
            string[] files = System.IO.Directory.GetFiles("./texts");
            foreach (var file in files)
            {
                texts.Add(System.IO.File.ReadAllText(file));
            }

            // begin ticking
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)Math.Floor(timeBetweenWindows * 1000));
            dispatcherTimer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            // do we need to spawn a new window?
            if (windows.Count >= maxWindowCount) { return; }

            var babyWindow = new FakeCmd();
            babyWindow.Closed += BabyWindow_Closed;

            Random rng = new Random();

            babyWindow.printcher = new Printcher();
            babyWindow.printcher.charactersPerSecond = 200;
            babyWindow.printcher.text = texts[rng.Next() % texts.Count];

            var screenRect = new Rect(new System.Windows.Point(SystemInformation.VirtualScreen.Left, SystemInformation.VirtualScreen.Top),
                                      new System.Windows.Point(SystemInformation.VirtualScreen.Right, SystemInformation.VirtualScreen.Bottom));

                
            babyWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            babyWindow.Left = rng.Next(SystemInformation.VirtualScreen.Left, SystemInformation.VirtualScreen.Right);
            babyWindow.Top = rng.Next(SystemInformation.VirtualScreen.Top, SystemInformation.VirtualScreen.Bottom);

            babyWindow.BeginTicking();
            babyWindow.Show();

            windows.Add(babyWindow);
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
