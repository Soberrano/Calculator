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
        public double Result { get; set; }
        public double ResultM { get; set; }
        public string Operation { get; set; }
        public bool OnlyFirstnumber { get; set; }
        public double Num { get; set; }
        public bool First { get; set; } = true;

        //public double GetResult()
        //{

        //    switch (Operation)
        //    {
        //        case "+":
        //            Result = Result + SecondNumber;

        //            break;
        //        case "-":
        //            Result = Result - SecondNumber;

        //            break;
        //        case "/":
        //            if (First == true)
        //            {
        //                First = false;
        //            }
        //            else
        //            {
        //                Result = Result / SecondNumber;
        //                First = true;
        //            }

        //            break;
        //        case "*":
        //            if (First == true)
        //            {
        //                First = false;
        //            }
        //            else
        //            {
        //                Result = Result * SecondNumber;
        //                First = true;
        //            }
        //            break;
        //        case "^":
        //            if (First == true)
        //            {
        //                First = false;
        //            }
        //            else
        //            {
        //                Result = Math.Pow((Result), SecondNumber);
        //                OnlyFirstnumber = true;
        //                First = true;
        //            }
        //            break;
        //        case "sin":
        //            Result = Math.Sin(Result);
        //            OnlyFirstnumber = true;
        //            break;
        //        case "cos":
        //            Result = Math.Cos(Result);
        //            OnlyFirstnumber = true;
        //            break;
        //        case "tan":
        //            Result = Math.Tan(Result);
        //            OnlyFirstnumber = true;
        //            break;
        //        case "ctg":
        //            Result = 1 / (Math.Tan(Result));
        //            OnlyFirstnumber = true;
        //            break;
        //        case "ln":
        //            Result = Math.Log(Result);
        //            OnlyFirstnumber = true;
        //            break;
        //        case "log":
        //            Result = Math.Log10(Result);
        //            OnlyFirstnumber = true;
        //            break;
        //        case "x!":
        //            double a = 1;
        //            for (int i = 1; i <= Result; i++)
        //            {
        //                a = a * i;
        //            }
        //            Result = a;
        //            OnlyFirstnumber = true;
        //            Operation = "факториал от ";
        //            break;
        //        case "x^2":
        //            Result = Math.Pow((Result), 2);
        //            OnlyFirstnumber = true;
        //            Operation = "квадрат от ";
        //            break;
        //        case "√":
        //            Result = Math.Sqrt(Result);
        //            OnlyFirstnumber = true;
        //            break;

        //        case "1/x":
        //            Result = 1 / (Result);
        //            OnlyFirstnumber = true;
        //            Operation = "1/";
        //            break;
        //    }
        //    return Result;
        //}


        #region new era
        /// <summary>
        /// Метод возвращает true, если проверяемый символ - разделитель ("пробел" или "равно")
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }
        /// <summary>
        ///  Метод возвращает true, если проверяемый символ - оператор
        /// </summary>
        /// <param name="с"></param>
        /// <returns></returns>
        static private bool IsOperator(char с)
        {
            if (("+-/*^()!sctklg√nqed".IndexOf(с) != -1))
                return true;
            return false;
        }

        /// <summary>
        /// Метод возвращает приоритет оператора
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case 'd': return 4;//1/x
                case '^': return 5;
                case 'e': return 5;//10^x
                case 'q': return 5;//x^2
                case '!': return 5;
                case 's': return 6;
                case 'c': return 6;
                case 't': return 6;
                case 'k': return 6;//ctg
                case 'l': return 6;//log
                case 'n': return 6;//ln
                case '√': return 7;
                default: return 8;
            }
        }

        //"Входной" метод класса
        public double Calculate(string input)
        {
            string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
            double result = Counting(output); //Решаем полученное выражение
            return result; //Возвращаем результат
        }

        static private string GetExpression(string input)
        {
            string output = string.Empty; //Строка для хранения выражения
            Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов

            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {
                //Разделители пропускаем
                if (IsDelimeter(input[i]))
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считываем все число
                if (Char.IsDigit(input[i])) //Если цифра
                {
                    //Читаем до разделителя или оператора, что бы получить число
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i]; //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                }

                //Если символ - оператор
                if (IsOperator(input[i])) //Если оператор
                {
                    if (input[i] == '(') //Если символ - открывающая скобка
                        operStack.Push(input[i]); //Записываем её в стек
                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                        operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                    }
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }

        static private double Counting(string input)
        {
            double result = 0; //Результат
            Stack<double> temp = new Stack<double>(); //Dhtvtyysq стек для решения

            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a)); //Записываем в стек
                    i--;
                }
                else if (IsOperator(input[i])) //Если символ - оператор
                {
                    //Берем два последних значения из стека
                    double a = temp.Pop();
                    double b;
                    try
                    {
                        if (input[i] == '+' || input[i] == '-' || input[i] == '*' || input[i] == '/' || input[i] == '^')
                        {
                            b = temp.Pop();
                            switch (input[i]) //И производим над ними действие, согласно оператору
                            {
                                case '+': result = b + a; break;
                                case '-': result = b - a; break;
                                case '*': result = b * a; break;
                                case '/': result = b / a; break;
                                case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                            }
                        }
                        switch (input[i])
                        {
                            case '!':
                                {
                                    double o = 1;
                                    for (int h = 1; h <= a; h++)
                                    {
                                        o = o * h;
                                    }
                                    return o;
                                }
                            case 's': result = (Math.Sin(a)); break;
                            case 'q': result = (Math.Pow(double.Parse(a.ToString()), 2)); break;
                            case 'e': result = (Math.Pow(10, double.Parse(a.ToString()))); break;
                            case 'l': result = (Math.Log(a)); break;
                            case 'n': result = (Math.Log10(a)); break;
                            case 'c': result = (Math.Cos(a)); break;
                            case 't': result = (Math.Tan(a)); break;
                            case 'k': result = 1 / (Math.Tan(a)); break;//ctg
                            case '√': result = Math.Sqrt(a); break;
                        }

                    }
                    catch { };

                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }
        #endregion








        public double GetResultM()
        {
            switch (Operation)
            {
                case "M+":
                    ResultM = ResultM + SecondNumber;
                    break;
                case "M-":
                    ResultM -= FirstNumber;
                    OnlyFirstnumber = true;
                    break;
            }
            return ResultM;
        }
    }
}

