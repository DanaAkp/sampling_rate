using Microsoft.Win32;
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
        private double[] Get_theretical_frequency(double[] arr, double[][] intervalArray)
        {
            double X_aver = DiscriptiveStatistics.Average(arr);
            double disp = DiscriptiveStatistics.Dispersion(arr);
            double[] P = new double[11];
            for(int i = 0; i < P.Length; i++)
            {
                double[] buf = intervalArray[i];
                P[i] = DiscriptiveStatistics.GetValueLaplasFunction(Math.Round((buf[0] - X_aver) / disp,2)) * DiscriptiveStatistics.GetValueLaplasFunction(Math.Round((buf[buf.Length - 1] - X_aver) / disp,2));
                P[i] *= buf.Length;
            }
            return P;
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
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ObjWorkbook = ObjExcel.Workbooks.Open(ofd.FileName, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Microsoft.Office.Interop.Excel.Worksheet objworksheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkbook.Sheets[1];


                Microsoft.Office.Interop.Excel.Range range = objworksheet.get_Range(Colum + i.ToString(), Colum + i.ToString());
                for (int i = Start; i < End + 1; i++)
                {
                    Microsoft.Office.Interop.Excel.Range range = objworksheet.get_Range(Colum + i.ToString(), Colum + i.ToString());
                    s += range.Text + "\r\n";
                }
                ObjExcel.Quit();
            }

            double[] d = DiscriptiveStatistics.GetSample(DiscriptiveStatistics.OpenExcel("C", 2, 570));
            double[][] newD = GetInterval(d);

            double x2 = 0;
            //double[] m_theor = Get_theretical_frequency(d, newD);
            double[] m_theor = theoretical_freq(d, newD);
            for (int i = 0; i < newD.Length; i++)
            {
                double[] buf = newD[i];
                x2 += (buf.Length - m_theor[i]) * (buf.Length - m_theor[i]) / m_theor[i];
            }

            tblres.Text = x2.ToString();
        }
        private double[] theoretical_freq(double[] arr, double[][] intervalArr)
        {
            double X_aver = DiscriptiveStatistics.Average(arr);
            double disp = DiscriptiveStatistics.Dispersion(arr);
            double[] P = new double[11];

            for(int i = 0; i < intervalArr.Length; i++)
            {
                double[] buf = intervalArr[i];
                double u = (buf[0] - X_aver) / disp;
                double f_u = Math.Pow(Math.E, -(u * u) / 2) / Math.Sqrt(2 * Math.PI);
                P[i] = buf.Length / disp * f_u;
            }

            return P;
        }
    }
}
