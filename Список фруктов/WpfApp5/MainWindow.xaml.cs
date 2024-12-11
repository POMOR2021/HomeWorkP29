using System;
using System.Collections.Generic;
using System.Linq;
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

public class Fruit
{
    public string Name { get; set; }
    public string Color { get; set; }
    public string Origin { get; set; }

    public override string ToString()
    {
        return Name;
    }
}

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private List<Fruit> fruits;

        public MainWindow()
        {
            InitializeComponent();
            LoadFruits();
            Fruits.ItemsSource = fruits;
        }

        private void LoadFruits()
        {
            fruits = new List<Fruit>
            {
                new Fruit { Name = "Яблоко", Color = "Красный", Origin = "Сад" },
                new Fruit { Name = "Банан", Color = "Желтый", Origin = "Тропики" },
                new Fruit { Name = "Апельсин", Color = "Оранжевый", Origin = "Цитрусплантации" },
                new Fruit { Name = "Груша", Color = "Зеленый", Origin = "Сад" },
                new Fruit { Name = "Киви", Color = "Коричневый", Origin = "Новая Зеландия" }
            };
        }

        private void FruitsLBSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Fruits.SelectedItem is Fruit selectFruit)
            {
                var details = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Название", selectFruit.Name),
                    new KeyValuePair<string, string>("Цвет", selectFruit.Color),
                    new KeyValuePair<string, string>("Происхождение", selectFruit.Origin)
                };

                PropLV.ItemsSource = details;
            }
            else
            {
                PropLV.ItemsSource = null;
            }
        }

        private void CleanClick(object sender, RoutedEventArgs e)
        {
            Fruits.SelectedItem = null;
            PropLV.ItemsSource = null;
        }
    }
}
