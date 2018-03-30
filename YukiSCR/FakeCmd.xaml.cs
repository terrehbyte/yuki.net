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
        public int charactersPerUpdate = 1;

        public FakeCmd()
        {
            InitializeComponent();
        }

        public void BeginTicking()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(Math.Floor(printcher.SecondsBetweenLetters * charactersPerUpdate * 1000)));
            dispatcherTimer.Start();
        } 

        private void Tick(object sender, EventArgs e)
        {
            Contents.Text += printcher.Advance(printcher.SecondsBetweenLetters * charactersPerUpdate);

            if(printcher.IsFinished)
            {
                Close();
            }
        }
    }
}
