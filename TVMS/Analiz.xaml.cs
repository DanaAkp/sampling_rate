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
        public Analiz()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            int colum = 12;
            tbMatrix.Text = "";

            double[][] columArray = new double[colum][];
            double[,] koeff = new double[colum,colum];

            for (int i = 0; i < colum; i++)
            {
                columArray[i] = DiscriptiveStatistics.GetSample(File.ReadAllText(@"C:\Users\Данагуль\source\repos\ТВМС\TVMS\Нормированные значения\" + (i + 1).ToString() + ".txt"));
            }

            for(int i = 0; i < colum; i++)
            {
                for(int j = 0; j < colum; j++)
                {
                    koeff[i, j] = koeff[j, i] = DiscriptiveStatistics.PairKoeff(columArray[i], columArray[j]);
                }
            }

            for (int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                    tbMatrix.Text += string.Format("{0:F2}\t", koeff[i, j]);

                tbMatrix.Text += "\n\n";
            }
        }
    }
}
