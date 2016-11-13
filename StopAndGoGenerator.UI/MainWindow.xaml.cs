using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StopAndGoGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _progress;
        private StopAndGo _generator;
        private Thread _workerThread;
        private Thread _progressThread;
        public MainWindow()
        {
            InitializeComponent();
            _generator = new StopAndGo();
            _workerThread = new Thread(GenerateOutput);
            _progressThread = new Thread(UpdateProgressBar);
        }

        private void GenerateOutput(object count)
        {
            var result = _generator.GenerateRandomValues((int)count, out _progress);
            Dispatcher.Invoke(() => OutputTextBox.Text = result);
        }

        private void UpdateProgressBar()
        {
            while (_progress < 100)
            {
                Dispatcher.Invoke(() => ProgressBar.Value = _progress);
            }
            
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = "";
            ProgressBar.Value = 0;


            var count =  int.Parse(BitsNumberTextBox.Text);
            _workerThread = new Thread(() =>
            {
                var result = _generator.GenerateRandomValues(count, out _progress);
                Dispatcher.Invoke(() => OutputTextBox.Text = result);
            });
            _workerThread.Start();
            _progressThread = new Thread(UpdateProgressBar);
            _progressThread.Start();


        }
    }
}
