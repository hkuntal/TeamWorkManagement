using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ReadDicomAttributesFromDicomFile
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dicom2exe = @"c:\Users\502230035\Downloads\dcmtk-3.6.0-win32-i386\dcmtk-3.6.0-win32-i386\bin\dcm2xml.exe"; 

            // Create OpenFileDialog 
            var dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".dcm";
            dlg.Filter = "Dicom Files (*.dcm)|*.dcm"; //|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                txtFileName.Text = filename;
            }

            // Read the file into memory
            
            //System.Diagnostics.Process.Start(dicom2exe, dlg.FileName, ) 
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = dicom2exe;
            process.StartInfo.Arguments = dlg.FileName + " " + @"c:\hariom\sampleFilehh.xml";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.Start();
            process.WaitForExit();// Waits here for the process to exit.
        }

        private void BtnLaunchUrl_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                weboutput.Source = new Uri(txtUrlToOpen.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
