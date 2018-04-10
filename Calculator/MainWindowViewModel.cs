﻿using System;
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
        string textShow;
        bool operationClicked = false;
        public MainWindowViewModel()
        {

            //#region Реализация команд
            ShowText = new Command((param) =>
            {
                textShow += CommandParameter;
                Text = textShow;
                if (!operationClicked)
                {
                    try
                    {
                        _model.ResultM = double.Parse(Text);
                    }
                    catch { }
                }
                if (operationClicked)
                    try
                    {
                        _model.SecondNumber = double.Parse(Text);
                    }
                    catch { }

            }, (param) => true);
            ////Реализация команды для операторов
            GetOperation = new Command((param) =>
            {
                textShow += CommandParameter;
                TextMem += textShow;
                textShow = "";
                Text = "";

            }, (param) => true);
            ////Реализация команды для операторов "M..."
            GetOperationM = new Command((param) =>
            {
                _model.Operation = CommandParameter;
                operationClicked = true;
                _model.ResultM = _model.GetResultM();
                textShow = "";
                Text = "";
            }, (param) => true);
            ////реализация команды для "="
            ShowResult = new Command((param) =>
            {
                TextMem += textShow;
                _model.Result = _model.Calculate(TextMem);
                Text = Convert.ToString(_model.Result);
            }, (param) => true);

            //реализация команды для "MR"
            ShowResultM = new Command((param) =>
            {
                _model.OnlyFirstnumber = false;
                _model.ResultM = _model.GetResultM();
                textShow = Convert.ToString(_model.ResultM);
                Text = textShow;
            }, (param) => true);
            //Реализация команды для сброса C
            Clear = new Command((param) =>
            {
                _model.Result = 0;
                _model.SecondNumber = 0;
                TextMem = "";
                Text = "";
                textShow = "";
                operationClicked = false;
            }, (param) => true);
            //Реализация команды для сброса CЕ

            ClearMem = new Command((param) =>
            {
                _model.Result = 0;
                _model.SecondNumber = 0;
                Text = "";
                textShow = "";
                operationClicked = false;
            }, (param) => true);
            //Реализация команды для сброса Del
            ClearDel = new Command((param) =>
            {
                try
                {
                    int i = Text.Length - 1;
                    Text = Text.Remove(i);
                    textShow = "";
                }
                catch { };
            }, (param) => true);
            //Реализация команды для MC
            ClearM = new Command((param) =>
            {
                _model.ResultM = 0;
                _model.SecondNumber = 0;
                operationClicked = false;
            }, (param) => true);

            //Реализация команды для "." и преобразование в double
            Point = new Command((param) =>
            {
                Text += CommandParameter;
                if (!operationClicked)
                {

                }

            }, (param) =>
            {
                if (Text != null && !Text.Contains(","))
                    return true;
                else return false;
            });

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
        string textMem;
        public string TextMem
        {
            get
            {
                return textMem;
            }
            set
            {
                textMem = value;
                OnPropertyChanged();
                Point.InvokeCanExecChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string Name = "") { if (PropertyChanged != null) { PropertyChanged.Invoke(this, new PropertyChangedEventArgs(Name)); } }

        #region Для корректной работы команд
        public class Command : ICommand
        {
            public event EventHandler CanExecuteChanged;
            Action<object> _execute;
            Predicate<object> __canexecute;

            public Command(Action<object> execute, Predicate<object> canexecute)
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

        public Command ShowText
        { get; set; }
        /// <summary>
        /// Получить нажатую арифметическую операцию
        /// </summary>
        public Command GetOperation
        { get; set; }
        public Command GetOperationM
        { get; set; }
        public Command ShowResult
        { get; set; }
        public Command ShowResultM
        { get; set; }
        public Command Clear
        { get; set; }
        public Command ClearMem
        { get; set; }
        public Command ClearDel
        { get; set; }
        public Command ClearM
        { get; set; }
        public Command Point
        { get; set; }
        public static string CommandParameter { get; private set; }
    }
}

