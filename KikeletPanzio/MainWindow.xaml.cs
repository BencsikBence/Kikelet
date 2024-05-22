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
                string numberOfPeople = (Ferohely.SelectedItem as ComboBoxItem).Content.ToString();
                bool isVIP = VIP.IsChecked == true;

                int osszeg = CalculateAmount(numberOfPeople, isVIP);
                Osszeg.Content = osszeg.ToString() + " Ft";
            }
        }

        private int CalculateAmount(string numberOfPeople, bool isVIP)
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

            if (isVIP)
            {
                
                egysegar = (int)(egysegar * 0.97);
            }

            return egysegar;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(Nevek.Text) || string.IsNullOrWhiteSpace(Email.Text) ||
                Szuletes.SelectedDate == null || Szobaszam.SelectedItem == null || Ferohely.SelectedItem == null ||
                FoglalasKezdete.SelectedDate == null || FoglalasVege.SelectedDate == null)
            {
                MessageBox.Show("Kérem, töltse ki az összes mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double id = MySlider.Value;
            string name = Nevek.Text;
            string email = Email.Text;
            bool isVIP = VIP.IsChecked == true;
            DateTime? birthDate = Szuletes.SelectedDate;
            string roomNumber = (Szobaszam.SelectedItem as ComboBoxItem)?.Content.ToString();
            string numberOfPeople = (Ferohely.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime? startDate = FoglalasKezdete.SelectedDate;
            DateTime? endDate = FoglalasVege.SelectedDate;

            int amount = CalculateAmount(numberOfPeople, isVIP);

            var reservation = new Reservation
            {
                Id = id,
                Nev = name,
                Email = email,
                VIP = isVIP,
                Szuletesnap = birthDate,
                Szobaszam = roomNumber,
                Fo = numberOfPeople,
                Osszeg = amount,
                FoglalasIdo = DateTime.Now, 
                Kezdet = startDate, // Foglalás kezdeti időpontja
                Vege = endDate // Foglalás záró időpontja
            };

            Reservations.Add(reservation);

            Osszeg.Content = amount.ToString() + " Ft";
        }

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private void ResetFields()
        {
            MySlider.Value = 0;
            MyLabel.Content = string.Empty;
            Nevek.Clear();
            Email.Clear();
            VIP.IsChecked = false;
            Szuletes.SelectedDate = null;
            Szobaszam.SelectedItem = null;
            Ferohely.SelectedItem = null;
            Osszeg.Content = string.Empty;
            FoglalasKezdete.SelectedDate = null;
            FoglalasVege.SelectedDate = null;
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
            public DateTime FoglalasIdo { get; set; } 
            public DateTime? Kezdet { get; set; } // Foglalás kezdeti időpontja
            public DateTime? Vege { get; set; } // Foglalás záró időpontja
        }
    }
}
