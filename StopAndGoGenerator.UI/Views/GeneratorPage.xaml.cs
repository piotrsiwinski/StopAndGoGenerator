using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using Microsoft.Win32;

namespace StopAndGoGenerator.UI.Views
{
    /// <summary>
    /// Interaction logic for GeneratorPage.xaml
    /// </summary>
    public partial class GeneratorPage
    {
        private double _progress;
        private StopAndGo _generator;
        private Thread _workerThread;
        private Thread _progressThread;
        public GeneratorPage()
        {
            InitializeComponent();
            _generator = new StopAndGo();
            var source = GetDefaultPolynomials();
            FirstLfsrPolynomial.ItemsSource = source;
            SecondLfsrPolynomial.ItemsSource = source;
            ThirdLfsrPolynomial.ItemsSource = source;
        }

        private void InitializeStopAndGo()
        {
            _generator.FirstLfsr = CreateLfsr(FirstLfsrRegisterStart, FirstLfsrPolynomial);
            _generator.SecondLfsr = CreateLfsr(SecondLfsrRegisterStart, SecondLfsrPolynomial);
            _generator.ThirdLfsr = CreateLfsr(ThirdLfsrRegisterStart, ThirdLfsrPolynomial);
        }

        private static Lfsr CreateLfsr(TextBox registerStart, ComboBox polynomial)
        {
            var text = (string) polynomial.SelectedValue;
            var tapSeqence = text.Split(' ', ';',':', ',').Select(int.Parse).ToArray();
            return new Lfsr(tapSeqence, int.Parse(registerStart.Text));
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeStopAndGo();
            OutputTextBox.Text = "";
            ProgressBar.Value = 0;

            var count = int.Parse(BitsNumberTextBox.Text);
            _workerThread = new Thread(() =>
            {
                var result = _generator.GenerateRandomValues(count, out _progress);
                Dispatcher.Invoke(() => OutputTextBox.Text = result);
            });
            _workerThread.Start();
            _progressThread = new Thread(UpdateProgressBar);
            _progressThread.Start();
        }
        private void UpdateProgressBar()
        {
            while (_progress < 100)
            {
                Dispatcher.Invoke(() => ProgressBar.Value = _progress);
            }
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var stream = new StreamWriter(saveFileDialog.FileName))
                {
                    stream.Write(OutputTextBox.Text);
                }
            }
        }

        private List<string> GetDefaultPolynomials()
        {
            List<string> result = new List<string>(300);
            string path = "../../Polynomials/wielomiany.txt";
            using (var stream = new StreamReader(path))
            {
                while (!stream.EndOfStream)
                {
                    result.Add(stream.ReadLine());
                }
            }
            return result;
        }
    }
}
