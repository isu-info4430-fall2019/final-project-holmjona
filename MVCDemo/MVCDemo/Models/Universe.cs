using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    public static class Universe {
        private static string _Name;
        private static List<Person> _People;
        private static List<SuperHero> _supers;
        private static List<Villian> _vills;
        private static List<Citizen> _innocs;
        private static List<Costume> _outfits;

        private static SuperHero _CurrentSuperHero;
        private static Villian _CurrentVillian;

        private static Hideout<SuperHero> _HallOfJustice;

        public enum Awesomeness {
            Very,
            Somewhat,
            Lacking
        }

        public static string Name {
            get {
                return _Name;
            }

            set {
                _Name = value;
            }
        }

        [ValidateNever]
        public static List<Person> People {
            get {
                if (_People == null) {
                    populateUniverse();
                }
                return _People;
            }
        }

        [ValidateNever]
        public static List<SuperHero> Supers {
            get {
                if (_supers == null) populateUniverse();
                return _supers;
            }

            set {
                _supers = value;
            }
        }

        [ValidateNever]
        public static List<Villian> Vills {
            get {
                if (_vills == null) populateUniverse();
                return _vills;
            }

            set {
                _vills = value;
            }
        }

        [ValidateNever]
        public static List<Citizen> Innocs {
            get {
                if (_innocs == null) populateUniverse();
                return _innocs;
            }

            set {
                _innocs = value;
            }
        }

        public static SuperHero CurrentSuperHero {
            get {
                return _CurrentSuperHero;
            }

            set {
                _CurrentSuperHero = value;
            }
        }

        public static Villian CurrentVillian {
            get {
                return _CurrentVillian;
            }

            set {
                _CurrentVillian = value;
            }
        }

        [ValidateNever]
        public static Hideout<SuperHero> HallOfJustice {
            get {
                if(_HallOfJustice == null) {
                    populateUniverse();
                }
                return _HallOfJustice;
            }

            set {
                _HallOfJustice = value;
            }
        }

        // No constructors allowed on static class.


        private static void populateUniverse() {
            // make sure lists exist.
            _People = new List<Person>();
            _supers = new List<SuperHero>();
            _vills = new List<Villian>();
            _innocs = new List<Citizen>();
            _outfits = new List<Costume>();


            _supers.Add(new SuperHero("Superman"));
            _supers.Add(new SuperHero("Batman"));
            _supers.Add(new SuperHero("Wonder Woman"));
            _supers.Add(new SuperHero("Green Lantern"));
            _supers.Add(new SuperHero("The Flash"));
            _supers.Add(new SuperHero("Supergirl"));
            _supers.Add(new SuperHero("Aquaman"));


            _vills.Add(new Villian("Lex Luther"));
            _vills.Add(new Villian("Joker"));
            _vills.Add(new Villian("Bane"));
            _vills.Add(new Villian("Sinestro"));
            _vills.Add(new Villian("Felix Faust"));
            _vills.Add(new Villian("Doomsday"));
            _vills.Add(new Villian("Circe"));
            _vills.Add(new Villian("Gigantica"));
            _vills.Add(new Villian("Harley"));
            _vills[1].SideKick = _vills[8];

            _innocs.Add(new Citizen("Clark", "Kent"));
            _innocs.Add(new Citizen("Bruce", "Wayne"));
            _innocs.Add(new Citizen("Jimmy", "Olsen"));
            _innocs.Add(new Citizen("Lois", "Lane"));


            _outfits.Add(new Costume() { ColorMain = (int)System.Drawing.KnownColor.Blue });
            _outfits.Add(new Costume() { ColorMain = (int)System.Drawing.KnownColor.Black, HasCape = true, HasMask = true });
            _outfits.Add(new Costume() { ColorMain = (int)System.Drawing.KnownColor.Green, HasMask = true });
            _outfits.Add(new Costume() { ColorMain = (int)System.Drawing.KnownColor.Purple });
            _outfits.Add(new Costume() { ColorMain = (int)System.Drawing.KnownColor.Red, HasMask = true });

            _supers[0].AlterEgo = _innocs[0];
            _supers[1].AlterEgo = _innocs[1];
            _supers[0].Costume = _outfits[0];
            _supers[1].Costume = _outfits[1];
            _supers[3].Costume = _outfits[2];
            _supers[4].Costume = _outfits[4];
            _vills[1].Costume = _outfits[3];

            _People.AddRange(_supers);
            _People.AddRange(_vills);
            _People.AddRange(_innocs);

            _HallOfJustice = new Hideout<SuperHero>();
            _HallOfJustice.Members.AddRange(_supers.Skip(2).Take(3));

        }

    }
}
