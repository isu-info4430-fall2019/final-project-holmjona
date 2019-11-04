using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo {
    public abstract class DatabaseRecord {
        private int _ID;

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

    }
}
