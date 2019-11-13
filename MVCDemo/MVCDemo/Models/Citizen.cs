using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
    public class Citizen : Person {
        #region Constructors
        public Citizen() {
        }
        internal Citizen(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "CitizenID";
        internal const string db_FirstName = "FirstName";
        internal const string db_LastName = "LastName";
        internal const string db_DateOfBirth = "DateOfBirth";
        internal const string db_EyeColor = "EyeColor";
        internal const string db_HeightInInches = "HeightInInches";

        #endregion

        #region Private Variables
        
        #endregion

        #region Public Properties
        #endregion

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add Citizen to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddCitizen(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update Citizen to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateCitizen(this);
        }

        /// <summary>
        /// Calls DAL function to remove Citizen from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove() {
            return SuperDAL.RemoveCitizen(this);
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
            _DateOfBirth = (DateTime)dr[db_DateOfBirth];
            _EyeColor = (byte)dr[db_EyeColor];
            _HeightInInches = (double)dr[db_HeightInInches];
        }

        #endregion

        public override string ToString() {
            return this.GetType().ToString();
        }

        public override void Dance(Action act) {

        }
   
        /// <summary>
        /// Shallow Copy
        /// </summary>
        /// <returns></returns>
        public Citizen CopyMe() {
            Citizen newCitizen;
            newCitizen = new Citizen();

            return newCitizen;
        }
        /// <summary>
        /// Deep Copy
        /// </summary>
        /// <returns></returns>
        public Citizen CloneMe() {
            Citizen cloner = new Citizen();
            cloner.FirstName = this.FirstName;
            cloner.LastName = this.LastName;
            cloner.Height = this.Height;
            cloner.ShoeSize = this.ShoeSize;
            return cloner;
        }
    }

}
