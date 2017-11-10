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
using System.Windows.Shapes;
using MyZoo.DAL;
using MyZoo.Model;

namespace MyZoo.UI
{
    /// <summary>
    /// Interaction logic for MakeVetAppointment.xaml
    /// </summary>
    public partial class MakeVetAppointment : Window
    {
        public MakeVetAppointment()
        {
            InitializeComponent();
            DataAccessZoo dataAccess = new DataAccessZoo();
            var list = dataAccess.GetVeterinaryList();
            ComboBoxVeterinarians.ItemsSource = list;
        }

        private void ButtonMakeBooking_OnClick(object sender, RoutedEventArgs e)
        {
            var date = GetSelectedDateTime();
            var vet = (Veterinary)ComboBoxVeterinarians.SelectedItem;
            var animal = (AnimalSimple)ListBoxNameResult.SelectedItem;
            var booking = new AnimalBooking()
            {
                AnimalId = animal.AnimalId,
                AnimalName = animal.Name,
                DateTime = date,
                VeterinaryId = vet.Id,
                VeterinaryName = vet.Name
            };

            DataAccessZoo dataAccess = new DataAccessZoo();
            try
            {
                dataAccess.AddBooking(booking);
                MessageBox.Show("Bokningen är inlagd!");
                UpdateCurrentBookingsListBox();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private DateTime GetSelectedDateTime()
        {
            var dateTime = (DateTime)CalendarVetBookingDate.SelectedDate;
            var split = TextBoxVetBookingsTime.Text.Split(':');
            dateTime = dateTime.AddHours(double.Parse(split[0]));
            dateTime = dateTime.AddMinutes(double.Parse(split[1]));
            return dateTime;
        }

        private void ButtonNameSearch_OnClick(object sender, RoutedEventArgs e)
        {
            string name = TextBoxNameSearch.Text;
            DataAccessZoo dataAccess = new DataAccessZoo();
            var list = dataAccess.GetSimpleAnimalByName(name);

            ListBoxNameResult.ItemsSource = list;
        }

        private void ListBoxNameResult_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCurrentBookingsListBox();
        }

        private void UpdateCurrentBookingsListBox()
        {
            var animal = (AnimalSimple)ListBoxNameResult.SelectedItem;
            DataAccessZoo dataAccess = new DataAccessZoo();
            var list = dataAccess.GetBookingByAnimalIdList(animal.AnimalId);
            ListBoxCurrentBookings.ItemsSource = list;
        }

        private void ButtonRemoveBooking_OnClick(object sender, RoutedEventArgs e)
        {
            var booking = (AnimalBooking)ListBoxCurrentBookings.SelectedItem;
            DataAccessZoo dataAccess = new DataAccessZoo();

            try
            {
                dataAccess.RemoveBooking(booking.BookingId);
                MessageBox.Show("Bokningen borttagen!");
                UpdateCurrentBookingsListBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
