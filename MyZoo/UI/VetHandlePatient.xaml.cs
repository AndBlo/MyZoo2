using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for VetHandlePatient.xaml
    /// </summary>
    public partial class VetHandlePatient : Window
    {
        public VetHandlePatient()
        {
            InitializeComponent();
            DataAccessZoo dataAccess = new DataAccessZoo();
            ListBoxPrescribedMedication.ItemsSource = new BindingList<Medication>();

            PopulateComboBoxVeterinarians(dataAccess);
            PopulateComboBoxAnimals(dataAccess);
            PopulateOrUpdateComboBoxDiagnoses(dataAccess);
            PopulateOrUpdateComboBoxMedication(dataAccess);
        }

        private void PopulateComboBoxVeterinarians(DataAccessZoo dataAccess)
        {
            var veterinaryList = dataAccess.GetVeterinaryList();
            ComboBoxVeterinarians.ItemsSource = veterinaryList;
        }

        private void PopulateComboBoxAnimals(DataAccessZoo dataAccess)
        {
            var animalList = dataAccess.GetSimpleAnimalList();
            ComboBoxCurrentPatient.ItemsSource = animalList;
        }

        private void PopulateOrUpdateComboBoxDiagnoses(DataAccessZoo dataAccess)
        {
            var diagnosisList = dataAccess.GetDiagnoses();
            ComboBoxDiagnoses.ItemsSource = diagnosisList;
        }

        private void PopulateOrUpdateComboBoxMedication(DataAccessZoo dataAccess)
        {
            var medicationList = dataAccess.GetMedications();
            ComboBoxMedications.ItemsSource = medicationList;
        }

        private void ComboBoxVeterinarians_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxVeterinarians.SelectedItem != null)
            {
                DataAccessZoo dataAccess = new DataAccessZoo();
                var veterinary = (Veterinary)ComboBoxVeterinarians.SelectedItem;
                PopulateOrUpdateBookingListByVeterinaryId(dataAccess, veterinary.Id);
            }
        }

        private void PopulateOrUpdateBookingListByVeterinaryId(DataAccessZoo dataAccess, int veterinaryId)
        {
            var bookingList = dataAccess.GetBookingListByVeterinaryId(veterinaryId);
            ListBoxBookings.ItemsSource = bookingList;
        }

        private void PopulateOrUpdateBookingListByAnimalId(DataAccessZoo dataAccess, int animalId)
        {
            var bookingList = dataAccess.GetBookingListByAnimalId(animalId);
            ListBoxBookings.ItemsSource = bookingList;
        }

        private void ListBoxBookings_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxBookings.SelectedItem != null)
            {
                DataAccessZoo dataAccess = new DataAccessZoo();
                var booking = (AnimalBooking)ListBoxBookings.SelectedItem;
                var animalList = ComboBoxCurrentPatient.ItemsSource as BindingList<AnimalSimple>;
                var currentAnimal = from animal in animalList
                                    where animal.AnimalId == booking.AnimalId
                                    select animal;

                ComboBoxCurrentPatient.SelectedItem = currentAnimal.Single();
                PopulateOrUpdateJournalEntryList(dataAccess, booking.AnimalId);
            }
        }

        private void PopulateOrUpdateJournalEntryList(DataAccessZoo dataAccess, int animalId)
        {
            var journalEntryList = dataAccess.GetJournalEntries(animalId);
            ListBoxJournalEntries.ItemsSource = journalEntryList;
        }

        private void ButtonPrescribeMedication_OnClick(object sender, RoutedEventArgs e)
        {
            DataAccessZoo dataAccess = new DataAccessZoo();
            var medication = (Medication)ComboBoxMedications.SelectedItem;

            var prescribedMedsList = (BindingList<Medication>)ListBoxPrescribedMedication.ItemsSource;
            prescribedMedsList.Add(medication);
            ListBoxPrescribedMedication.ItemsSource = prescribedMedsList;

            PopulateOrUpdateComboBoxMedication(dataAccess);

        }

        private void ButtonAddJournalEntry_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DataAccessZoo dataAccess = new DataAccessZoo();

                var animal = (AnimalSimple)ComboBoxCurrentPatient.SelectedItem;
                var diagnose = (Diagnosis)ComboBoxDiagnoses.SelectedItem;
                var medications = GetMedicationsAsNameStringList();
                var booking = (AnimalBooking) ListBoxBookings.SelectedItem;

                ValidateRequiredFields(booking, animal, diagnose, medications);

                var journalEntry = new JournalEntry()
                {
                    AnimalId = animal.AnimalId,
                    AnimalName = animal.Name,
                    JournalId = animal.AnimalId,
                    DiagnoseId = diagnose.DiagnosisId,
                    DiagnoseName = diagnose.Name,
                    DiagnoseDescription = diagnose.Description,
                    Medications = medications
                };


                dataAccess.AddJournalEntryToJournals(journalEntry);
                MessageBox.Show("Journalanteckning inlagd");
                PopulateOrUpdateJournalEntryList(dataAccess, animal.AnimalId);
                ClearDiagnoseAndMedicationFields();

            }
            catch (RequiredFieldsNullException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ValidateRequiredFields(AnimalBooking booking, AnimalSimple animal, Diagnosis diagnose, List<string> medications)
        {
            StringBuilder sb = new StringBuilder();
            if (booking == null)
            {
                throw new RequiredFieldsNullException("Fel. En bokning måste väljas för att ställa diagnos");
            }
            if (animal == null)
            {
                throw new RequiredFieldsNullException("Fel. Inget djur är valt");
            }
            if (diagnose == null)
            {
                throw new RequiredFieldsNullException("Fel. Ingen diagnos är vald");
            }
            if (medications.Count == 0)
            {
                throw new RequiredFieldsNullException("Fel. Ingen medicin är vald");
            }
        }

        private void ClearDiagnoseAndMedicationFields()
        {
            ComboBoxDiagnoses.SelectedItem = null;
            ComboBoxMedications.SelectedItem = null;
            LabelDiagnosisDescription.Text = "";
            ListBoxPrescribedMedication.ItemsSource = new BindingList<Medication>();

        }

        private List<string> GetMedicationsAsNameStringList()
        {
            var medicationEnumerable = (BindingList<Medication>)ListBoxPrescribedMedication.ItemsSource;
            return medicationEnumerable.Select(p => p.Name).ToList();
        }

        private void ButtonRemoveJournalEntry_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxJournalEntries.SelectedItem != null)
            {
                DataAccessZoo dataAccess = new DataAccessZoo();

                var journalEntry = (JournalEntry)ListBoxJournalEntries.SelectedItem;

                try
                {
                    dataAccess.RemoveJournalEntry(journalEntry);
                    PopulateOrUpdateJournalEntryList(dataAccess, journalEntry.AnimalId);
                }
                catch (DbObjectNotFoundException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                MessageBox.Show("Ingen journalanteckning är vald");
            }
        }

        private void ButtonRemoveBookingAndCloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxBookings.SelectedItem != null)
            {
                DataAccessZoo dataAccess = new DataAccessZoo();
                var booking = (AnimalBooking) ListBoxBookings.SelectedItem;

                try
                {
                    dataAccess.RemoveBooking(booking.BookingId);
                    MessageBox.Show("Mötet avslutat!");
                    this.Close();

                }
                catch (DbObjectNotFoundException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void ListBoxBookings_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataAccessZoo dataAccess = new DataAccessZoo();
            var booking = (AnimalBooking)ListBoxBookings.SelectedItem;
            var journalEntryList = dataAccess.GetJournalEntries(booking.AnimalId);
        }

        private void ComboBoxCurrentPatient_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataAccessZoo dataAccess = new DataAccessZoo();
            var animal = (AnimalSimple)ComboBoxCurrentPatient.SelectedItem;
            var journalEntryList = dataAccess.GetJournalEntries(animal.AnimalId);
            ListBoxJournalEntries.ItemsSource = journalEntryList;
            PopulateOrUpdateBookingListByAnimalId(dataAccess, animal.AnimalId);

        }

        private void ComboBoxDiagnoses_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxDiagnoses.SelectedItem != null)
            {
                DataAccessZoo dataAccess = new DataAccessZoo();
                var diagnosis = (Diagnosis)ComboBoxDiagnoses.SelectedItem;
                LabelDiagnosisDescription.Text = diagnosis.Description;
            }
        }

        private void ButtonAddNewDiagnosis_OnClick(object sender, RoutedEventArgs e)
        {
            DataAccessZoo dataAccess = new DataAccessZoo();

            var diagnosisName = TextBoxNewDiagnosis.Text;
            var diagnosisDescription = TextBoxDiagnoseDescription.Text;
            try
            {
                var diagnosis = dataAccess.AddOrUpdateAndGetDiagnosis(diagnosisName, diagnosisDescription);
                PopulateOrUpdateComboBoxDiagnoses(dataAccess);
                ComboBoxDiagnoses.Text = diagnosis.Name;
                LabelDiagnosisDescription.Text = diagnosis.Description;
            }
            catch (DbObjectNotFoundException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ButtonAddNewMedication_OnClick(object sender, RoutedEventArgs e)
        {
            DataAccessZoo dataAccess = new DataAccessZoo();

            var medicationName = TextBoxNewMedication.Text;
            try
            {
                var medication = dataAccess.AddOrUpdateAndGetMedication(medicationName);
                PopulateOrUpdateComboBoxMedication(dataAccess);
                ComboBoxMedications.Text = medication.Name;
            }
            catch (DbObjectNotFoundException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
