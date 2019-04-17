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
using MathNet;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;

namespace TVMS
{
    /// <summary>
    /// Логика взаимодействия для Analiz.xaml
    /// </summary>
    public partial class Analiz : System.Windows.Window
    {
        double[][] columArray;
        DenseMatrix koeffPair;
        DenseMatrix koeffPrivate;
        int colum = 12;
        public Analiz()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            tbMatrix.Text = "";

            columArray = new double[colum][];
            koeffPair = new DenseMatrix(colum, colum); 

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

        private double Get_AlgebralAddition(DenseMatrix A, int i, int j)
        {
            return Math.Pow(-1, i + j) * GetMinor(A, colum, i, j).Determinant();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            tbMatrix2.Text = "";
            koeffPrivate = new DenseMatrix(colum, colum);
            for(int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                    if (i != j) koeffPrivate[i, j] = Get_AlgebralAddition(koeffPair, i, j) / Math.Sqrt(Get_AlgebralAddition(koeffPair, i, i) * Get_AlgebralAddition(koeffPair, j, i));
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
            Random rand = new Random();
            var test = new DenseMatrix(4, 4);
            for(int i = 0; i < test.RowCount; i++)
            {
                for(int j = 0; j < test.ColumnCount; j++)
                {
                    test[i, j] = rand.Next(3);
                }
            }
            tbMatrix3.Text = test.Determinant().ToString();
        }
        public static DenseMatrix GetMinor(DenseMatrix A, int n, int i, int j)
        {
            DenseMatrix M = new DenseMatrix(n - 1, n - 1);

            for (int k = 0; k < n; k++)
            {
                for (int m = 0; m < n; m++)
                {
                    if (k < i && m < j) M[k, m] = A[k, m];
                    if (k > i && m > j) M[k - 1, m - 1] = A[k, m];
                    if (k > i && m < j) M[k - 1, m] = A[k, m];
                    if (k < i && m > j) M[k, m - 1] = A[k, m];
                }
            }

            return M;
        }
    }
}
