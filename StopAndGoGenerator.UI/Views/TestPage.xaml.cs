using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace StopAndGoGenerator.UI.Views
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {

        private Dictionary<string, Func<string, bool>> _dictionary;
    private List<string> _list;

    public TestPage()
    {
        InitializeComponent();
        _dictionary = new Dictionary<string, Func<string, bool>>
            {
                {"Individual Bits", TestSingleBits},
                {"Pair Bits", TestSerialBits},
                {"Poker", TestPoker},
                {"Long series", TestLongSerial}
            };
        _list = new List<string>
            {
                "Individual Bits",
                "Pair Bits",
                "Poker",
                "Long series"
            };

        TestsComboBox.ItemsSource = _list.ToList();

    }

    private void TestButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var selected = TestsComboBox.SelectionBoxItem.ToString();
            var result = _dictionary[selected](KeyTextBox.Text);
            OutputTextBox.Text = result.ToString();
        }
        catch (IndexOutOfRangeException)
        {
            MessageBox.Show("W tescie poker liczba bitów musi być podzielna przez 4");
        }
        catch (Exception)
        {

            MessageBox.Show("Proszę wybrać test");
        }

    }

    private bool TestSingleBits(string text)
    {
        double n0 = 0;
        double n1 = 0;
        n0 = text.Count(n => n == '0');
        n1 = text.Length - n0;

        return n1 > 0.48625 * text.Length && n1 < text.Length * 0.51375;
    }

    public bool TestSerialBits(string storedText)
    {
        int n00 = 0, n01 = 0, n10 = 0, n11 = 0; //liczba 00 wciągu, liczba 01 wciągu, liczba 10 wciągu, liczba 11 wciągu
        string temp = "";
        for (int i = 0; i < storedText.Length - 1; i++)
        {
            temp = storedText[i].ToString() + storedText[i + 1].ToString();
            if (temp == "00")
                n00++;
            else if (temp == "01")
                n01++;
            else if (temp == "10")
                n10++;
            else
                n11++;
        }
        int test1 = n00 - n01, test2 = n00 - n10, test3 = n00 - n11;
        if ((test1 < 150 && test1 > -150) && (test2 < 150 && test2 > -150) && (test3 < 150 && test3 > -150))
            return true;
        return false;
    }



    public bool TestPoker(string storedText)
    {

        var tab = storedText.ToCharArray();
        var dSegments = new Dictionary<string, int>
            {
                {"0000", 0},
                {"0001", 0},
                {"0011", 0},
                {"0101", 0},
                {"1001", 0},
                {"0010", 0},
                {"0110", 0},
                {"1010", 0},
                {"0100", 0},
                {"1100", 0},
                {"0111", 0},
                {"1101", 0},
                {"1011", 0},
                {"1110", 0},
                {"1000", 0},
                {"1111", 0}
            };
        double x = 0, result = 0;
        for (var i = 0; i < tab.Length; i += 4)
        {
            var temp = tab[i] + tab[i + 1].ToString() + tab[i + 2] + tab[i + 3];
            dSegments[temp] = dSegments[temp] + 1;
        }
        var values = dSegments.Values;
        foreach (var val in values)
        {
            x += Math.Pow(val, 2);
        }
        result = 0.0032 * x - 5000;
        return Math.Round(result, 2) > 2.16 && Math.Round(result, 2) < 46.17;
    }

    public bool TestLongSerial(string storedText)
    {

        var serie = 0;
        var compare = storedText[0];
        foreach (var c in storedText)
        {
            if (compare == c)
                serie++;
            else
            {
                compare = c;
                serie = 0;
            }
            if (serie >= 26)
                break;
        }
        return serie < 26;
    }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = new StreamReader(openFileDialog.FileName))
                {
                    KeyTextBox.Text = stream.ReadToEnd();
                }
            }
        }
    }
}
