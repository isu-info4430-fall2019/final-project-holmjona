using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
public class SuperHero :Person,ISideKickable<SuperHero>,IComparable<SuperHero> {
        #region Class Level Variables
        private Citizen _AlterEgo;
        private bool _HasCar;
        private Costume _Costume;
        private SuperHero _SideKick;

        #endregion

        #region Contstructors
        public SuperHero() {

        }
        public SuperHero(string fName, Citizen alterEgo, DateTime bDay,bool hasCar)
            : base(fName, "Good Guy", bDay) {
            AlterEgo = alterEgo;
            _HasCar = hasCar;
        }

        public SuperHero(string fName, string alterEgo, DateTime bDay, bool hasCar)
            : base(fName, "Good Guy", bDay) {
            AlterEgo = new Citizen();
            AlterEgo.FirstName = alterEgo;
             _HasCar = hasCar;
        }


        public SuperHero(string fName, string alterEgo, DateTime bDay)
            :base(fName,"Good Guy",bDay) {
            AlterEgo = new Citizen() { FirstName = alterEgo };

        }
        public SuperHero(string fName)
    : base(fName, "Good Guy", new DateTime(1960,1,1)) {

        }

        #endregion

        #region Properities
        public Citizen AlterEgo {
            get { return _AlterEgo; }
            set { _AlterEgo = value; }
        }
        
        [Display(Name ="How tall are you")]
        public override double Height {
            get {
                return base.Height + 6;
            }
            set {
                base.Height = value - 6;
                _Height = value - 6;
            }
        }

        public Costume Costume {
            get {
                return _Costume;
            }

            set {
                _Costume = value;
            }
        }

        internal SuperHero SideKick {
            get {
                return _SideKick;
            }

            set {
                _SideKick = value;
            }
        }
        #endregion


        #region Public Methods
        public override void Dance(Action act) {
            // thi is how a hero dances.
            if(act == Action.Fly) {
                // Do fly stuff
            }else if(act == Action.Break) {
                // stand on my head
            }
        }

        public override void Fight(int h) {
            // this is how a hero fights with
            // Weapons!
            if (h == 0) {
                base.Fight(h);
            }
        }

        public override string Laugh() {
            return "HA HA Ha";
        }

        public override string ToString() {
            return "Hero: " + FullName;
        }

        public SuperHero callSideKick() {
            return SideKick ;
        }

        public string callForHelp(int loudy) {
            string call = "Hey SideKick";
            for (int i = 0; i < loudy; i++)
                call += "!";
            return call;
        }

        public int CompareTo(SuperHero theOtherGuy) {
            //// 0 - WE are the same
            //// 1 = I am bigger
            //// -1 other guy is bigger
            //if (this.Age() > theOtherGuy.Age()) {
            //    return 1;
            //}else if (this.Age() < theOtherGuy.Age()) {
            //    return -1;
            //}else {
            //    return 0;
            //}
            return this.FirstName.CompareTo(theOtherGuy.FullName);
        }

        #endregion

        #region operators

        public static String operator +(SuperHero s, Villian v){
            if (s.Costume != null) {
                return s.FirstName + " wins.";
            }
            return s.FirstName + " vs. " + v.FirstName;
            }

        public static bool operator >(SuperHero s1, SuperHero s2) {
            if (s1.Costume != null && s2.Costume != null) {
                return false;
            } else if (s1.Costume != null && s2.Costume == null) {
                return true;
            } else if (s2.Costume != null && s1.Costume == null) {
                return false;
            } else {
                return false;
            }
        }

        public static bool operator <(SuperHero s1, SuperHero s2) {
            return !(s1 > s2);
        }

        #endregion

    }
}
