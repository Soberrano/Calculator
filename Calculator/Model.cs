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
        public string Operation { get; set; }
        public bool OnlyFirstnumber { get; set; }
        public double Resulting
        {

            get
            {
                switch (Operation)
                {
                    case "+":
                        Result = FirstNumber + SecondNumber;

                        break;
                    case "-":
                        Result = FirstNumber - SecondNumber;

                        break;
                    case "/":
                        Result = FirstNumber / SecondNumber;

                        break;
                    case "*":
                        Result = FirstNumber * SecondNumber;
                        break;
                    case "^":
                        Result = Math.Pow((FirstNumber), SecondNumber);
                        OnlyFirstnumber = true;
                        break;
                    case "sin":
                        Result = Math.Sin(FirstNumber);
                        OnlyFirstnumber = true;
                        break;
                    case "cos":
                        Result = Math.Cos(FirstNumber);
                        OnlyFirstnumber = true;
                        break;
                    case "tan":
                        Result = Math.Tan(FirstNumber);
                        OnlyFirstnumber = true;
                        break;
                    case "ctg":
                        Result = 1 / (Math.Tan(FirstNumber));
                        OnlyFirstnumber = true;
                        break;
                    case "ln":
                        Result = Math.Log(FirstNumber);
                        OnlyFirstnumber = true;
                        break;
                    case "log":
                        Result = Math.Log10(FirstNumber);
                        OnlyFirstnumber = true;
                        break;
                    case "x!":
                        double a = 1;
                        for (int i = 1; i <= FirstNumber; i++)
                        {
                            a = a * i;
                        }
                        Result = a;
                        OnlyFirstnumber = true;
                        Operation = "факториал от ";
                        break;
                    case "x^2":
                        Result = Math.Pow((FirstNumber), 2);
                        OnlyFirstnumber = true;
                        Operation = "квадрат от ";
                        break;
                    case "√":
                        Result = Math.Sqrt(FirstNumber);
                        OnlyFirstnumber = true;
                        break;

                    case "1/x":
                        Result = 1 / (FirstNumber);
                        OnlyFirstnumber = true;
                        Operation = "1/";
                        break;
                }
                return Result;
            }
        }
    }
}
