using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    public class Villian : Person,ISideKickable<Villian> {
        #region Constructors
        public Villian() {
        }
        public Villian(string fName, DateTime bDay)
     : base(fName, "Bad Guy", bDay) {
            //FirstName = fName;
            //BirthDate = bDay;
        }
        public Villian(string fName)
    : base(fName, "Bad Guy", new DateTime(1960, 1, 1)) {

        }
        internal Villian(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "VillianID";
        internal const string db_FirstName = "FirstName";
        internal const string db_LastName = "LastName";
        internal const string db_DateOfBirth = "DateOfBirth";
        internal const string db_EyeColor = "EyeColor";
        internal const string db_HeightInInches = "HeightInInches";
        internal const string db_SideKick = "SideKickID";
        internal const string db_Costume = "CostumeID";

        #endregion

        #region Private Variables
        private int _SideKickID;
        private Villian _SideKick;
        private int _CostumeID;
        private Costume _Costume;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the SideKick for this ChangeMeOut.Villian object.
        /// </summary>
        /// <remarks></remarks>
        public Villian SideKick {
            get {
                return _SideKick;
            }
            set {
                _SideKick = value;
            }
        }

        /// <summary>
        /// Gets or sets the Costume for this ChangeMeOut.Villian object.
        /// </summary>
        /// <remarks></remarks>
        
        public Costume Costume {
            get {
                if (_Costume == null) {
                    _Costume = SuperDAL.GetCostume(_CostumeID);
                }
                return _Costume;
            }
            set {
                _Costume = value;
                if (value == null) {
                    _CostumeID = -1;
                } else {
                    _CostumeID = value.ID;
                }
            }
        }
        /// <summary>
        /// Gets or sets the CostumeID for this ChangeMeOut.Villian object.
        /// </summary>
        /// <remarks></remarks>
        public int CostumeID {
            get {
                return _CostumeID;
            }
            set {
                _CostumeID = value;
            }
        }


        #endregion

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add Villian to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddVillian(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update Villian to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateVillian(this);
        }

        /// <summary>
        /// Calls DAL function to remove Villian from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove() {
            return SuperDAL.RemoveVillian(this);
        }



        public string callForHelp(int Loudness) {
            return "Heeeeyyyy!";
        }

        public Villian callSideKick() {
            return SideKick;
        }
        public override void Dance(Action act) {
            throw new NotImplementedException();
        }

        public override string Laugh() {
            return "Mwhahahaha!";
        }

        public override string ToString() {
            return "Villian: " + FullName;
        }
        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft.Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _FirstName = (string)dr[db_FirstName];
            _LastName = (string)dr[db_LastName];
            _BirthDate = (DateTime)dr[db_DateOfBirth];
            _EyeColor = (Color)Enum.ToObject(typeof(Color), (byte)dr[db_EyeColor]);
            _Height = (double)dr[db_HeightInInches];
            _SideKickID = (int)dr[db_SideKick];
            _CostumeID = (int)dr[Costume.db_ID];
        }


        #endregion


    }
}
