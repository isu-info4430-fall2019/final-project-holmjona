using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo {
    public abstract class DatabaseNamedRecord : DatabaseRecord {
        protected string _Name;
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}
