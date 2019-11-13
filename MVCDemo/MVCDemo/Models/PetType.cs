using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo {
    public class PetType : DatabaseRecord {
        #region Constructors
        public PetType() {
        }
        internal PetType(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "PetTypeID";
        internal const string db_Name = "Name";

        #endregion

        #region Private Variables
        private string _Name;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Name for this ChangeMeOut.PetType object.
        /// </summary>
        /// <remarks></remarks>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value.Trim();
            }
        }


        #endregion

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add PetType to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddPetType(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update PetType to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdatePetType(this);
        }

        /// <summary>
        /// Calls DAL function to remove PetType from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove() {
            return SuperDAL.RemovePetType(this);
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft.Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _Name = (string)dr[db_Name];
        }

        #endregion

        public override string ToString() {
            return this.GetType().ToString();
        }

    }

}
