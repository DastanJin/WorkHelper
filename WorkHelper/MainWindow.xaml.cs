using Microsoft.Win32;
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

namespace WorkHelper
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private static string ByteToBase64String(byte[] data) => Convert.ToBase64String(data);

    private void Text_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (!text.IsSelectionActive) return;
      base64.Text = ByteToBase64String(Encoding.UTF8.GetBytes(text.Text));
    }

    private void Base64_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (!base64.IsSelectionActive) return;
      try
      {
        text.Text = Encoding.UTF8.GetString(Convert.FromBase64String(base64.Text));
      }
      catch (Exception ex)
      {
        text.Text = $"Wrong BASE64 format...\n{ex.Message}";
      }
    }

    private void FileLoadButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        OpenFileDialog theDialog = new()
        {
          Title = "Upload Your File",
          InitialDirectory = @"C:\"
        };
        Stream myStream;
        if (theDialog.ShowDialog() != true) return;

        if ((myStream = theDialog.OpenFile()) == null) return;

        using MemoryStream ms = new();
        myStream.CopyTo(ms);
        var base64EncodedBytes = ms.ToArray();

        text.Text = Encoding.UTF8.GetString(base64EncodedBytes);
        base64.Text = ByteToBase64String(base64EncodedBytes);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
      }
    }


  }




}

