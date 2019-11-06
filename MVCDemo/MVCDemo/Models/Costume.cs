using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
 public class Costume :DatabaseRecord {
        private string _Color = "Blue";
        private bool _HasCape = false;
        private bool _HasMask = false;

        /// <summary>
        /// 
        /// </summary>
        public string Color {
            get {
                return _Color;
            }

            set {
                _Color = value;
            }
        }

        public bool HasCape {
            get {
                return _HasCape;
            }

            set {
                _HasCape = value;
            }
        }

        public bool HasMask {
            get {
                return _HasMask;
            }

            set {
                _HasMask = value;
            }
        }
    }
}
