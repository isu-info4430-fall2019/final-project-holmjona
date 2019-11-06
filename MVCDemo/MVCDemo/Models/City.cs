using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
   public class City :DatabaseRecord {
        private Hideout<SuperHero, double>[,] _PossibleHideouts;

        public City() {
            _PossibleHideouts = new Hideout<SuperHero, double>[3, 3];
        }

        internal Hideout<SuperHero, double>[,] PossibleHideouts {
            get {
                return _PossibleHideouts;
            }

            set {
                _PossibleHideouts = value;
            }
        }
    }
}
