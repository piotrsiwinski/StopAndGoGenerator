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
using XorEncrypt;

namespace StopAndGoGenerator.UI.Views
{
    /// <summary>
    /// Interaction logic for EncryptionPage.xaml
    /// </summary>
    public partial class EncryptionPage : Page
    {
        private XorEncryption _encryption;
        public EncryptionPage()
        {
            InitializeComponent();
            _encryption = new XorEncryption();
        }

        private void ReadFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = new StreamReader(openFileDialog.FileName))
                {
                    PlainTextTextBox.Text = stream.ReadToEnd();
                }
            }
        }

        private void EncryptMessageButton_OnClick(object sender, RoutedEventArgs e)
        {
            EncryptedMessageTextBox.Text = _encryption.Encrypt(KeyTextTextBox.Text, PlainTextTextBox.Text);
        }

        private void SaveMessageToFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var stream = new StreamWriter(saveFileDialog.FileName))
                {
                    stream.Write(EncryptedMessageTextBox.Text);
                }
            }
        }

        private void GetKeyTextFromFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = new StreamReader(openFileDialog.FileName))
                {
                    KeyTextTextBox.Text = stream.ReadToEnd();
                }
            }
        }
    }
}
