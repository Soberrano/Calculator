using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string command;
        string cureFunc; //текущая функция
        Model _model = new Model();
        Rpn _rpn = new Rpn();

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string CurBtn = (String)(sender as Button).CommandParameter;
            Rpn._ForOneOperation = CurBtn;
            command = CurBtn; // Получаем нажатую кнопку
            textBox.Text += command; // Записываем то, что было нажато в textBox
            //}
        }
        /// <summary>
        /// Метод обработки кнопки "="
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (command != null)
                {
                    command = null;
                    cureFunc = textBoxMem.Text + textBox.Text; // Заполняем текущую функцию
                    textBoxMem.Text = cureFunc; // выводим текущую функцию на отображение 
                    _model.Result = _rpn.Calculate(cureFunc); // Получаем ответ
                    textBox.Text = _model.Result.ToString(); // Записываем ответ
                }
            }
            catch { }
        }
        /// <summary>
        /// Метод для подсчёта выражений, имеющих два числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForTwo_Click(object sender, RoutedEventArgs e)
        {
            command = (String)(sender as Button).CommandParameter;
            textBoxMem.Text += textBox.Text += command;
            textBox.Text = "";
        }

        /// <summary>
        /// Метод для подсчёта выражений, имеющих одно число
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForOne_Click(object sender, RoutedEventArgs e)
        {
            command = (String)(sender as Button).CommandParameter;
            Rpn._ForOneOperation = command;
            textBoxMem.Text = textBox.Text += command;
            textBox.Text = "";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {

            switch ((sender as Button).Content)
            {
                case "C":
                    {
                        textBox.Text = "";
                        textBoxMem.Text = "";
                        command = null;
                        cureFunc = null;
                        break;
                    }
                case "CE":
                    {
                        textBox.Text = "";
                        command = null;
                        break;
                    }
                case "Del":
                    {
                        try
                        {
                            int i = textBox.Text.Length - 1;
                            textBox.Text = textBox.Text.Remove(i);
                            command = null;
                        }
                        catch { };
                        break;
                    }
            }

        }
        private void MOperation_Click(object sender, RoutedEventArgs e)
        {
            
            string CurBtn = (String)(sender as Button).CommandParameter;
            try
            {
                double Num = Double.Parse(textBox.Text);
                _model.GetResultM(CurBtn, Num);
                textBox.Clear();
            }
            catch
            {
                textBox.Text = Model.ResultM.ToString();
            }
            
        }
    }
}

