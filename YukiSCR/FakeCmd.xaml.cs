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
        public const int uiUpdateIntervalInMs = 20;

        Binding binding;
        BindingExpression bindingExp;

        DateTime lastTime;

        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public FakeCmd()
        {
            InitializeComponent();
        }

        public void BeginTicking()
        {
            binding = new Binding("displayText");
            binding.Source = printcher;
            binding.Mode = BindingMode.OneWay;

            BindingOperations.SetBinding(Contents, TextBox.TextProperty, binding);
            bindingExp = BindingOperations.GetBindingExpression(Contents, TextBox.TextProperty);

            int interval = Math.Max((int)(Math.Floor(printcher.SecondsBetweenLetters * charactersPerUpdate * 1000)), uiUpdateIntervalInMs);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            dispatcherTimer.Start();

            lastTime = DateTime.Now;
        } 

        private void Tick(object sender, EventArgs e)
        {
            printcher.Advance((float)((DateTime.Now - lastTime).TotalMilliseconds / 1000));

            bindingExp.UpdateTarget();

            ScrollPane.ScrollToEnd();

            lastTime = DateTime.Now;

            if (printcher.IsFinished)
            {
                dispatcherTimer.Stop();
                BindingOperations.ClearAllBindings(Contents);
                Close();
            }
        }
    }
}
