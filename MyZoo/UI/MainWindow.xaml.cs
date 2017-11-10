using MyZoo.DAL;
using MyZoo.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyZoo.Migrations;
using MyZoo.UI;

namespace MyZoo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Configuration conf = new Configuration();
            //conf.doIt();
        }

        private void ButtonSearch_OnClick(object sender, RoutedEventArgs e)
        {
            UserSearchModel search = new UserSearchModel()
            {
                Discrimination = ComboBoxDiscrimination.Text,
                Type = ComboBoxType.Text,
                SpeciesSearch = TextBoxSearchSpecies.Text,
                Environment = ComboBoxEnvironment.Text
            };

            var access = new DataAccessZoo();
            var detailedAnimalList = access.GetDetailedAnimalListBySearchTerms(search);

            detailedAnimalList.AllowEdit = false;
            detailedAnimalList.AllowNew = false;

            ListBoxResultList.ItemsSource = detailedAnimalList;
        }

        private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
        {
            DataAccessZoo dataAccess = new DataAccessZoo();
            var animal = (AnimalDetailed)ListBoxResultList.SelectedItem;
            try
            {
                dataAccess.RemoveAnimal(animal.AnimalId);
                var list =  ListBoxResultList.ItemsSource as BindingList<AnimalDetailed>;
                list.Remove(animal);
                ClearAnimalDetailsLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearAnimalDetailsLabels()
        {
            LabelCountry.Content = "";
            LabelEnvironment.Content = "";
            LabelFather.Content = "";
            LabelMother.Content = "";
            LabelId.Content = "";
            LabelName.Content = "";
            LabelSex.Content = "";
            LabelWeight.Content = "";
            LabelType.Content = "";
            LabelSpecies.Content = "";
            //ListBoxParentsTo.ItemsSource = null;
        }

        private void ListBoxResultList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGridResultDetails. = ((AnimalDetailed)ListBoxResultList.SelectedItem)
            if (ListBoxResultList.SelectedItem != null && ListBoxResultList.SelectedItem is AnimalDetailed)
            {
                var animal = (AnimalDetailed)ListBoxResultList.SelectedItem;

                LabelId.Content = animal.AnimalId;
                LabelName.Content = animal.Name;
                LabelSex.Content = animal.Sex;
                LabelWeight.Content = animal.WeightInKilogram;
                LabelCountry.Content = animal.CountryOfOrigin;
                LabelSpecies.Content = animal.Species;
                LabelType.Content = animal.Type;
                LabelEnvironment.Content = animal.Environment;
                LabelMother.Content = animal.Mother;
                LabelFather.Content = animal.Father;
                //ListBoxParentsTo.ItemsSource = animal.ChildList;
            }

        }

        

        private void ButtonEditAnimal_OnClick(object sender, RoutedEventArgs e)
        {
            var animal = (AnimalDetailed) ListBoxResultList.SelectedItem;
            if (animal != null)
            {
                AddOrEditAnimal addOrEdit = new AddOrEditAnimal(animal);
                addOrEdit.Show();
            }
            else
            {
                MessageBox.Show("Inget djur är markerat för redigering.");
            }
        }

        private void MenuItemAddAnimals_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrEditAnimal addOrEdit = new AddOrEditAnimal();
            addOrEdit.Show();
        }

        private void MenuItemMakeVetAppointment_OnClick(object sender, RoutedEventArgs e)
        {
            MakeVetAppointment booking = new MakeVetAppointment();
            booking.Show();
        }

        private void MenuItemHandlePatients_OnClick(object sender, RoutedEventArgs e)
        {
            VetHandlePatient vet = new VetHandlePatient();
            vet.Show();
        }

        private void MenuItemQuit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
