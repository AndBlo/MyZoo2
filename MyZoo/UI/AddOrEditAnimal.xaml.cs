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
    /// Interaction logic for EditAnimal.xaml
    /// </summary>
    public partial class AddOrEditAnimal : Window
    {
        public AddOrEditAnimal()
        {
            InitializeComponent();
        }

        public AddOrEditAnimal(AnimalDetailed animal)
        {
            InitializeComponent();

            TextBoxCountry.Text = animal.CountryOfOrigin;
            TextBoxEnvironment.Text = animal.Environment;
            TextBoxFather.Text = animal.Father;
            TextBoxMother.Text = animal.Mother;
            TextBoxName.Text = animal.Name;
            TextBoxSex.Text = animal.Sex;
            TextBoxSpecies.Text = animal.Species;
            TextBoxType.Text = animal.Type;
            TextBoxWeight.Text = animal.WeightInKilogram.ToString();
            LabelId.Content = animal.AnimalId;
        }

        private void ButtonSaveChanges_OnClick(object sender, RoutedEventArgs e)
        {
            DataAccessZoo access = new DataAccessZoo();

            bool isValid = ValidateRequiredFields(out string message);
            if (!isValid)
            {
                MessageBox.Show(message);
                return;
            }

            AnimalDetailed animal = new AnimalDetailed()
            {
                AnimalId = LabelId.Content is int id ? id : 0,
                Name = TextBoxName.Text,
                Sex = TextBoxSex.Text,
                WeightInKilogram = float.Parse(TextBoxWeight.Text),
                CountryOfOrigin = TextBoxCountry.Text,
                Species = TextBoxSpecies.Text,
                Environment = TextBoxEnvironment.Text,
                Type = TextBoxType.Text,
                Mother = TextBoxMother.Text,
                Father = TextBoxFather.Text
            };


            try
            {
                access.AddOrUpdateAnimal(animal);
                MessageBox.Show("Ändringar sparade!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n\nIngen ändring sparades.");
            }
        }

        private bool ValidateRequiredFields(out string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Alla obligatoriska fält är inte ifyllda:");

            bool isValid = true;

            if (TextBoxName.Text == "")
            {
                sb.AppendLine("* Namn");
                isValid = false;
            }
            if (TextBoxWeight.Text == "")
            {
                sb.AppendLine("* Vikt");
                isValid = false;
            }
            if (TextBoxSex.Text == "")
            {
                sb.AppendLine("* Kön");
                isValid = false;
            }
            if (TextBoxSpecies.Text == "")
            {
                sb.AppendLine("* Art");
                isValid = false;
            }
            if (TextBoxEnvironment.Text == "")
            {
                sb.AppendLine("* Miljö");
                isValid = false;
            }
            if (TextBoxType.Text == "")
            {
                sb.AppendLine("* Typ");
                isValid = false;
            }
            if (TextBoxCountry.Text == "")
            {
                sb.AppendLine("* Land");
                isValid = false;
            }

            message = sb.ToString();

            return isValid;
        }
    }
}
