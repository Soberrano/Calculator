using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        Model _model = new Model();
        bool resultClicked = false;
        bool operationClicked = false;
        public MainWindowViewModel()
        {
            #region Реализация команд
            ShowText = new MyCommand((param) =>
            {

                if (!resultClicked)
                {
                    Text += CommandParameter;
                    if (!operationClicked)
                        _model.FirstNumber = double.Parse(Text);
                    if (operationClicked)
                        _model.SecondNumber = double.Parse(Text);
                }
                else//если нажато "=" => выведи результат
                    Text = Convert.ToString(_model.Resulting);

                operationClicked = false;
            }, (param) => true);
            //Реализация команды для операторов
            GetOperation = new MyCommand((param) =>
            {
                _model.Operation = CommandParameter;
                operationClicked = true;
                resultClicked = false;
                Text = "";
            }, (param) => true);
            //реализация команды для "="
            ShowResult = new MyCommand((param) =>
            {
                _model.OnlyFirstnumber = false;
                resultClicked = true;
                _model.Result = _model.Resulting;
                _model.FirstNumber = _model.Result;
                Text = Convert.ToString(_model.Result);
            }, (param) => true);

            //Реализация команды для сброса
            Clear = new MyCommand((param) =>
            {

                _model.FirstNumber = 0;
                _model.SecondNumber = 0;
                resultClicked = false;
                Text = "";
                operationClicked = false;
            }, (param) => true);

            //Реализация команды для "." и преобразование в double
            Point = new MyCommand((param) =>
            {
                Text += CommandParameter;
                if (!operationClicked)
                    _model.FirstNumber = double.Parse(Text);
            }, (param) =>
            {
                if (Text != null && !Text.Contains(","))
                    return true;
                else return false;
            });
            #endregion
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged();
                Point.InvokeCanExecChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string Name = "") { if (PropertyChanged != null) { PropertyChanged.Invoke(this, new PropertyChangedEventArgs(Name)); } }

        #region Для корректной работы команд
        public class MyCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;
            Action<object> _execute;
            Predicate<object> __canexecute;

            public MyCommand(Action<object> execute, Predicate<object> canexecute)
            {
                _execute = execute;
                __canexecute = canexecute;
            }

            public bool CanExecute(object parameter)
            {
                return __canexecute.Invoke(parameter);
            }

            public void Execute(object parameter)
            {
                CommandParameter = (string)parameter;
                _execute.Invoke(parameter);
            }

            public void InvokeCanExecChanged()
            {
                if (CanExecuteChanged != null)
                { CanExecuteChanged.Invoke(this, new EventArgs()); }
            }
        }
        #endregion

        public MyCommand ShowText
        {
            get; set;
        }
        /// <summary>
        /// Получить нажатую арифметическую операцию
        /// </summary>
        public MyCommand GetOperation
        { get; set; }

        public MyCommand ShowResult
        { get; set; }

        public MyCommand Clear
        { get; set; }

        public MyCommand Point
        { get; set; }

        public static string CommandParameter { get; private set; }
    }
}

