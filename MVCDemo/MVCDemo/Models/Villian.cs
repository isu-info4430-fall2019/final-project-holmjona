using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
   public class Villian : Person,ISideKickable<Villian> {
        private Costume _Costume;
        private Villian _SideKick;

        #region Contstructors
        public Villian(string fName,DateTime bDay)
            :base(fName,"Bad Guy",bDay) {
            //FirstName = fName;
            //BirthDate = bDay;
        }
        public Villian(string fName)
    : base(fName, "Bad Guy", new DateTime(1960, 1, 1)) {

        }
        internal Costume Costume {
            get {
                return _Costume;
            }

            set {
                _Costume = value;
            }
        }

        internal Villian SideKick {
            get {
                return _SideKick;
            }

            set {
                _SideKick = value;
            }
        }

        public string callForHelp(int Loudness) {
            return "Heeeeyyyy!";
        }

        public Villian callSideKick() {
            return SideKick;
        }
        #endregion
        #region Public Methods
        public override void Dance(Action act) {
            throw new NotImplementedException();
        }

        public override string Laugh() {
            return "Mwhahahaha!";
        }

        public override string ToString() {
            return "Villian: " + FullName;
        }
        #endregion
    }
}
