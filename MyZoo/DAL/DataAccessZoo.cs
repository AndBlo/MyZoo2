using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.SqlServer.Server;
using MyZoo.DataContext;
using MyZoo.Model;

namespace MyZoo.DAL
{
    class DataAccessZoo
    {
        public DataAccessZoo()
        {

        }

        public BindingList<AnimalDetailed> GetDetailedAnimalListBySearchTerms(UserSearchModel search)
        {
            BindingList<AnimalDetailed> list = null;

            using (var db = new ZooDataBaseContext())
            {
                if (search.SpeciesSearch != "" || search.Environment != "--------" || search.Discrimination != "--------" || search.Type != "--------")
                {
                    var queryEnvironment = from a in db.Animals
                                           where a.Species.Name.ToLower().Contains(search.SpeciesSearch)
                                                 && a.Species.Environment.Name == (
                                                              search.Environment == "--------"
                                                                  ? a.Species.Environment.Name
                                                                  : search.Environment)
                                                 && a.Species.Type.Name == (
                                                        search.Type == "--------"
                                                            ? a.Species.Type.Name
                                                            : search.Type)
                                           select a;

                    if (search.Discrimination != "--------")
                    {
                        switch (search.Discrimination)
                        {
                            case "Förälder":
                                var queryDiscriminationParent = from animal in queryEnvironment
                                                                from family in db.Families
                                                                where animal.AnimalId == family.AnimalFather.AnimalId
                                                                      || animal.AnimalId == family.AnimalMother.AnimalId
                                                                join animalFamily in db.Families
                                                                    on animal.AnimalId equals animalFamily.ChildId into animalChildFamily
                                                                from af in animalChildFamily.DefaultIfEmpty()
                                                                select new AnimalDetailed()
                                                                {
                                                                    AnimalId = animal.AnimalId,
                                                                    CountryOfOrigin = animal.Country.Name,
                                                                    Environment = animal.Species.Environment.Name,
                                                                    Father = af.AnimalFather.Name,
                                                                    Mother = af.AnimalMother.Name,
                                                                    Name = animal.Name,
                                                                    Sex = animal.Sex,
                                                                    Species = animal.Species.Name,
                                                                    Type = animal.Species.Type.Name,
                                                                    WeightInKilogram = animal.Weight,
                                                                };
                                list = new BindingList<AnimalDetailed>(queryDiscriminationParent.ToList());
                                break;
                            case "Moder":
                                var queryDiscriminationMother = from animal in queryEnvironment
                                                                from family in db.Families
                                                                where animal.AnimalId == family.AnimalMother.AnimalId
                                                                join animalFamily in db.Families
                                                                    on animal.AnimalId equals animalFamily.ChildId into animalChildFamily
                                                                from af in animalChildFamily.DefaultIfEmpty()
                                                                select new AnimalDetailed()
                                                                {
                                                                    AnimalId = animal.AnimalId,
                                                                    CountryOfOrigin = animal.Country.Name,
                                                                    Environment = animal.Species.Environment.Name,
                                                                    Father = af.AnimalFather.Name,
                                                                    Mother = af.AnimalMother.Name,
                                                                    Name = animal.Name,
                                                                    Sex = animal.Sex,
                                                                    Species = animal.Species.Name,
                                                                    Type = animal.Species.Type.Name,
                                                                    WeightInKilogram = animal.Weight,
                                                                };
                                list = new BindingList<AnimalDetailed>(queryDiscriminationMother.ToList());
                                break;
                            case "Fader":
                                var queryDiscriminationFather = from animal in queryEnvironment
                                                                from family in db.Families
                                                                where animal.AnimalId == family.AnimalFather.AnimalId
                                                                join animalFamily in db.Families
                                                                    on animal.AnimalId equals animalFamily.ChildId into animalChildFamily
                                                                from af in animalChildFamily.DefaultIfEmpty()
                                                                select new AnimalDetailed()
                                                                {
                                                                    AnimalId = animal.AnimalId,
                                                                    CountryOfOrigin = animal.Country.Name,
                                                                    Environment = animal.Species.Environment.Name,
                                                                    Father = af.AnimalFather.Name,
                                                                    Mother = af.AnimalMother.Name,
                                                                    Name = animal.Name,
                                                                    Sex = animal.Sex,
                                                                    Species = animal.Species.Name,
                                                                    Type = animal.Species.Type.Name,
                                                                    WeightInKilogram = animal.Weight,
                                                                };
                                list = new BindingList<AnimalDetailed>(queryDiscriminationFather.ToList());
                                break;
                            case "Barn":
                                var queryDiscriminationChild = from animal in queryEnvironment
                                                               from family in db.Families
                                                               where animal.AnimalId == family.AnimalChild.AnimalId
                                                               join animalFamily in db.Families
                                                                   on animal.AnimalId equals animalFamily.ChildId into animalChildFamily
                                                               from af in animalChildFamily.DefaultIfEmpty()
                                                               select new AnimalDetailed()
                                                               {
                                                                   AnimalId = animal.AnimalId,
                                                                   CountryOfOrigin = animal.Country.Name,
                                                                   Environment = animal.Species.Environment.Name,
                                                                   Father = af.AnimalFather.Name,
                                                                   Mother = af.AnimalMother.Name,
                                                                   Name = animal.Name,
                                                                   Sex = animal.Sex,
                                                                   Species = animal.Species.Name,
                                                                   Type = animal.Species.Type.Name,
                                                                   WeightInKilogram = animal.Weight,
                                                               };
                                list = new BindingList<AnimalDetailed>(queryDiscriminationChild.ToList());
                                break;
                        }
                    }
                    else
                    {
                        var query = from animal in queryEnvironment
                                    join animalFamily in db.Families
                                    on animal.AnimalId equals animalFamily.ChildId into animalChildFamily
                                    from af in animalChildFamily.DefaultIfEmpty()
                                    select new AnimalDetailed()
                                    {
                                        AnimalId = animal.AnimalId,
                                        CountryOfOrigin = animal.Country.Name,
                                        Environment = animal.Species.Environment.Name,
                                        Father = af.AnimalFather.Name,
                                        Mother = af.AnimalMother.Name,
                                        Name = animal.Name,
                                        Sex = animal.Sex,
                                        Species = animal.Species.Name,
                                        Type = animal.Species.Type.Name,
                                        WeightInKilogram = animal.Weight,
                                    };

                        list = new BindingList<AnimalDetailed>(query.ToList());
                    }
                }
                else
                {
                    var query =
                        from animal in db.Animals
                        join family in db.Families
                            on animal.AnimalId equals family.ChildId into animalChildFamily
                        from af in animalChildFamily.DefaultIfEmpty()
                        select new AnimalDetailed()
                        {
                            AnimalId = animal.AnimalId,
                            CountryOfOrigin = animal.Country.Name,
                            Environment = animal.Species.Environment.Name,
                            Father = af.AnimalFather.Name,
                            Mother = af.AnimalMother.Name,
                            Name = animal.Name,
                            Sex = animal.Sex,
                            Species = animal.Species.Name,
                            Type = animal.Species.Type.Name,
                            WeightInKilogram = animal.Weight,
                        };

                    list = new BindingList<AnimalDetailed>(query.ToList());
                }

            }

            return list;
        }

