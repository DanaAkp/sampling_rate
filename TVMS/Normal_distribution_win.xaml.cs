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
        private double[] Get_theretical_frequency_Laplas(double[] arr, double[][] intervalArray)
        {
            double X_aver = DiscriptiveStatistics.Average(arr);
            double disp = Math.Sqrt(DiscriptiveStatistics.Dispersion(arr));
            double[] P = new double[intervalArray.Length];
            for(int i = 0; i < P.Length; i++)
            {
                double[] buf = intervalArray[i];
                P[i] = DiscriptiveStatistics.GetValueLaplasFunction(Math.Round(Math.Abs((buf[0] - X_aver) / disp),2)) * DiscriptiveStatistics.GetValueLaplasFunction(Math.Round(Math.Abs((buf[buf.Length - 1] - X_aver) / disp),2));
                P[i] *= buf.Length;
            }
            return P;
        }
        private double[][] GetInterval(double[] arr)
        {
            double[] newArray = arr.OrderBy(x => x).ToArray();

            #region 11
            double[][] Res = new double[11][];
            for (int i = 0; i < 2; i++)
            {
                double[] buf = new double[56];
                for (int j = 0; j < buf.Length; j++)
                {
                    buf[j] = newArray[i * buf.Length + j];
                }
                Res[i] = buf;
            }
            for (int i = 2; i < 11; i++)
            {
                double[] buf = new double[50];
                for (int j = 0; j < buf.Length; j++)
                {
                    buf[j] = newArray[i * buf.Length + j];
                }
                Res[i] = buf;
            }
            #endregion

            #region 31
            //double[][] Res = new double[31][];
            //for (int i = 0; i < 20; i++)
            //{
            //    double[] buf = new double[17];
            //    for (int j = 0; j < buf.Length; j++)
            //    {
            //        buf[j] = newArray[i * buf.Length + j];
            //    }
            //    Res[i] = buf;
            //}
            //for (int i = 20; i < 28; i++)
            //{
            //    double[] buf = new double[20];
            //    for (int j = 0; j < buf.Length; j++)
            //    {
            //        buf[j] = newArray[340+(i-20)*buf.Length + j];
            //    }
            //    Res[i] = buf;
            //}
            //for (int i = 28; i < 31; i++)
            //{
            //    double[] buf = new double[23];
            //    for (int j = 0; j < buf.Length; j++)
            //    {
            //        buf[j] = newArray[500+(i-28)*buf.Length + j];
            //    }
            //    Res[i] = buf;
            //}
            #endregion

            #region 114
            //double[][] Res = new double[114][];
            //for (int i = 0; i < 113; i++)
            //{
            //    double[] buf = new double[5];
            //    for (int j = 0; j < buf.Length; j++)
            //    {
            //        buf[j] = newArray[i * buf.Length + j];
            //    }
            //    Res[i] = buf;
            //}
            //double[] buf2 = new double[4];
            //buf2[0] = newArray[newArray.Length - 4];
            //buf2[1] = newArray[newArray.Length - 3];
            //buf2[2] = newArray[newArray.Length - 2];
            //buf2[3] = newArray[newArray.Length - 1];
            //Res[Res.Length - 1] = buf2;
            #endregion

            return Res;
        }


        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            double k = 43.2;
            int interval = 31;
            int colum = 12;

            double[][] columArray = new double[colum][];
            double[][] points = new double[colum][];
            double[][] xn = new double[colum][];
            double[][] m_e = new double[colum][];
            double[][] m_t = new double[colum][];
            double[] x2_arr = new double[colum];

            for (int i = 0; i < colum; i++)
            {
                columArray[i] = DiscriptiveStatistics.GetSample(File.ReadAllText(@"C:\Users\Данагуль\source\repos\ТВМС\TVMS\Нормированные значения\" + (i + 1).ToString() + ".txt"));

                points[i] = point(columArray[i], interval);
                double[] me_buf = m_e[i] = Get_m_e(columArray[i], points[i], interval);
                xn[i] = Get_xn(columArray[i], points[i], interval);
                double[] mt_buf = m_t[i] = theoretical_freq(columArray[i], m_e[i].Sum(), xn[i], interval);

                for (int j = 0; j < mt_buf.Length; j++)
                {
                    x2_arr[i] += Math.Pow(me_buf[j] - mt_buf[j], 2) / mt_buf[j];
                }
            }
            #region Вывод
            tb0x.Text = x2_arr[1].ToString();
            if (x2_arr[1] <k) tb0x.Text += "\nНормальное распределение";
            else tb0x.Text += "\nНе нормальное распределение";

            tb1x.Text = x2_arr[2].ToString();
            if (x2_arr[2] < k) tb1x.Text += "\nНормальное распределение";
            else tb1x.Text += "\nНе нормальное распределение";

            tb2x.Text = x2_arr[3].ToString();
            if (x2_arr[3] < k) tb2x.Text += "\nНормальное распределение";
            else tb2x.Text += "\nНе нормальное распределение";

            tb3x.Text = x2_arr[4].ToString();
            if (x2_arr[4] < k) tb3x.Text += "\nНормальное распределение";
            else tb3x.Text += "\nНе нормальное распределение";

            tb4x.Text = x2_arr[5].ToString();
            if (x2_arr[5] < k) tb4x.Text += "\nНормальное распределение";
            else tb4x.Text += "\nНе нормальное распределение";

            tb5x.Text = x2_arr[6].ToString();
            if (x2_arr[6] < k) tb5x.Text += "\nНормальное распределение";
            else tb5x.Text += "\nНе нормальное распределение";

            tb6x.Text = x2_arr[7].ToString();
            if (x2_arr[7] < k) tb6x.Text += "\nНормальное распределение";
            else tb6x.Text += "\nНе нормальное распределение";

            tb7x.Text = x2_arr[8].ToString();
            if (x2_arr[8] < k) tb7x.Text += "\nНормальное распределение";
            else tb7x.Text += "\nНе нормальное распределение";

            tb8x.Text = x2_arr[9].ToString();
            if (x2_arr[9] < k) tb8x.Text += "\nНормальное распределение";
            else tb8x.Text += "\nНе нормальное распределение";

            tb9x.Text = x2_arr[10].ToString();
            if (x2_arr[10] < k) tb9x.Text += "\nНормальное распределение";
            else tb9x.Text += "\nНе нормальное распределение";

            tb10x.Text = x2_arr[11].ToString();
            if (x2_arr[11] < k) tb10x.Text += "\nНормальное распределение";
            else tb10x.Text += "\nНе нормальное распределение";

            #endregion
        }
        private double[] theoretical_freq(double[] arr,double sum, double[] xn, int CountInterval)
        {
            double X_aver = DiscriptiveStatistics.Average(arr);
            double disp = Math.Sqrt( DiscriptiveStatistics.Dispersion(arr));
            double[] m_t = new double[CountInterval];
            double h = (DiscriptiveStatistics.Max(arr) - DiscriptiveStatistics.Min(arr)) / CountInterval;
            for (int i = 0; i < m_t.Length; i++)
            {
                double u = (xn[i] - X_aver) / disp;
                double f_u =1 / (Math.Sqrt(2 * Math.PI) * Math.Pow(Math.E, (u * u) / 2));
                m_t[i] = sum * h/ disp * f_u;
            }
            return m_t;
        }

        private double[] point(double[] arr, int CountInterval)
        {
            double max = DiscriptiveStatistics.Max(arr);
            double min = DiscriptiveStatistics.Min(arr);
            double[] points = new double[CountInterval + 1];
            for(int i = 0; i < points.Length; i++)
            {
                points[i] = max - i * (max - min) / CountInterval;
            }

            return points.OrderBy(x => x).ToArray();
        }
        private double[] Get_m_e(double[] arr, double[] points, int CountInterval)
        {
            double[] m_e = new double[CountInterval];
            for(int i = 0; i < points.Length; i++)
            {
                foreach(double x in arr)
                {
                    if (points[i] < x && x < points[i + 1]) m_e[i]++;
                }
            }
            return m_e;

        }
        private double[] Get_xn(double[] arr, double[] points, int CountInterval)
        {
            double[] xn = new double[CountInterval];
            for(int i = 0; i < points.Length-1; i++)
            {
                xn[i] = points[i + 1] - (points[i + 1] - points[i]) / 2;
            }
            return xn;
        }
    }
}
