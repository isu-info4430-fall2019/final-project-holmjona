using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
   public class Citizen : Person {
        public Citizen() { }
        public Citizen(string fName,string lastName)
    : base(fName, lastName, new DateTime(1960, 1, 1)) {

        }
        public override void Dance(Action act) {
            
        }
        public override string ToString() {
            return "Citizen: " + FullName;
        }
        /// <summary>
        /// Shallow Copy
        /// </summary>
        /// <returns></returns>
        public Citizen CopyMe() {
            Citizen newCitizen;
            newCitizen = new Citizen();

            return newCitizen;
        }
        /// <summary>
        /// Deep Copy
        /// </summary>
        /// <returns></returns>
        public Citizen CloneMe() {
            Citizen cloner = new Citizen();
            cloner.FirstName = this.FirstName;
            cloner.LastName = this.LastName;
            cloner.Height = this.Height;
            cloner.ShoeSize = this.ShoeSize;
            return cloner;
        }

    }
}