        public BindingList<AnimalSimple> GetSimpleAnimalByName(string name)
        {
            BindingList<AnimalSimple> list = null;
            using (var db = new ZooDataBaseContext())
            {
                var query = from animal in db.Animals
                            where animal.Name.Contains(name)
                            select new AnimalSimple()
                            {
                                AnimalId = animal.AnimalId,
                                Name = animal.Name,
                                Species = animal.Species.Name
                            };
                list = new BindingList<AnimalSimple>(query.ToList());
            }

            return list;
        }

        public BindingList<Veterinary> GetVeterinaryList()
        {
            BindingList<Veterinary> list = null;
            using (var db = new ZooDataBaseContext())
            {
                var query = from vet in db.Veterinarians
                            select new Veterinary()
                            {
                                Id = vet.VeterinaryId,
                                Name = vet.Namn
                            };
                list = new BindingList<Veterinary>(query.ToList());
            }

            return list;
        }

        public void AddOrUpdateAnimal(AnimalDetailed animalDetailed)
        {
            using (var db = new ZooDataBaseContext())
            {
                Country country = GetOrCreateCountry(animalDetailed, db);
                Species species = GetOrCreateSpecies(animalDetailed, db);
                Animal animal = null;
                if (animalDetailed.AnimalId == 0)
                {
                    animal = new Animal()
                    {
                        Name = animalDetailed.Name,
                        Sex = animalDetailed.Sex,
                        Weight = animalDetailed.WeightInKilogram,
                        Country = country,
                        Species = species
                    };
                }
                else
                {
                    animal = GetAndAlterAnimal(animalDetailed, db, country, species);
                }

                Animal mother = GetOrCreateAnimalMother(animalDetailed, db, country, species);
                Animal father = GetOrCreateAnimalFather(animalDetailed, db, country, species);

                var family = db.Families.Where(f => f.AnimalChild.AnimalId == animalDetailed.AnimalId)
                    .Select(f => f)
                    .FirstOrDefault();
                if (family != null)
                {
                    family.AnimalFather = father;
                    family.AnimalMother = mother;
                    family.AnimalChild = animal;

                    db.Families.AddOrUpdate(f => f.FamilyId,
                        family
                        );
                }
                else if (family == null && (animalDetailed.Mother != "" || animalDetailed.Mother != ""))
                {
                    family = new Family()
                    {
                        AnimalChild = animal,
                        AnimalMother = mother,
                        AnimalFather = father
                    };

                    db.Families.AddOrUpdate(f => f.ChildId,
                        family
                    );
                }
                else
                {
                    db.Animals.AddOrUpdate(a => a.AnimalId,
                        animal
                        );
                }

                db.SaveChanges();

            }
        }

