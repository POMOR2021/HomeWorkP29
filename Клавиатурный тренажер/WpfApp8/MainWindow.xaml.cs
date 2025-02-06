using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainer
{
    public partial class MainWindow : Window
    {
        private string targetText = "";
        private int currentIndex = 0;
        private int errors = 0;
        private Stopwatch stopwatch = new Stopwatch();
        private Random random = new Random();
        private readonly Brush[] keyColors = { Brushes.LightBlue, Brushes.LightGreen, Brushes.LightCoral, Brushes.LightPink, Brushes.LightSalmon, Brushes.LightSkyBlue };

        public MainWindow()
        {
            InitializeComponent();
            GenerateKeyboard();
        }

        // Генерация клавиатуры с разными цветами клавиш
        private void GenerateKeyboard()
        {
            string keys = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            var grid = UniformGrid;

            foreach (char key in keys)
            {
                Border border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Background = keyColors[random.Next(keyColors.Length)], // Разноцветные клавиши
                    Child = new TextBlock
                    {
                        Text = key.ToString(),
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    },
                    Margin = new Thickness(2),
                    Padding = new Thickness(10),
                    Tag = key.ToString()
                };
                grid.Children.Add(border);
            }
        }

        // Генерация случайной строки
        private void GenerateText()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            if (CheckCaseSensitive.IsChecked == true) chars += chars.ToUpper();
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < (int)SliderLength.Value; i++)
                sb.Append(chars[rnd.Next(chars.Length)]);

            targetText = sb.ToString();
            TextToType.Text = targetText;
            TextInput.Text = "";
            currentIndex = 0;
            errors = 0;
            stopwatch.Restart();
        }

        // Запуск тренировки
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            GenerateText();
            BtnStart.IsEnabled = false;
            BtnStop.IsEnabled = true;
        }

        // Остановка тренировки
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            BtnStart.IsEnabled = true;
            BtnStop.IsEnabled = false;
            stopwatch.Stop();
        }

        // Обработка нажатий клавиш
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!BtnStop.IsEnabled) return; // Тренировка не запущена

            char inputChar = GetCharFromKey(e.Key);
            if (inputChar == '\0') return; // Игнорируем ненужные клавиши

            HighlightKey(e.Key, true);

            if (currentIndex < targetText.Length)
            {
                if (inputChar == targetText[currentIndex])
                {
                    TextInput.Text += inputChar;
                    currentIndex++;
                }
                else
                {
                    errors++;
                }
            }

            // Завершение тренировки
            if (currentIndex >= targetText.Length)
            {
                stopwatch.Stop();
                double speed = currentIndex / (stopwatch.Elapsed.TotalMinutes + 0.01);
                TextStats.Text = $"Скорость: {speed:F1} зн/мин | Ошибки: {errors}";
                BtnStart.IsEnabled = true;
                BtnStop.IsEnabled = false;
            }
        }

        // Обработка отпускания клавиш
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            HighlightKey(e.Key, false);
        }

        // Получение символа с учетом регистра
        private char GetCharFromKey(Key key)
        {
            string normal = "abcdefghijklmnopqrstuvwxyz1234567890";
            string shifted = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()";

            int index = normal.IndexOf(key.ToString().ToLower());
            if (index >= 0)
                return CheckCaseSensitive.IsChecked == true ? shifted[index] : normal[index];

            return '\0';
        }

        // Подсветка клавиши
        private void HighlightKey(Key key, bool isPressed)
        {
            var grid = UniformGrid;
            foreach (Border border in grid.Children)
            {
                var textBlock = (TextBlock)border.Child;
                if (textBlock.Text.Equals(key.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    border.Background = isPressed ? Brushes.Yellow : keyColors[random.Next(keyColors.Length)];
                }
            }
        }
    }
}