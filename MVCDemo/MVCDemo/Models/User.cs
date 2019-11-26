using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MVCDemo.Models {
    public class User: DatabaseRecord {
        private string _UserName;
        private string _Password;
        private string _Salt;
        private int _RoleID;
        private Role _Role;

        public User(User usrToClone = null) {
            if (usrToClone != null) Fill(usrToClone);
        }

        public string UserName {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }

        /// <summary>
        /// Only used to confirm passwords
        /// </summary>
        public string ConfirmPassword { get; set; }

        public string Salt {
            get { return _Salt; }
            set { _Salt = value; }
        }

        public int RoleID {
            get {
                return _RoleID;
            }
            set {
                _RoleID = value;
            }
        }
        
        public Role Role {
            get {
                if (_Role == null) {
                    _Role = SuperDAL.GetRole(_RoleID);
                }
                return _Role;
            }
            set {
                _Role = value;
                if (value == null) {
                    _RoleID = -1;
                } else {
                    _RoleID = value.ID;
                }
            }
        }




        public override int dbAdd() {
            throw new NotImplementedException();
        }

        public override int dbUpdate() {
            throw new NotImplementedException();
        }

        public override void Fill(SqlDataReader dr) {
            throw new NotImplementedException();
        }

        public void Fill(User otherUser) {
            this.ID = otherUser.ID;
            this.UserName = otherUser.UserName;
            this.RoleID = otherUser.RoleID;
            this.Password = otherUser.Password;
            this.Salt = otherUser.Salt;
        }
    }
}
