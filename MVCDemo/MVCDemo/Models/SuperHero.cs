using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
public class SuperHero :Person,ISideKickable<SuperHero>,IComparable<SuperHero> {
        #region Constructors
        public SuperHero() {
        }
        internal SuperHero(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "SuperHeroID";
        internal const string db_FirstName = "FirstName";
        internal const string db_LastName = "LastName";
        internal const string db_DateOfBirth = "DateOfBirth";
        internal const string db_EyeColor = "EyeColor";
        internal const string db_HeightInInches = "HeightInInches";
        internal const string db_AlterEgo = "AlterEgoID";
        internal const string db_SideKick = "SideKickID";
        internal const string db_Costume = "CostumeID";

        #endregion

        #region Private Variables
        private int _AlterEgoID;
        private Citizen _AlterEgo;
        private int _SideKickID;
        private SuperHero _SideKick;
        private int _CostumeID;
        private Costume _Costume;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets the AlterEgo for this ChangeMeOut.SuperHeroe object.
        /// </summary>
        /// <remarks></remarks>
        public Citizen AlterEgo {
            get {
                return _AlterEgo;
            }
            set {
                _AlterEgo = value;
            }
        }

        /// <summary>
        /// Gets or sets the SideKick for this ChangeMeOut.SuperHeroe object.
        /// </summary>
        /// <remarks></remarks>
        public SuperHero SideKick {
            get {
                return _SideKick;
            }
            set {
                _SideKick = value;
            }
        }

        /// <summary>
        /// Gets or sets the Costume for this ChangeMeOut.SuperHeroe object.
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
        /// Gets or sets the CostumeID for this ChangeMeOut.SuperHeroe object.
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
        /// Calls DAL function to add SuperHeroe to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddSuperHero(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update SuperHeroe to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateSuperHero(this);
        }

        /// <summary>
        /// Calls DAL function to remove SuperHeroe from the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbRemove() {
            return SuperDAL.RemoveSuperHero(this);
        }

        public override void Dance(Action act) {
            // thi is how a hero dances.
            if (act == Action.Fly) {
                // Do fly stuff
            } else if (act == Action.Break) {
                // stand on my head
            }
        }

        public override void Fight(int h) {
            // this is how a hero fights with
            // Weapons!
            if (h == 0) {
                base.Fight(h);
            }
        }

        public override string Laugh() {
            return "HA HA Ha";
        }

        public override string ToString() {
            return "Hero: " + FullName;
        }

        public SuperHero callSideKick() {
            return SideKick;
        }

        public string callForHelp(int loudy) {
            string call = "Hey SideKick";
            for (int i = 0; i < loudy; i++)
                call += "!";
            return call;
        }

        public int CompareTo(SuperHero theOtherGuy) {
            //// 0 - WE are the same
            //// 1 = I am bigger
            //// -1 other guy is bigger
            //if (this.Age() > theOtherGuy.Age()) {
            //    return 1;
            //}else if (this.Age() < theOtherGuy.Age()) {
            //    return -1;
            //}else {
            //    return 0;
            //}
            return this.FirstName.CompareTo(theOtherGuy.FullName);
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
            _AlterEgoID = (int)dr[db_AlterEgo];
            _SideKickID = (int)dr[db_SideKick];
            _CostumeID = (int)dr[Costume.db_ID];
        }



        #endregion

        
        #region operators

        public static String operator +(SuperHero s, Villian v){
            if (s.Costume != null) {
                return s.FirstName + " wins.";
            }
            return s.FirstName + " vs. " + v.FirstName;
            }

        public static bool operator >(SuperHero s1, SuperHero s2) {
            if (s1.Costume != null && s2.Costume != null) {
                return false;
            } else if (s1.Costume != null && s2.Costume == null) {
                return true;
            } else if (s2.Costume != null && s1.Costume == null) {
                return false;
            } else {
                return false;
            }
        }

        public static bool operator <(SuperHero s1, SuperHero s2) {
            return !(s1 > s2);
        }

        #endregion

    }
}
