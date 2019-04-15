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
using LiveCharts;
using LiveCharts.Wpf;

namespace TVMS
{
    /// <summary>
    /// Логика взаимодействия для Analiz.xaml
    /// </summary>
    public partial class Analiz : Window
    {
        double[][] columArray;
        double[,] koeffPair;
        double[,] koeffPrivate;
        int colum = 12;
        public Analiz()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            tbMatrix.Text = "";

            columArray = new double[colum][];
            koeffPair = new double[colum, colum];

            for (int i = 0; i < colum; i++)
            {
                columArray[i] = DiscriptiveStatistics.GetSample(File.ReadAllText(@"C:\Users\Данагуль\source\repos\ТВМС\TVMS\Нормированные значения\" + (i + 1).ToString() + ".txt"));
            }

            for(int i = 0; i < colum; i++)
            {
                for(int j = 0; j < colum; j++)
                {
                    koeffPair[i, j] = koeffPair[j, i] = DiscriptiveStatistics.PairKoeff(columArray[i], columArray[j]);
                }
            }

            for (int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                    tbMatrix.Text += string.Format("{0:F2}\t", koeffPair[i, j]);

                tbMatrix.Text += "\n\n";
            }
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            tbMatrix2.Text = "";
               koeffPrivate = new double[colum, colum];
            for(int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                    if (i != j) koeffPrivate[i, j] = DiscriptiveStatistics.PrivateKoeff(koeffPair[0, i], koeffPair[0, j], koeffPair[i, j]);
                koeffPrivate[i, i] = koeffPair[i, i];
            }

            for (int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                    tbMatrix2.Text += string.Format("{0:F2}\t", koeffPrivate[i, j]);

                tbMatrix2.Text += "\n\n";
            }

        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {

        }
        public static double GetDeterminant_3(double[,] A, int n)
        {
            return A[0, 0] * A[1, 1] * A[2, 2] + A[0, 1] * A[1, 2] * A[2, 0] + A[1, 0] * A[2, 1] * A[0, 2] - A[2, 0] * A[1, 1] * A[0, 2] - A[1, 0] * A[0, 1] * A[2, 2] - A[2, 1] * A[1, 2] * A[0, 0];
        }
        public static double[,] GetMinor(double[,] A, int n, int i, int j)
        {
            double[,] M = new double[n - 1, n - 1];

            for (int k = 0; k < n; k++)
            {
                for (int m = 0; m < n; m++)
                {
                    //if (i != k && m != j)
                    //{
                    if (k < i && m < j) M[k, m] = A[k, m];
                    if (k > i && m > j) M[k - 1, m - 1] = A[k, m];
                    if (k > i && m < j) M[k - 1, m] = A[k, m];
                    if (k < i && m > j) M[k, m - 1] = A[k, m];
                    //}
                }
            }

            return M;
        }
        public static double GetDeterminant_4(double[,] A, int n)
        {
            double determ = 0;
            for (int i = 0; i < n; i++)
            {
                determ += A[0, i] * Math.Pow(-1, 2 + i) * GetDeterminant_3(GetMinor(A, n, 0, i), n);
            }
            return determ;
        }
    }
}
