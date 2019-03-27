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
        
        private void Get_theretical_frequency(double[] arr, double[][] intervalArray)
        {
            double X_aver = DiscriptiveStatistics.Average(arr);
            double disp = DiscriptiveStatistics.Dispersion(arr);
            double[] P = new double[11];
            for(int i = 0; i < P.Length; i++)
            {
                double[] buf = intervalArray[i];
                P[i] = DiscriptiveStatistics.GetValueLaplasFunction(Math.Round((buf[0] - X_aver) / disp,2)) * DiscriptiveStatistics.GetValueLaplasFunction(Math.Round((buf[buf.Length - 1] - X_aver) / disp,2));
            }
        }
        private double[][] GetInterval(double[] arr)
        {
            double[] newArray = arr.OrderBy(x => x).ToArray();

            double[][] Res = new double[11][];
            for(int i = 0; i < 2; i++)
            {
                double[] buf = new double[56];
                for (int j = 0; j < buf.Length; j++)
                {
                    buf[j] = newArray[i*buf.Length+j];
                }
                Res[i] = buf;
            }
            for(int i = 2; i < 11; i++)
            {
                double[] buf = new double[50];
                for (int j = 0; j < buf.Length; j++)
                {
                    buf[j] = newArray[i * buf.Length + j];
                }
                Res[i] = buf;
            }
            return Res;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            double[] d = DiscriptiveStatistics.GetSample(DiscriptiveStatistics.OpenExcel("D", 2, 570));
            double[][] newD = GetInterval(d);
            tblres.Text = DiscriptiveStatistics.Output(newD);
        }
    }
}
