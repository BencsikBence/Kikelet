using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KikeletPanzio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Reservation> Reservations { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Reservations = new ObservableCollection<Reservation>();
            DataGridReservations.ItemsSource = Reservations;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            if (Ferohely.SelectedItem != null)
            {
                int ferohely = int.Parse((Ferohely.SelectedItem as ComboBoxItem).Content.ToString());

                int egysegar = 0;

                switch (ferohely)
                {
                    case 2:
                        egysegar = 12000;
                        break;
                    case 3:
                        egysegar = 18000;
                        break;
                    case 4:
                        egysegar = 24000;
                        break;
                }

                int osszeg = egysegar;
                Osszeg.Content = osszeg.ToString() + " Ft";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            double id = MySlider.Value;
            string name = Nevek.Text;
            string email = Email.Text;
            bool isVIP = VIP.IsChecked == true;
            DateTime? birthDate = Szuletes.SelectedDate;
            string roomNumber = (Szobaszam.SelectedItem as ComboBoxItem)?.Content.ToString();
            string numberOfPeople = (Ferohely.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Calculate the payment amount
            int amount = CalculateAmount(numberOfPeople);

            var reservation = new Reservation
            {
                Id = id,
                Nev = name,
                Email = email,
                VIP = isVIP,
                Szuletesnap = birthDate,
                Szobaszam = roomNumber,
                Fo = numberOfPeople,
                Osszeg = amount
            };

            Reservations.Add(reservation);

            // Update the displayed amount
            Osszeg.Content = amount.ToString() + " Ft";
        }

        private int CalculateAmount(string numberOfPeople)
        {
            int ferohely = int.Parse(numberOfPeople);

            int egysegar = 0;

            switch (ferohely)
            {
                case 2:
                    egysegar = 12000;
                    break;
                case 3:
                    egysegar = 18000;
                    break;
                case 4:
                    egysegar = 24000;
                    break;
            }

            return egysegar;
        }

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            // Clear all fields or perform any cancellation logic if necessary
        }

        private void MySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MyLabel != null)
            {
                MyLabel.Content = MySlider.Value.ToString("F2");
            }
        }

        public class Reservation
        {
            public double Id { get; set; }
            public string Nev { get; set; }
            public string Email { get; set; }
            public bool VIP { get; set; }
            public DateTime? Szuletesnap { get; set; }
            public string Szobaszam { get; set; }
            public string Fo { get; set; }
            public int Osszeg { get; set; }
        }
    }
}
