using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    public class City : DatabaseRecord {
        #region Constructors
        public City() {
        }
        internal City(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "CityID";
        internal const string db_Name = "Name";

        #endregion

        #region Private Variables
        private string _Name;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Name for this ChangeMeOut.City object.
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
        /// Calls DAL function to add City to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddCity(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update City to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateCity(this);
        }

        /// <summary>
        /// Calls DAL function to remove City from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove() {
            return SuperDAL.RemoveCity(this);
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
