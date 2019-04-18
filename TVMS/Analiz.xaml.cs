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
            columArray = new double[colum][];
            koeffPair = new DenseMatrix(colum, colum); 

            for (int i = 0; i < colum; i++)
                columArray[i] = DiscriptiveStatistics.GetSample(File.ReadAllText(@"C:\Users\Данагуль\source\repos\ТВМС\TVMS\Нормированные значения\" + (i + 1).ToString() + ".txt"));

            for (int i = 0; i < colum; i++)
                for (int j = 0; j < colum; j++)
                    koeffPair[i, j] = koeffPair[j, i] = DiscriptiveStatistics.PairKoeff(columArray[i], columArray[j]);

            DenseMatrix t_Matrix = T_Matrix_Koeff(koeffPair);


            tbMatrix.Text = Output_R(koeffPair);
            tbMatrix.Text += "Коэффициент значим при t > 2.178\n";
            tbMatrix.Text += Output_R(t_Matrix);



        }
        
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            tbMatrix2.Text = "";
            koeffPrivate = new DenseMatrix(colum, colum);
            for(int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                    if (i != j)
                    {
                        koeffPrivate[i, j] = Get_AlgebralAddition(koeffPair, i, j) / Math.Sqrt(Get_AlgebralAddition(koeffPair, i, i) * Get_AlgebralAddition(koeffPair, j, j));
                    }
                koeffPrivate[i, i] = koeffPair[i, i];
            }
            DenseMatrix t_Matrix = T_Matrix_Koeff(koeffPrivate);
            tbMatrix2.Text = Output_R(koeffPrivate);
            tbMatrix.Text += "Коэффициент значим при t > 2.178\n";
            tbMatrix2.Text += Output_R(t_Matrix);
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            double det_R = koeffPrivate.Determinant();
            DenseMatrix R = new DenseMatrix(colum);
            for(int i = 0; i < colum; i++)
            {
                for(int j=0;j<colum;j++)
                     R[i,j] = Math.Sqrt(1 - (det_R / Get_AlgebralAddition(koeffPrivate, i,i)));
            }
            tbMatrix3.Text = Output_R(R);
        }

        #region Методы
        public string Output_R(DenseMatrix Matrix)
        {
            string s = "\t";
            for (int i = 0; i < colum; i++) s += (i + 1).ToString() + "\t";
            s += "\n\n";
            for (int i = 0; i < colum; i++)
            {
                s += (i + 1).ToString() + "\t";
                for (int j = 0; j < colum; j++)
                    s += string.Format("{0:F2}\t", Matrix[i, j]);
                s += "\n\n";
            }
            s += "\n\n";
            return s;
        }
        public DenseMatrix T_Matrix_Koeff(DenseMatrix Matrix_Koeff)
        {
            DenseMatrix t_Matrix = new DenseMatrix(colum);
            for (int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                    t_Matrix[i, j] = Math.Abs(Matrix_Koeff[i, j]) * Math.Sqrt((colum - 2) / (1 - Matrix_Koeff[i, j]));
                }
            }
            return t_Matrix;
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
        private double Get_AlgebralAddition(DenseMatrix A, int i, int j)
        {
            return Math.Pow(-1, i + j) * GetMinor(A, colum, i, j).Determinant();
        }
        #endregion
    }
}
