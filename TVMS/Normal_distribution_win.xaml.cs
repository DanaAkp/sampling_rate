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

namespace TVMS
{
    /// <summary>
    /// Логика взаимодействия для Normal_distribution_win.xaml
    /// </summary>
    public partial class Normal_distribution_win : Window
    {
        public Normal_distribution_win()
        {
            InitializeComponent();
        }
        private void Get_X_2(double[] arr)
        {

        }
        
        private void Get_theretical_frequency(double[] arr)
        {
            
        }
        private double[][] GetInterval(double[] arr, int ElementInInterval)
        {
            double[][] Res = new double[arr.Length / ElementInInterval + 1][];
            for(int i = 0; i < arr.Length/ElementInInterval; i++)
            {
                double[] buf = new double[ElementInInterval];
                for (int j=0;j<ElementInInterval;j++)
                {
                    buf[j] = arr[i + j];
                }
                Res[i] = buf;
            }
            return Res;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            double[] d = DiscriptiveStatistics.GetSample(DiscriptiveStatistics.OpenExcel("D", 2, 570));
            double[][] newD = GetInterval(d, 5);
            tblres.Text = DiscriptiveStatistics.Output(newD);
        }
    }
}
