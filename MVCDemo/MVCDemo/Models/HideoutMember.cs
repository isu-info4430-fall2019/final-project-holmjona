//Created By: COB\holmjona (using Code generator)
//Created On: 11/12/2019 10:01:44 AM
using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace MVCDemo {
    /// <summary>
    /// TODO: Comment this
    /// </summary>
    /// <remarks></remarks>

    public class HideoutMember<M> : DatabaseRecord {
        #region Private Variables
        private int _HideoutID;
        private Hideout<M> _Hideout;
        private int _MemberID;
        private M _Member;
        private Type T = typeof(M);


        #endregion
        #region Constructors
        public HideoutMember() {
        }
        internal HideoutMember(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "HideoutMemberID";
        internal const string db_Hideout = "HideoutID";
        internal const string db_Member = "MemberID";

        #endregion


        #region Public Properties
        /// <summary>
        /// Gets or sets the Hideout for this ChangeMeOut.HideoutMember object.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Hideout<M> Hideout {
            get {
                if (_Hideout == null) {
                    _Hideout = SuperDAL.GetHideout(T,_HideoutID);
                }
                return _Hideout;
            }
            set {
                _Hideout = value;
                if (value == null) {
                    _HideoutID = -1;
                } else {
                    _HideoutID = value.ID;
                }
            }
        }
        /// <summary>
        /// Gets or sets the HideoutID for this ChangeMeOut.HideoutMember object.
        /// </summary>
        /// <remarks></remarks>
        public int HideoutID {
            get {
                return _HideoutID;
            }
            set {
                _HideoutID = value;
            }
        }

        /// <summary>
        /// Gets or sets the Member for this ChangeMeOut.HideoutMember object.
        /// </summary>
        /// <remarks></remarks>
        public M Member {
            get {
                return _Member;
            }
            set {
                _Member = value;
            }
        }


        #endregion

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add HideoutMember to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddHideoutMember(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update HideoutMember to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateHideoutMember(this);
        }

        /// <summary>
        /// Calls DAL function to remove HideoutMember from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove() {
            return SuperDAL.RemoveHideoutMember(this);
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft.Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _HideoutID = (int)dr[db_Hideout];
            _MemberID = (int)dr[db_Member];
        }

        #endregion

        public override string ToString() {
            return this.GetType().ToString();
        }

    }
}