        private static Animal GetAndAlterAnimal(AnimalDetailed animalDetailed, ZooDataBaseContext db, Country country, Species species)
        {
            var animal = db.Animals.Find(animalDetailed.AnimalId);

            animal.Country = country;
            animal.Species = species;
            animal.Name = animalDetailed.Name;
            animal.Sex = animalDetailed.Sex;
            animal.Weight = animalDetailed.WeightInKilogram;
            return animal;
        }

        private static Animal GetOrCreateAnimalFather(AnimalDetailed animalDetailed, ZooDataBaseContext db, Country country, Species species)
        {
            var father = db.Families.Where(f => f.AnimalFather.Name == animalDetailed.Father)
                                .Select(f => f.AnimalFather).FirstOrDefault();
            var newFather = db.Animals.Where(a => a.Name == animalDetailed.Father)
                .Select(a => a).FirstOrDefault();
            if (father == null && newFather == null && animalDetailed.Father != "")
            {
                father = new Animal()
                {
                    Name = animalDetailed.Father,
                    Country = country,
                    Species = species,
                    Weight = 0,
                    Sex = "Hane"
                };

                return father;
            }
            if (father == null && newFather != null)
            {
                return newFather;
            }
            else
            {
                return father;
            }
        }

        private static Animal GetOrCreateAnimalMother(AnimalDetailed animalDetailed, ZooDataBaseContext db, Country country, Species species)
        {
            var mother = db.Families.Where(f => f.AnimalMother.Name == animalDetailed.Mother)
                                .Select(f => f.AnimalMother).FirstOrDefault();
            var newMother = db.Animals.Where(a => a.Name == animalDetailed.Mother)
                                .Select(a => a).FirstOrDefault();
            if (mother == null && newMother == null && animalDetailed.Mother != "")
            {
                mother = new Animal()
                {
                    Name = animalDetailed.Mother,
                    Country = country,
                    Species = species,
                    Weight = 0,
                    Sex = "Hona"
                };

                return mother;
            }
            if (mother == null && newMother != null)
            {
                return newMother;
            }
            else
            {
                return mother;
            }
        }

