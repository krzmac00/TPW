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

namespace Program_przykładowy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public decimal liczba;
        public decimal potega;
        public static decimal Potega(decimal liczba, decimal potega)
        {
            if (potega == 0 | liczba == 1)
            {
                return 1;
            }
            decimal wynik = liczba;
            if (potega < 0)
            {
                potega *= -1;
                wynik = 1 / liczba;
                for (int i = 1; i < potega; i++)
                {
                    wynik /= liczba;
                }
            }
            else
            {
                wynik = liczba;
                for (int i = 1; i < potega; i++)
                {
                    wynik *= liczba;
                }
            }
            return wynik;
        }

        public MainWindow()
        {
            InitializeComponent();
            przycisk_oblicz.FontSize = 12;
        }

        private void Wcisnieto_Przycisk1(object sender, RoutedEventArgs e)
        {
            przycisk_oblicz.Foreground = przycisk_oblicz.IsMouseOver ? new SolidColorBrush(Colors.DarkRed) : new SolidColorBrush(Colors.Blue);
        }

        private void Przycisk_Oblicz_Click(object sender, RoutedEventArgs e)
        {
            if (Decimal.TryParse(pole_tekstowe_liczba.Text, out liczba) & Decimal.TryParse(pole_tekstowe_potega.Text, out potega))
            {
                blad.Text = "";
                decimal wynik_potegowania = Potega(liczba, potega);
                wynik_txt.Text = "= " + wynik_potegowania.ToString();

            }
            else
            {
                wynik_txt.Text = "= ?";
                blad.Text = "BŁĄD! Podaj liczbę dziesiętną";
            }
        }
    }
}
