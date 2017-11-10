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
    /// Interaction logic for VetHandlePatient.xaml
    /// </summary>
    public partial class VetHandlePatient : Window
    {
        public VetHandlePatient()
        {
            InitializeComponent();
            DataAccessZoo dataAccess = new DataAccessZoo();
            var veterinaryList = dataAccess.GetVeterinaryList();
            ComboBoxVeterinarians.ItemsSource = veterinaryList;
        }

        private void ComboBoxVeterinarians_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxVeterinarians.SelectedItem != null)
            {
                DataAccessZoo dataAccess = new DataAccessZoo();
                var veterinary = (Veterinary)ComboBoxVeterinarians.SelectedItem;
                var bookingList = dataAccess.GetBookingByVeterinaryIdList(veterinary.Id);
                ListBoxBookings.ItemsSource = bookingList;
            }
        }

        private void ListBoxBookings_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxBookings.SelectedItem != null)
            {
                DataAccessZoo dataAccess = new DataAccessZoo();
                var booking = (AnimalBooking)ListBoxBookings.SelectedItem;
                var journalEntryList = dataAccess.GetJournalEntries(booking.AnimalId);
            }
        }

        private void ButtonPrescribeMedication_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonAddJournalEntry_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonRemoveJournalEntry_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonRemoveBookingAndCloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ListBoxBookings_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataAccessZoo dataAccess = new DataAccessZoo();
            var booking = (AnimalBooking)ListBoxBookings.SelectedItem;
            var journalEntryList = dataAccess.GetJournalEntries(booking.AnimalId);
        }
    }
}
