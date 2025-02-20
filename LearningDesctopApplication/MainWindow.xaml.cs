using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearningDesctopApplication
{

    public partial class MainWindow : Window
    {
        private string _input = "";
        private double num1 = 0;
        private double num2 = 0;
        private char operation;
        private bool isNewOperation = false;
        private string nic;

        public MainWindow(string userNic)
        {
            InitializeComponent();
            nic = userNic;
        }

        private void btn0_Click_1(object sender, RoutedEventArgs e)
        {
            AppendNumber("0");
            LoggingUtility.LogButtonPress(nic, "0");
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("1");
            LoggingUtility.LogButtonPress(nic, "1");
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("2");
            LoggingUtility.LogButtonPress(nic, "2");
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("3");
            LoggingUtility.LogButtonPress(nic, "3");
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("4");
            LoggingUtility.LogButtonPress(nic, "4");
        }
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("5");
            LoggingUtility.LogButtonPress(nic, "5");
        }
        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("6");
            LoggingUtility.LogButtonPress(nic, "6");
        }
        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("7");
            LoggingUtility.LogButtonPress(nic, "7");
        }
        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("8");
            LoggingUtility.LogButtonPress(nic, "8");
        }
        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            AppendNumber("9");
            LoggingUtility.LogButtonPress(nic, "9");
        }
        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            AppendDot();
            LoggingUtility.LogButtonPress(nic, "Dot");
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
            LoggingUtility.LogButtonPress(nic, "Clear");
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            SetOperation('+');
            LoggingUtility.LogButtonPress(nic, "Plus");
        }
        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            SetOperation('-');
            LoggingUtility.LogButtonPress(nic, "Reduce");
        }
        private void btnMultiple_Click(object sender, RoutedEventArgs e)
        {
            SetOperation('*');
            LoggingUtility.LogButtonPress(nic, "Multiple");
        }
        private void btnDevide_Click(object sender, RoutedEventArgs e)
        {
            SetOperation('/');
            LoggingUtility.LogButtonPress(nic, "Devide");
        }

        private void btnSum_Click(object sender, RoutedEventArgs e)
        {
            CalculateResult();
            LoggingUtility.LogButtonPress(nic, "Sum");
        }


        private void AppendNumber(string number)
        {
            try
            {
                if (isNewOperation)
                {
                    _input = "";
                    isNewOperation = false;
                }
                _input += number;
                txtDisplay.Text = _input;
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Invalid Input");
                throw;
            }

        }

        private void AppendDot()
        {
            try
            {
                if (!_input.Contains("."))
                {
                    _input += ".";
                    txtDisplay.Text = _input;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Invalid Input");
                throw;
            }

        }

        private void SetOperation(char op)
        {
            try
            {
                if (double.TryParse(_input, out num1))
                {
                    operation = op;
                    _input = "";
                    txtDisplay.Text = $"{num1} {operation}";
                    isNewOperation = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Invalid Input");
                throw;
            }

        }

        private void CalculateResult()
        {
            try
            {
                if (double.TryParse(_input, out num2))
                {
                    double result = 0;
                    bool isValid = true;
                    switch (operation)
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
                            {
                                txtDisplay.Text = "Error: Division by Zero";
                                LoggingUtility.LogResult(nic, "Error: Division by Zero");
                                isValid = false;
                            }
                            break;
                        default:
                            txtDisplay.Text = "Error: Invalid Operation";
                            LoggingUtility.LogResult(nic, "Error: Invalid Operation");
                            isValid = false;
                            break;
                    }
                    if (isValid)
                    {
                        txtDisplay.Text = $"{num1} {operation} {num2} = {result}";
                        LoggingUtility.LogResult(nic, $"{num1} {operation} {num2} = {result}");
                        _input = result.ToString();
                        isNewOperation = true;
                    }
                }
                else
                {
                    txtDisplay.Text = "Error: Invalid Input";
                    LoggingUtility.LogResult(nic, "Error: Invalid Input");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Invalid Input");
                throw;
            }

        }
        private void ClearInput()
        {
            _input = "";
            num1 = 0;
            num2 = 0;
            txtDisplay.Text = "0";
            isNewOperation = false;
        }
        private void ReadLogging_Click(object sender, RoutedEventArgs e)
        {
            ReadLoggingWindow readLoggingWindow = new ReadLoggingWindow();
            readLoggingWindow.ShowDialog();
        }
    }
}