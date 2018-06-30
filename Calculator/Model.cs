using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Model
    {
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public static double ResultM { get; set; } = 0;
        public double Result { get; set; }
        public bool OnlyFirstnumber { get; set; }

        private static Dictionary<char, Func<double, double, double>> _forTwoPairs = new Dictionary<char, Func<double, double, double>>()
        {
            { '+', (a,b) => a + b },
            { '-', (a,b) => a - b },
            { '*', (a,b) => a * b },
            { '/', (a,b) => a / b },
            { '^', (a,b) => double.Parse(Math.Pow(double.Parse(a.ToString()), double.Parse(b.ToString())).ToString()) },
            { '√', (a,b) => b * Math.Sqrt(a) },
            { '!', (a,b) =>  { double o = 1; for (int h = 1; h <= a; h++) { o = o * h; } return b * o; } },
        };

        private static Dictionary<string, Func<double, double>> _forOnePairs = new Dictionary<string, Func<double, double>>()
        {
            { "log", a =>  (Math.Log(a)) },
            { "sin", a =>  (Math.Sin(a)) },
            { "1/", a =>  (1 / a)},
            { "^2", a =>  (Math.Pow(double.Parse(a.ToString()), 2)) },
            { "10^", a =>  (Math.Pow(10, double.Parse(a.ToString()))) },
            { "ln", a =>  (Math.Log10(a)) },
            { "cos", a =>  (Math.Cos(a)) },
            { "tan", a =>  (Math.Tan(a)) },
            { "ctg", a =>  (1 / (Math.Tan(a))) },
            { "√", a =>  Math.Sqrt(a) },
            { "!", a =>  { double o = 1; for (int h = 1; h <= a; h++) { o = o * h; } return o; } },
        };
        /// <summary>
        /// Обеспечивает вычисление для операций с двумя переменными
        /// </summary>
        /// <param name="firstv">Певрое число</param>
        /// <param name="secondv">Второе число</param>
        /// <param name="command">Операция</param>
        /// <returns>Результат</returns>
        public static double ForTwo(double firstv, double secondv, char command) => _forTwoPairs[command](firstv, secondv);
        /// <summary>
        ///  Обеспечивает вычисление для операций с одной переменной
        /// </summary>
        /// <param name="firstv">Переменная</param>
        /// <param name="command">Операция</param>
        /// <returns>Результат</returns>
        public static double ForOne(double firstv, string command) => _forOnePairs[command](firstv);


        public double? GetResultM(string Operation, double Num)
        {
            switch (Operation)
            {
                case "M+":
                    ResultM = ResultM + Num;
                    return null;
                case "M-":
                    ResultM -= Num;
                    return null;
                case "MC":
                    ResultM = 0;
                    return null;
                case "MR":
                    return ResultM;
                default: return null;
            }
        }
    }
}

