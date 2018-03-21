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

        public double GetResult()
        {
            switch (Operation)
            {
                case "+":
                    Result = Result + SecondNumber;

                    break;
                case "-":
                    Result = Result - SecondNumber;

                    break;
                case "/":
                    if (First == true)
                    {
                        First = false;
                    }
                    else
                    {
                        Result = Result / SecondNumber;
                        First = true;
                    }

                    break;
                case "*":
                    if (First == true)
                    {
                        First = false;
                    }
                    else
                    {
                        Result = Result * SecondNumber;
                        First = true;
                    }
                    break;
                case "^":
                    if (First == true)
                    {
                        First = false;
                    }
                    else
                    {
                        Result = Math.Pow((Result), SecondNumber);
                        OnlyFirstnumber = true;
                        First = true;
                    }
                    break;
                case "sin":
                    Result = Math.Sin(Result);
                    OnlyFirstnumber = true;
                    break;
                case "cos":
                    Result = Math.Cos(Result);
                    OnlyFirstnumber = true;
                    break;
                case "tan":
                    Result = Math.Tan(Result);
                    OnlyFirstnumber = true;
                    break;
                case "ctg":
                    Result = 1 / (Math.Tan(Result));
                    OnlyFirstnumber = true;
                    break;
                case "ln":
                    Result = Math.Log(Result);
                    OnlyFirstnumber = true;
                    break;
                case "log":
                    Result = Math.Log10(Result);
                    OnlyFirstnumber = true;
                    break;
                case "x!":
                    double a = 1;
                    for (int i = 1; i <= Result; i++)
                    {
                        a = a * i;
                    }
                    Result = a;
                    OnlyFirstnumber = true;
                    Operation = "факториал от ";
                    break;
                case "x^2":
                    Result = Math.Pow((Result), 2);
                    OnlyFirstnumber = true;
                    Operation = "квадрат от ";
                    break;
                case "√":
                    Result = Math.Sqrt(Result);
                    OnlyFirstnumber = true;
                    break;

                case "1/x":
                    Result = 1 / (Result);
                    OnlyFirstnumber = true;
                    Operation = "1/";
                    break;
            }
            return Result;
        }

        public double GetResultM()
        {
            switch(Operation)
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
