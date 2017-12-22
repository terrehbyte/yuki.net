using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YukiSCR
{
    /// <summary>
    /// Interaction logic for FakeCmd.xaml
    /// </summary>
    public partial class FakeCmd : Window
    {
        public Printcher printcher;

        DateTime startTime;
        DateTime lastTime;

        public FakeCmd()
        {
            InitializeComponent();

            startTime = DateTime.Now;
            lastTime = startTime;
        }

        public void BeginTicking()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        } 

        private void Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            float deltaTime = (float)(currentTime - lastTime).TotalSeconds;
            float totalTime = (float)(currentTime - startTime).TotalSeconds;
            lastTime = currentTime;

            Contents.Text += printcher.Advance(deltaTime);

            if(printcher.IsFinished)
            {
                Close();
            }
        }
    }
}
