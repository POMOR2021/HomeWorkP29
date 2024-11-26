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

namespace WpfApp4
{ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string note = Zamet.Text.Trim();

            if (!string.IsNullOrEmpty(note))
            {
                ZametLB.Items.Add(note);
                Zamet.Clear();
            }
        }

        private void RButton_Click(object sender, RoutedEventArgs e)
        { 
            if (ZametLB.SelectedItem != null)
            {
                ZametLB.Items.Remove(ZametLB.SelectedItem);
            }
        }
    }
}

