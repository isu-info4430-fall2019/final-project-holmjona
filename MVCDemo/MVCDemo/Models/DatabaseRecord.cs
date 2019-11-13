using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo {
    public abstract class DatabaseRecord {
        protected int _ID;
        [Key]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        #region Database Methods
        #region Public Functions
        /// <summary>
        /// Calls DAL function to add object to the database.
        /// </summary>
        /// <remarks></remarks>
        public abstract int dbAdd();

        /// <summary>
        /// Calls DAL function to update object in the database.
        /// </summary>
        /// <remarks></remarks>
        public abstract int dbUpdate();

        /// <summary>
        /// Calls DAL function to remove object from the database.
        /// </summary>
        /// <remarks></remarks>
        public virtual int dbRemove() {
            return -1;
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public abstract void Fill(Microsoft.Data.SqlClient.SqlDataReader dr);

        #endregion
        #endregion

    }
}
