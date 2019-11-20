using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo {
    public class SuperPet : DatabaseRecord {
        #region Constructors
        public SuperPet() {
        }
        internal SuperPet(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "SuperPetID";
        internal const string db_Name = "Name";
        internal const string db_PetType = "PetTypeID";
        internal const string db_SuperHero = "SuperHeroID";

        #endregion

        #region Private Variables
        private string _Name;
        private int _PetTypeID;
        private PetType _PetType;
        private int _SuperHeroID;
        private SuperHero _SuperHero;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Name for this ChangeMeOut.SuperPet object.
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

        /// <summary>
        /// Gets or sets the PetType for this ChangeMeOut.SuperPet object.
        /// </summary>
        /// <remarks></remarks>
        
        public PetType PetType {
            get {
                if (_PetType == null) {
                    _PetType = SuperDAL.GetPetType(_PetTypeID);
                }
                return _PetType;
            }
            set {
                _PetType = value;
                if (value == null) {
                    _PetTypeID = -1;
                } else {
                    _PetTypeID = value.ID;
                }
            }
        }
        /// <summary>
        /// Gets or sets the PetTypeID for this ChangeMeOut.SuperPet object.
        /// </summary>
        /// <remarks></remarks>
        public int PetTypeID {
            get {
                return _PetTypeID;
            }
            set {
                _PetTypeID = value;
            }
        }

        /// <summary>
        /// Gets or sets the SuperHero for this ChangeMeOut.SuperPet object.
        /// </summary>
        /// <remarks></remarks>
        public SuperHero SuperHero {
            get {
                if (_SuperHero == null) {
                    _SuperHero = SuperDAL.GetSuperHero(_SuperHeroID);
                }
                return _SuperHero;
            }
            set {
                _SuperHero = value;
                if (value == null) {
                    _SuperHeroID = -1;
                } else {
                    _SuperHeroID = value.ID;
                }
            }
        }
        /// <summary>
        /// Gets or sets the SuperHeroID for this ChangeMeOut.SuperPet object.
        /// </summary>
        /// <remarks></remarks>
        public int SuperHeroID {
            get {
                return _SuperHeroID;
            }
            set {
                _SuperHeroID = value;
            }
        }


        #endregion

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add SuperPet to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddSuperPet(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update SuperPet to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateSuperPet(this);
        }

        /// <summary>
        /// Calls DAL function to remove SuperPet from the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbRemove() {
            return SuperDAL.RemoveSuperPet(this);
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
            _PetTypeID = (int)dr[PetType.db_ID];
            _SuperHeroID = (int)dr[db_SuperHero];
        }

        #endregion

        public override string ToString() {
            return this.GetType().ToString();
        }

    }
}

