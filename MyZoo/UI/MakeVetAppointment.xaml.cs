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
using MyZoo.Exceptions;
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
            try
            {
                var date = GetSelectedDateTime();
                var vet = (Veterinary) ComboBoxVeterinarians.SelectedItem;
                var animal = (AnimalSimple) ListBoxNameResult.SelectedItem;

                ValidateRequriedBookingFields(date, vet, animal);

                var booking = new AnimalBooking()
                {
                    AnimalId = animal.AnimalId,
                    AnimalName = animal.Name,
                    DateTime = date,
                    VeterinaryId = vet.Id,
                    VeterinaryName = vet.Name
                };

                DataAccessZoo dataAccess = new DataAccessZoo();

                dataAccess.AddBooking(booking);
                MessageBox.Show("Bokningen är inlagd!");
                UpdateCurrentBookingsListBox();

            }
            catch (AddingDuplicateException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (InvalidBookingDateTimeException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (RequiredFieldsNullException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ValidateRequriedBookingFields(DateTime date, Veterinary vet, AnimalSimple animal)
        {
            if (date < DateTime.Now)
            {
                throw new InvalidBookingDateTimeException("Den angivna tiden har passerat");
            }
            if (vet == null)
            {
                throw new RequiredFieldsNullException("Ingen veterinär har valts för bokningen");
            }
            if (animal == null)
            {
                throw new RequiredFieldsNullException("Inget djur har valts för bokningen");
            }
        }

        private DateTime GetSelectedDateTime()
        {
            DateTime? dateTime = null;
            if (CalendarVetBookingDate.SelectedDate != null)
            {
                dateTime = (DateTime)CalendarVetBookingDate.SelectedDate;
            }
            else
            {
                throw new InvalidBookingDateTimeException("Ett datum måste väljas från kalendern");
            }

            var split = TextBoxVetBookingsTime.Text.Split(':');

            if (double.TryParse(split[0], out double hour) && double.TryParse(split[1], out double minute))
            {
                dateTime = dateTime.Value.AddHours(hour);
                dateTime = dateTime.Value.AddMinutes(minute);
            }
            else
            {
                throw new InvalidBookingDateTimeException("Den angivna tiden är i fel format.\nhh:mm");
            }

            return (DateTime)dateTime;
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
