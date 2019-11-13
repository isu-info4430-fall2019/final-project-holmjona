using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
    public abstract class Person : DatabaseRecord {
        #region Private Variables
        protected String _FirstName = "";
        protected String _LastName = "";
        protected DateTime _BirthDate = DateTime.MaxValue;
        protected Double _Height = 0.0;
        protected float _ShoeSize = 0.0F;
        protected Color _EyeColor = Color.Brown;
        // 0 = brown, 1 = blue, 

        public enum Color {
            Brown,
            Blue,
            Green,
            Hazel = 99,
            Gray= 56,
            Aqua
        }

        public enum Action {
            Funky,
           Fly,
           Break
        }


        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Person() {
            FirstName = LastName = "Unknown";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="bDay"></param>
        public Person(String fName, String lName, DateTime bDay, double ShoeSize) {
            FirstName = fName;
            LastName = lName;
            BirthDate = bDay;
        }

        public Person(String fName, String lName, DateTime bDay) {
            FirstName = fName;
            LastName = lName;
            BirthDate = bDay;
        }
        #endregion

        #region Properties
        [Display(Name = "First name")]
        [StringLength(60)]
        [Required(ErrorMessage = "Oops, you need a {0}")]
        public string FirstName {
            get {
                return _FirstName;
            }
            set {
                if (value != null && value != "")
                    _FirstName = value;
                else
                    _FirstName = "Unknown";

            }
        }
        [Display(Name = "Last name")]
        [StringLength(60)]
        public string LastName {
            get {
                return _LastName;
            }
            set {
                if (value != null && value != "")
                    _LastName = value;
                else
                    _LastName = "Unknown";
            }
        }

        public string FullName {
            get {
                return String.Format("{0} {1}",
                    FirstName, LastName);
            }
        }

        [Display(Name = "When you entered the world")]
        [DataType(DataType.Date,ErrorMessage ="This date does not match")]
        public DateTime BirthDate {
            get {
                return _BirthDate;
            }
            set {
                if (value <= DateTime.Now)
                    _BirthDate = value;
            }
        }


        [Range(1,20)]
        public float ShoeSize {
            get {
                return _ShoeSize;
            }
            set {
                if (value > 0)
                    _ShoeSize = value;
            }
        }

        public Color EyeColor {
            get {
                return _EyeColor;
            }
            set {
                _EyeColor = value;
            }
        }


        public virtual double Height {
            get {
                return _Height;
            }
            set {
                if (value > 0)
                    _Height = value;
            }
        }

     
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public int Age() {
            return DateTime.Now.Year - BirthDate.Year;
        }
        public string Info() {
            return string.Format(
                "{0} {1} {2}",
               FullName, Age(), EyeColor);
        }


        public override string ToString() {
            return FullName;
        }

        public void setEyeColor(Color clr ) {
            EyeColor = clr;
        }

        public abstract void Dance(Action act);

        public virtual void Fight(int i) {
            // this is how a normal person fights.
            // with pillows.
        }

        public virtual string Laugh() {
            return "hehehe";
        }



        #endregion

    }
}
