using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    [Serializable]
    public class Costume : DatabaseRecord {
        #region Constructors
        public Costume() {
        }
        internal Costume(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "CostumeID";
        internal const string db_ColorMain = "ColorMain";
        internal const string db_ColorSecondary = "ColorSecondary";
        internal const string db_ColorTertiary = "ColorTertiary";
        internal const string db_HasCape = "HasCape";
        internal const string db_HasMask = "HasMask";

        #endregion

        #region Private Variables
        private int _ColorMain;
        private int _ColorSecondary;
        private int _ColorTertiary;
        private bool _HasCape;
        private bool _HasMask;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the ColorMain for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public int ColorMain {
            get {
                return _ColorMain;
            }
            set {
                _ColorMain = value;
            }
        }

        /// <summary>
        /// Gets or sets the ColorSecondary for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public int ColorSecondary {
            get {
                return _ColorSecondary;
            }
            set {
                _ColorSecondary = value;
            }
        }

        /// <summary>
        /// Gets or sets the ColorTertiary for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public int ColorTertiary {
            get {
                return _ColorTertiary;
            }
            set {
                _ColorTertiary = value;
            }
        }

        /// <summary>
        /// Gets or sets the ColorMain for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public string ColorMainAsHexString {
            get {
                return GetAsHexString( _ColorMain);
            }
            set {
                _ColorMain = ConvertFromHex(value);
            }
        }

        /// <summary>
        /// Gets or sets the ColorSecondary for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public string ColorSecondaryAsHexString {
            get {
                return GetAsHexString( _ColorSecondary);
            }
            set {
                _ColorSecondary = ConvertFromHex(value);
            }
        }

        /// <summary>
        /// Gets or sets the ColorTertiary for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public string ColorTertiaryAsHexString {
            get {
                return GetAsHexString( _ColorTertiary);
            }
            set {
                _ColorTertiary = ConvertFromHex(value);
            }
        }

        /// <summary>
        /// Gets or sets the ColorTertiary for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public string ColorsString {
            get {
                return String.Format("{0}|{1}|{2}", 
                    GetAsHexString(_ColorMain),
                    GetAsHexString(_ColorSecondary),
                    GetAsHexString(_ColorTertiary));
            }
        }

        /// <summary>
        /// Gets or sets the HasCape for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public bool HasCape {
            get {
                return _HasCape;
            }
            set {
                _HasCape = value;
            }
        }

        /// <summary>
        /// Gets or sets the HasMask for this ChangeMeOut.Costume object.
        /// </summary>
        /// <remarks></remarks>
        public bool HasMask {
            get {
                return _HasMask;
            }
            set {
                _HasMask = value;
            }
        }

        #endregion

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add Costume to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = SuperDAL.AddCostume(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update Costume to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return SuperDAL.UpdateCostume(this);
        }

        /// <summary>
        /// Calls DAL function to remove Costume from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove() {
            return SuperDAL.RemoveCostume(this);
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft.Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _ColorMain = (int)dr[db_ColorMain];
            _ColorSecondary = (int)dr[db_ColorSecondary];
            _ColorTertiary = (int)dr[db_ColorTertiary];
            _HasCape = (bool)dr[db_HasCape];
            _HasMask = (bool)dr[db_HasMask];
        }

        #endregion

        #region Private Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <remarks>https://stackoverflow.com/questions/1139957/convert-integer-to-hexadecimal-and-back-again</remarks>
        private string GetAsHexString(int i) {

            return i.ToString("X").PadLeft(6,'0');

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <remarks>https://stackoverflow.com/questions/1139957/convert-integer-to-hexadecimal-and-back-again</remarks>
        public static int ConvertFromHex(string s) {
            int retVal = 0;
            try {
                retVal = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
            }catch(Exception ex) {
                retVal = 324567;
            }
            return retVal;
        }

        #endregion

        public override string ToString() {
            return this.GetType().ToString();
        }

    }

}
