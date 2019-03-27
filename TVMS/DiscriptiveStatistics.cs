using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVMS
{

    class DiscriptiveStatistics
    {
        /// <summary>
        /// Нахождение среднего для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Average(double[] arr)
        {
            return arr.Sum() / arr.Length;
        }
        /// <summary>
        /// Нахождение стандартной ошибки для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double StandartError(double[] arr)
        {
            return StandartDeviation(arr) / Math.Sqrt(arr.Length);
        }
        /// <summary>
        /// Нахождение моды для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Fashion(double[] arr)
        {
            var dic = arr.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => new { Element = y.Key, Count = y.Count() }).ToList();
            var w = from v in dic orderby v.Count select v;
            return w.Last().Element;
        }
        /// <summary>
        /// Нахождение максимального элемента для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Max(double[] arr)
        {
            return arr.Max();
        }
        /// <summary>
        /// Нахождение минимального элемента для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Min(double[] arr)
        {
            return arr.Min();
        }
        /// <summary>
        /// Нахождение эксцеса для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Excess(double[] arr)
        {
            double aver = Average(arr);
            double res = 0;
            foreach (double x in arr)
                res += (x - aver) * (x - aver) * (x - aver) * (x - aver);
            double n = arr.Length;
            res /= Math.Pow(StandartDeviation(arr), 4);
            res *= (n * (n + 1)) / ((n - 1) * (n - 2) * (n - 3));
            res -= 3 * (n - 1) * (n - 1) / ((n - 2) * (n - 3));
            return res;
        }
        /// <summary>
        /// Нахождение суммы всех элементов для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Amount(double[] arr)
        {
            return arr.Sum();
        }
        /// <summary>
        /// Нахождение интервала для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Interval(double[] arr)
        {
            return Max(arr) - Min(arr);
        }
        /// <summary>
        /// Нахождение стандартного отклонения для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double StandartDeviation(double[] arr)
        {
            return Math.Sqrt(Dispersion(arr));
        }
        /// <summary>
        /// Нахождение асимметрии для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Asymmetry(double[] arr)
        {
            double aver = Average(arr);
            double res = 0;
            foreach (double x in arr)
                res += (x - aver) * (x - aver) * (x - aver);
            res /= Math.Pow(StandartDeviation(arr), 3);
            res *= (double)arr.Length / (double)((arr.Length - 1) * (arr.Length - 2));
            return res;
        }
        /// <summary>
        /// Нахождение медианы для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Median(double[] arr)
        {
            var s = from t in arr orderby t select t;
            var d = s.Select((elem, index) => new { index, elem = arr[index] });

            var e = s.ToArray().GetValue(s.ToArray().Count() / 2);

            if (arr.Length % 2 != 0) return (double)e;

            var f1 = s.ToArray().GetValue(s.ToArray().Count() / 2 - 1);
            return ((double)f1 + (double)e) / 2;
            //return f1;
        }
        /// <summary>
        /// Нахождение дисперсии для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        /// <returns></returns>
        public static double Dispersion(double[] arr)
        {
            double aver = Average(arr);
            double res = 0;
            foreach (double x in arr)
                res += (x - aver) * (x - aver);
            res /= arr.Length - 1;
            return res;
        }
        /// <summary>
        /// Вывод описательной статистики для какой-то совокупности
        /// </summary>
        /// <param name="arr">Какая-то совокупность</param>
        public static string Output_descriptive_statistics(double[] arr)
        {
            string s = "Описательная статистика: ";
            s += "\nСреднее: " + Average(arr);
            s += "\nСумма: " + Amount(arr);
            s += "\nСтандартная ошибка: " + StandartError(arr);
            s += "\nМедиана: " + Median(arr);
            s += "\nМода: " + Fashion(arr);
            s += "\nСтандартное отклонение: " + StandartDeviation(arr);
            s += "\nДисперсия выборки: " + Dispersion(arr);
            s += "\nЭксцесс: " + Excess(arr);
            s += "\nАсимметричность: " + Asymmetry(arr);
            s += "\nИнтервал: " + Interval(arr);
            s += "\nМинимум: " + Min(arr);
            s += "\nМаксимум: " + Max(arr);
            s += "\nСчет: " + arr.Length;
            return s;
        }
        /// <summary>
        /// Нормирование с помощью максимального и минимального элементов
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double[] Rationing_MaxMin(double[] arr)
        {
            double MM = Max(arr) - Min(arr);
            var s = arr.Select(x => x / MM).ToArray();
            return s;
        }
        /// <summary>
        /// Нормирование с помощью дисперсии
        /// </summary>
        /// <param name="arr"></param>
        public static void Rationing_Dispersive(double[][] arr)
        {

        }
        /// <summary>
        /// Вывод массива
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string Output(double[] arr)
        {
            string s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                s+=string.Format("{0:F2} ", arr[i]);
            }
            s += "\n";
            return s;
        }
        /// <summary>
        /// Получение массива выборки данных из строки
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns></returns>
        public static double[] GetSample(string source)
        {
            string[] s = source.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Replace('.', ',')).ToArray();
            double[] arr = new double[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                arr[i] = double.Parse(s[i]);
            }

            return arr;
        }
        /// <summary>
        /// Вывод массива массивов
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string Output(double[][] arr)
        {
            string s = "";
            for(int i = 0; i < arr.Length; i++)
            {
                double[] buf = arr[i];
                for (int j = 0; j < buf.Length; j++) s += buf[j] + " ";
                s += "\n";
            }
            return s;
        }
        /// <summary>
        /// Получение строки из определенного столбца в заданном диапазоне
        /// </summary>
        /// <param name="Colum">Столбец для считывания</param>
        /// <param name="Start">Начало интервала</param>
        /// <param name="End">Конец интервала</param>
        /// <returns></returns>
        public static string OpenExcel(string Colum, int Start, int End)
        {
            string s = "";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ObjWorkbook = ObjExcel.Workbooks.Open(ofd.FileName, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Microsoft.Office.Interop.Excel.Worksheet objworksheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkbook.Sheets[1];
                for (int i = Start; i < End+1; i++)
                {
                    Microsoft.Office.Interop.Excel.Range range = objworksheet.get_Range(Colum + i.ToString(),Colum + i.ToString());
                    s += range.Text + "\r\n";
                }
                ObjExcel.Quit();
            }
            return s;
        }
        /// <summary>
        /// Получение значения функции Лапласа
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static double GetValueLaplasFunction(double argument)
        {
            double value = 0;

            string s = "";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ObjWorkbook = ObjExcel.Workbooks.Open(ofd.FileName, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Microsoft.Office.Interop.Excel.Worksheet objworksheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkbook.Sheets[1];
                for (int i = 1; i < 501; i++)
                {
                    Microsoft.Office.Interop.Excel.Range range = objworksheet.get_Range("A" + i.ToString(), "A" + i.ToString());
                    if(double.Parse(range.Text)==argument)
                    {
                        range = objworksheet.get_Range("B" + i.ToString(), "B" + i.ToString());
                        value = double.Parse(range.Text);
                    }
                }
                ObjExcel.Quit();
            }
            return value;
        }
    }
}
