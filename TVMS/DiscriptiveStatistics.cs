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

        public static double[] Rationing_MaxMin(double[] arr)
        {
            double MM = Max(arr) - Min(arr);
            var s = arr.Select(x => x / MM).ToArray();
            return (double[])s;
        }
        /// <summary>
        /// Нормирование с помощью дисперсии
        /// </summary>
        /// <param name="arr"></param>
        public static void Rationing_Dispersive(double[][] arr)
        {

        }
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
    }
}
