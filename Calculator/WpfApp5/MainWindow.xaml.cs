using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private string previousOperation = string.Empty;
        private string currentNumber = string.Empty;
        private char? lastOperator = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void NumClick(object sender, RoutedEventArgs e)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    string number = button.Content.ToString();
                    if (currentNumber == "0" && number != "0")
                    {
                        currentNumber = number;
                    }
                    else
                    {
                        currentNumber += number;
                    }
                    NumberTB.Text = currentNumber;
                }
            }

            private void FLoatClick(object sender, RoutedEventArgs e)
            {
                if (!currentNumber.Contains("."))
                {
                    currentNumber += ".";
                    NumberTB.Text = currentNumber;
                }
            }

            private void OperClick(object sender, RoutedEventArgs e)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    if (currentNumber != string.Empty)
                    {
                        if (previousOperation != string.Empty)
                        {
                            Calculate();
                        }
                    previousOperation = currentNumber + " " + button.Content.ToString();
                        PredZadTB.Text = previousOperation;
                        currentNumber = string.Empty;
                        lastOperator = button.Content.ToString()[0];
                    }
                }
            }

            private void CalcClick(object sender, RoutedEventArgs e)
            {
                if (currentNumber != string.Empty && previousOperation != string.Empty)
                {
                    Calculate();
                    previousOperation = string.Empty;
                }
            }

            private void Calculate()
            {
                double num1 = double.Parse(previousOperation.Split(' ')[0]);
                double num2 = double.Parse(currentNumber);
                double result = 0;

                switch (lastOperator)
                {
                    case '+':
                        result = num1 + num2;
                        break;
                    case '-':
                        result = num1 - num2;
                        break;
                    case '*':
                        result = num1 * num2;
                        break;
                    case '/':
                        if (num2 != 0)
                            result = num1 / num2;
                        else
                            MessageBox.Show("Ошибка: Деление на ноль.");
                        break;
                }

                NumberTB.Text = result.ToString();
                previousOperation = string.Empty;
                currentNumber = result.ToString();
            }

            private void CleanClick(object sender, RoutedEventArgs e)
            {
                currentNumber = string.Empty;
                NumberTB.Text = currentNumber;
            }

            private void ClearAllClick(object sender, RoutedEventArgs e)
            {
                currentNumber = string.Empty;
                previousOperation = string.Empty;
                NumberTB.Text = currentNumber;
                PredZadTB.Text = previousOperation;
            }

            private void DeleteLastNumClick(object sender, RoutedEventArgs e)
            {
                if (currentNumber.Length > 0)
                {
                    currentNumber = currentNumber.Remove(currentNumber.Length - 1);
                    NumberTB.Text = currentNumber;
                }
            }
        }
    }