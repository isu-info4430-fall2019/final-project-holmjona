using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
   public class Hideout<A,GGG> {
        private String _Name;
        private List<A> _Members;
        private GGG _Level;

        public Hideout() {
            _Members = new List<A>();
        }
        public List<A> Members {
            get { return _Members; }
        }

        public string Name {
            get {
                return _Name;
            }

            set {
                _Name = value;
            }
        }

        public GGG Level {
            get {
                return _Level;
            }
        }

        public void addMember(A newMember) {
            _Members.Add(newMember);
        }
        public void setLevel(GGG newLevel) {
            _Level = newLevel;
        }
    }
}
