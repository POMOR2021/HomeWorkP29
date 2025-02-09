using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp9
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateControlsState();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            UpdateControlsState();
        }

        private void UpdateControlsState()
        {
            bool isValid = ValidateRange();
            BoldButton.IsEnabled = isValid;
            ItalicButton.IsEnabled = isValid;
            UnderlineButton.IsEnabled = isValid;
            ClearButton.IsEnabled = isValid;
            FontSizeComboBox.IsEnabled = isValid;
            ColorComboBox.IsEnabled = isValid;
        }

        private bool ValidateRange()
        {
            if (int.TryParse(StartIndexTextBox.Text, out int start) &&
                int.TryParse(EndIndexTextBox.Text, out int end))
            {
                TextRange textRange = new TextRange(MainRichTextBox.Document.ContentStart,
                                                  MainRichTextBox.Document.ContentEnd);
                return start <= end && start >= 0 && end < textRange.Text.Length;
            }
            return false;
        }

        private TextRange GetSelectedRange()
        {
            TextPointer start = MainRichTextBox.Document.ContentStart.GetPositionAtOffset(
                int.Parse(StartIndexTextBox.Text));
            TextPointer end = MainRichTextBox.Document.ContentStart.GetPositionAtOffset(
                int.Parse(EndIndexTextBox.Text) + 1);
            return new TextRange(start, end);
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateRange())
            {
                TextRange range = GetSelectedRange();
                range.ApplyPropertyValue(TextElement.FontWeightProperty,
                    range.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold)
                        ? FontWeights.Normal
                        : FontWeights.Bold);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateRange())
            {
                TextRange range = GetSelectedRange();
                range.ApplyPropertyValue(TextElement.FontStyleProperty,
                    range.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic)
                        ? FontStyles.Normal
                        : FontStyles.Italic);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateRange())
            {
                TextRange range = GetSelectedRange();
                TextDecorationCollection decorations = (TextDecorationCollection)range.GetPropertyValue(Inline.TextDecorationsProperty);
                if (decorations != null && decorations.Contains(TextDecorations.Underline[0]))
                    range.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateRange())
            {
                TextRange range = GetSelectedRange();
                range.ClearAllProperties();
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ValidateRange() && FontSizeComboBox.SelectedItem != null)
            {
                TextRange range = GetSelectedRange();
                double fontSize = double.Parse((FontSizeComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                range.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ValidateRange() && ColorComboBox.SelectedItem != null)
            {
                TextRange range = GetSelectedRange();
                string colorName = (ColorComboBox.SelectedItem as ComboBoxItem).Content.ToString();
                Color color = (Color)ColorConverter.ConvertFromString(colorName);
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
            }
        }
    }
}