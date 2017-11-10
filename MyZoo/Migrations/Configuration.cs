using System.Collections.Generic;
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

            context.Families.AddOrUpdate(f => f.FamilyId,
                new Family()
                {
                    AnimalChild = bearChildBjorne,
                    AnimalFather = bearFatherSture,
                    AnimalMother = bearMotherPascha
                });
            context.Families.AddOrUpdate(f => f.FamilyId,
                new Family()
                {
                    AnimalChild = parrotChildSvea,
                    AnimalMother = parrotMotherDoris,
                    AnimalFather = parrotFatherGreger
                });
            context.Families.AddOrUpdate(f => f.FamilyId,
                new Family()
                {
                    AnimalChild = sealChildSara,
                    AnimalMother = sealMotherBerta,
                    AnimalFather = sealFatherRoger
                });

            var vetKurt = new Veterinarian()
            {
                Namn = "Kurt Wallin"
            };

            var vetSaida = new Veterinarian()
            {
                Namn = "Saida Broberg"
            };

            var vetKarin = new Veterinarian()
            {
                Namn = "Karin Andersson"
            };

            var booking1Time = new DateTime(2017, 12, 13, 15, 30, 0);

            var booking2Time = new DateTime(2017, 11, 20, 13, 0, 0);

            var booking3Time = new DateTime(2017, 11, 15, 10, 30, 0);

            var booking1 = new Booking()
            {
                Animal = bearChildBjorne,
                Veterinarian = vetKurt,
                DateTime = booking1Time
            };
            var booking2 = new Booking()
            {
                Animal = parrotChildSvea,
                Veterinarian = vetSaida,
                DateTime = booking2Time
            };
            var booking3 = new Booking()
            {
                Animal = sealFatherRoger,
                Veterinarian = vetKarin,
                DateTime = booking3Time
            };

            context.Bookings.AddOrUpdate(b => b.AnimalId,
                booking1,
                booking2,
                booking3
                );



            Diagnosis worm = new Diagnosis()
            {
                Name = "Mask",
                Description = "En parasit som orsakar näringsbrist och magbesvär"
            };
            Diagnosis brokenLeg = new Diagnosis()
            {
                Name = "Brutet ben",
                Description = "Ett ben brutet på ett ställe. Orsakar inmobilitet och smärta."
            };

            Medication painkiller = new Medication()
            {
                Name = "Smärtstillande medicin"
            };

            Medication dewormingMedication = new Medication()
            {
                Name = "Avmaskningsmedel"
            };

            //Journal bearBjorneJournal = new Journal()
            //{
            //    Animal = context.Animals.Find(1)
            //};

            //Journal parrotSveaJournal = new Journal()
            //{
            //    Animal = context.Animals.Find(4)
            //};

            //Journal sealRogerJournal = new Journal()
            //{
            //    Animal = context.Animals.Find(7),

            //};


            JournalsDiagnos diagnoseJournalBjorne = new JournalsDiagnos()
            {
                Diagnosis = brokenLeg,
                Journal = new Journal()
                {
                    Animal = bearChildBjorne
                },
                Medications = {painkiller}
            };

            JournalsDiagnos diagnoseJournalSara = new JournalsDiagnos()
            {
                Diagnosis = worm,
                Journal = new Journal()
                {
                    Animal = sealChildSara
                },
                Medications = { dewormingMedication }
            };

            JournalsDiagnos diagnoseJournalRoger = new JournalsDiagnos()
            {
                Diagnosis = brokenLeg,
                Journal = new Journal()
                {
                    Animal = sealFatherRoger

                },
                Medications = { painkiller }
            };

            context.JournalsDiagnoses.AddOrUpdate(j => j.JournalDiagnoseId,
                diagnoseJournalRoger,
                diagnoseJournalBjorne,
                diagnoseJournalSara
            );
        }
    }
}
