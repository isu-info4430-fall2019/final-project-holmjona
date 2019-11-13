using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
   public class Hideout<A> : DatabaseNamedRecord {
        private List<A> _Members;
        //private GGG _Level;
        private bool _IsHeroBase;


        #region Constructors
        public Hideout() {
        }
        internal Hideout(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "HideoutID";
        internal const string db_Name = "Name";
        internal const string db_IsHeroBase = "IsHeroBase";

        #endregion

        public List<A> Members {
            get { return _Members; }
        }

        /// <summary>
        /// Gets or sets the IsHeroBase for this ChangeMeOut.Hideout object.
        /// </summary>
        /// <remarks></remarks>
        public bool IsHeroBase {
            get {
                return _IsHeroBase;
            }
            set {
                _IsHeroBase = value;
            }
        }


        //public GGG Level {
        //    get {
        //        return _Level;
        //    }
        //}

        public void addMember(A newMember) {
            _Members.Add(newMember);
        }
        //public void setLevel(GGG newLevel) {
        //    _Level = newLevel;
        //}

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add Hideout to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddHideout(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update Hideout to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateHideout(this);
        }

        /// <summary>
        /// Calls DAL function to remove Hideout from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove() {
            return SuperDAL.RemoveHideout(this);
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft .Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _Name = (string)dr[db_Name];
            _IsHeroBase = (bool)dr[db_IsHeroBase];
        }

        #endregion

        public override string ToString() {
            return this.GetType().ToString();
        }

    }
}

