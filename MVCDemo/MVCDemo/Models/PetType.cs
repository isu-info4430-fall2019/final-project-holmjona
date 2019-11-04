using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    public class PetType : DatabaseNamedRecord {
        public PetType() {
            Name = "Mutt";
        }
    }
}
