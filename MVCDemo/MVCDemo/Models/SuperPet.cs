using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    public class SuperPet : DatabaseNamedRecord {
        private int _PetTypeID;
        private int _SuperHeroID;

        public int PetTypeID {
            get { return _PetTypeID; }
            set { _PetTypeID = value; }
        }
        public int SuperHeroID {
            get { return _SuperHeroID; }
            set { _SuperHeroID = value; }
        }


    }
}
