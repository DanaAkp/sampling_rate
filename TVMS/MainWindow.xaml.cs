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

namespace TVMS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string s = "";
        double[] sampleSource;
        double[] sampleRationing;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog FileOT = new OpenFileDialog();
            FileOT.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (FileOT.ShowDialog() == true)
            {
                Stream ms = new FileStream(FileOT.FileName, FileMode.Open);
                byte[] array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
                string buf = Encoding.Default.GetString(array);
                s = buf.ToLower();
            }
            sampleSource = DiscriptiveStatistics.GetSample(s);
            tbDiscrStat_source.Text = DiscriptiveStatistics.Output_descriptive_statistics(sampleSource);
            tblSourceSample.Text = DiscriptiveStatistics.Output(sampleSource);
        }

        private void BtnRationing_Click(object sender, RoutedEventArgs e)
        {
            sampleRationing = DiscriptiveStatistics.Rationing_MaxMin(sampleSource);
            tblRationning.Text = DiscriptiveStatistics.Output(sampleRationing);
            tbDiscrStat_Ration.Text = DiscriptiveStatistics.Output_descriptive_statistics(sampleRationing);
        }

        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (sd.ShowDialog() == true)
            {
                File.WriteAllText(sd.FileName, DiscriptiveStatistics.Output(sampleRationing));
            }
        }
    }
}