        private static Species GetOrCreateSpecies(AnimalDetailed animalDetailed, ZooDataBaseContext db)
        {
            var environment = db.Environments.Where(e => e.Name == animalDetailed.Environment).Select(e => e).FirstOrDefault();
            var type = db.Types.Where(t => t.Name == animalDetailed.Type).Select(t => t).FirstOrDefault();

            var species = db.Species.Where(s => s.Name == animalDetailed.Species).Select(s => s).FirstOrDefault();
            if (species == null)
            {
                species = new Species()
                {
                    Name = animalDetailed.Species,
                    Environment = environment,
                    Type = type
                };
            }
            else
            {
                if (species.Environment.Name != animalDetailed.Environment)
                {
                    throw new InvalidOperationException($"Arten \"{species.Name}\" kan inte ha {animalDetailed.Environment} som miljö.");
                }
                if (species.Type.Name != animalDetailed.Type)
                {
                    throw new InvalidOperationException($"Arten \"{species.Name}\" kan inte anges som {animalDetailed.Type}.");
                }
            }

            return species;
        }

        private static Country GetOrCreateCountry(AnimalDetailed animalDetailed, ZooDataBaseContext db)
        {
            var country = db.Countries.Where(c => c.Name == animalDetailed.CountryOfOrigin).Select(c => c).FirstOrDefault();
            if (country == null)
            {
                country = new Country()
                {
                    Name = animalDetailed.CountryOfOrigin
                };
            }

            return country;
        }

        public void RemoveAnimal(int animalId)
        {
            using (var db = new ZooDataBaseContext())
            {
                var animal = db.Animals.Find(animalId);
                var childQuery = from f in db.Families
                                 where f.AnimalChild.AnimalId == animalId
                                 select f;
                var motherQuery = from f in db.Families
                                  where f.AnimalMother.AnimalId == animalId
                                  select f;
                var fatherQuery = from f in db.Families
                                  where f.AnimalFather.AnimalId == animalId
                                  select f;
                Family childFamily = childQuery.FirstOrDefault();
                Family motherFamily = motherQuery.FirstOrDefault();
                Family fatherFamily = fatherQuery.FirstOrDefault();

                if (childFamily != null)
                {
                    db.Families.Remove(childFamily);
                }
                else
                {
                    if (motherFamily != null)
                    {
                        motherFamily.AnimalMother = null;
                    }
                    if (fatherFamily != null)
                    {
                        fatherFamily.AnimalFather = null;
                    }
                }

                db.Animals.Remove(animal);
                db.SaveChanges();
            }
        }

        public BindingList<AnimalBooking> GetBookingByAnimalIdList(int animalId)
        {
            BindingList<AnimalBooking> list = null;

            using (var db = new ZooDataBaseContext())
            {
                var query = from booking in db.Bookings
                            where booking.AnimalId == animalId
                            select new AnimalBooking()
                            {
                                AnimalId = booking.AnimalId,
                                AnimalName = booking.Animal.Name,
                                BookingId = booking.BookingId,
                                DateTime = booking.DateTime,
                                VeterinaryId = booking.VeterinaryId,
                                VeterinaryName = booking.Veterinarian.Namn
                            };
                list = new BindingList<AnimalBooking>(query.ToList());
            }
            return list;
        }

        public void AddBooking(AnimalBooking bookingModel)
        {
            using (var db = new ZooDataBaseContext())
            {
                var registeredBooking = db.Bookings
                    .Where(b => b.DateTime == bookingModel.DateTime
                                && b.VeterinaryId == bookingModel.VeterinaryId
                                && b.AnimalId == bookingModel.AnimalId)
                    .Select(b => b).FirstOrDefault();

                if (registeredBooking != null)
                {
                    throw new Exception("Bokningen finns redan.");
                }
                else
                {
                    var animal = db.Animals.Find(bookingModel.AnimalId);
                    var vet = db.Veterinarians.Find(bookingModel.VeterinaryId);
                    db.Bookings.Add(new Booking()
                    {
                        Animal = animal,
                        Veterinarian = vet,
                        DateTime = bookingModel.DateTime
                    });

                    db.SaveChanges();
                }

            }
        }

        public void RemoveBooking(int bookingId)
        {
            using (var db = new ZooDataBaseContext())
            {
                var currentBooking = db.Bookings.Find(bookingId);
                db.Bookings.Remove(currentBooking);

                db.SaveChanges();
            }
        }
    }
}
