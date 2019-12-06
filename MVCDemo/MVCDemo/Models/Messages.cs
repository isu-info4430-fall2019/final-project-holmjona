using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    public class Messages {
        private List<String> _Errors;
        private List<String> _Successes;

        public List<String> Errors {
            get { if (_Errors == null) _Errors = new List<string>();
                    return _Errors; }
        }

        public List<String> Successes {
            get {
                if (_Successes == null) _Successes = new List<string>();
                return _Successes;
            }
        }

    }
}
