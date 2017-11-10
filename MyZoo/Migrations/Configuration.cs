using System.Data.Entity.Validation;
using System.Windows;
using MyZoo.DataContext;

namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyZoo.DataContext.ZooDataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public void doIt()
        {
            using (var context = new ZooDataBaseContext())
            {
                try
                {
                    Seed(context);
                }
                catch (DbEntityValidationException e)
                {
                    MessageBox.Show(e.Message);
                    //throw;
                }
            }
        }

        protected override void Seed(MyZoo.DataContext.ZooDataBaseContext context)
        {
            Country sweden = new Country()
            {
                Name = "Sverige"
            };
            Country russia = new Country()
            {
                Name = "Ryssland"
            };
            Country zimbabwe = new Country()
            {
                Name = "Zimbabwe"
            };
            Country norway = new Country()
            {
                Name = "Norge"
            };

            DataContext.Type carnivore = new DataContext.Type()
            {
                Name = "Köttätare"
            };
            DataContext.Type herbivore = new DataContext.Type()
            {
                Name = "Växtätare"
            };

            DataContext.Environment ground = new DataContext.Environment()
            {
                Name = "Mark"
            };
            DataContext.Environment tree = new DataContext.Environment()
            {
                Name = "Träd"
            };
            DataContext.Environment water = new DataContext.Environment()
            {
                Name = "Vatten"
            };

            Species bear = new Species()
            {
                Name = "Björn",
                Environment = ground,
                Type = carnivore
            };
            Species amazonParrot = new Species()
            {
                Name = "Amazonpapegoja",
                Environment = tree,
                Type = herbivore
            };
            Species seal = new Species()
            {
                Name = "Knubbsäl",
                Environment = water,
                Type = carnivore
            };

            Animal bearMotherPascha = new Animal()
            {
                Name = "Pascha",
                Weight = 145,
                Sex = "Hona",
                Species = bear,
                Country = russia
            };
            Animal bearFatherSture = new Animal()
            {
                Name = "Sture",
                Weight = 230,
                Sex = "Hane",
                Species = bear,
                Country = sweden
            };
            Animal bearChildBjorne = new Animal()
            {
                Name = "Björne",
                Weight = 95,
                Sex = "Hane",
                Species = bear,
                Country = sweden
            };

            Animal parrotMotherDoris = new Animal()
            {
                Name = "Doris",
                Weight = (float)1.1,
                Sex = "Hona",
                Species = amazonParrot,
                Country = sweden
            };
            Animal parrotFatherGreger = new Animal()
            {
                Name = "Greger",
                Sex = "Hane",
                Weight = (float)1.5,
                Species = amazonParrot,
                Country = sweden
            };
            Animal parrotChildSvea = new Animal()
            {
                Name = "Svea",
                Sex = "Hona",
                Weight = (float)0.5,
                Species = amazonParrot,
                Country = sweden
            };

            Animal sealMotherBerta = new Animal()
            {
                Name = "Berta",
                Weight = 53,
                Sex = "Hona",
                Species = seal,
                Country = norway
            };
            Animal sealFatherRoger = new Animal()
            {
                Name = "Roger",
                Weight = 125,
                Sex = "Hane",
                Species = seal,
                Country = sweden
            };
            Animal sealChildSara = new Animal()
            {
                Name = "Sara",
                Weight = 35,
                Sex = "Hona",
                Species = seal,
                Country = sweden
            };

            Family parrotFamily = new Family()
            {
                FamilyId = 1,
                AnimalChild = parrotChildSvea,
                AnimalMother = parrotMotherDoris,
                AnimalFather = parrotFatherGreger
            };
            Family bearFamily = new Family()
            {
                FamilyId = 2,
                AnimalChild = bearChildBjorne,
                AnimalFather = bearFatherSture,
                AnimalMother = bearMotherPascha
            };
            Family sealFamily = new Family()
            {
                FamilyId = 3,
                AnimalChild = sealChildSara,
                AnimalMother = sealMotherBerta,
                AnimalFather = sealFatherRoger
            };

            context.Bookings.AddOrUpdate(b => b.DateTime,
                new Booking()
                {
                    Animal = bearChildBjorne,
                    Veterinarian = new Veterinarian()
                    {
                        Namn = "Kurt Wallin"
                    },
                    DateTime = new DateTime(2017, 12, 13, 15, 30, 0)
                },
                new Booking()
                {
                    Animal = sealChildSara,
                    Veterinarian = new Veterinarian()
                    {
                        Namn = "Saida Broberg"
                    },
                    DateTime = new DateTime(2017, 11, 20, 13, 0, 0)
                },
            new Booking()
            {
                Animal = parrotFatherGreger,
                Veterinarian = new Veterinarian()
                {
                    Namn = "Karin Andersson"
                },
                DateTime = new DateTime(2017, 11, 15, 10, 30, 0)
            }
            );

            context.Families.AddOrUpdate(f => f.FamilyId,
                bearFamily,
                parrotFamily,
                sealFamily
                );


            //Diagnosis worm = new Diagnosis()
            //{
            //    Name = "Mask",
            //    Description = "En parasit som orsakar näringsbrist och magbesvär"
            //};
            //Diagnosis brokenLeg = new Diagnosis()
            //{
            //    Name = "Brutet ben",
            //    Description = "Ett ben brutet på ett ställe. Orsakar inmobilitet och smärta."
            //};

            //Medication painkiller = new Medication()
            //{
            //    Name = "Smärtstillande medicin"
            //};

            //Medication dewormingMedication = new Medication()
            //{
            //    Name = "Avmaskningsmedel"
            //};

            //Journal bearChildJournal = new Journal()
            //{
            //    Animal = bearChildBjorne,
            //    Diagnoses = { new Diagnosis()
            //    {
            //        Name = "Mask",
            //        Description = "En parasit som orsakar näringsbrist och magbesvär",
            //        Medications = {dewormingMedication, painkiller}
            //    }}
            //};

            //Journal parrotMotherJournal = new Journal()
            //{
            //    Animal = parrotMotherDoris,
            //    Diagnoses = {new Diagnosis()
            //    {
            //        Name = "Brutet ben",
            //        Description = "Ett ben brutet på ett ställe. Orsakar inmobilitet och smärta.",
            //        Medications = {painkiller}
            //    } }
            //};

            //context.Journals.AddOrUpdate(j => j.AnimalId,
            //    bearChildJournal,
            //    parrotMotherJournal
            //    );

            //JournalsDiagnos connection1 = new JournalsDiagnos()
            //{
            //    Diagnosis = worm,
            //    Journal = bearChildJournal,
            //    Medications = {dewormingMedication, painkiller}
            //};

            //JournalsDiagnos connection2 = new JournalsDiagnos()
            //{
            //    Diagnosis = brokenLeg,
            //    Journal = parrotMotherJournal,
            //    Medications = {painkiller}
            //};

            //context.JournalsDiagnoses.AddOrUpdate(jd => jd.DiagnoseId,
            //connection1,
            //connection2
            //);


        }
    }
}
