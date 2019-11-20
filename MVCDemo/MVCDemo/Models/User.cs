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

        public override int dbAdd() {
            throw new NotImplementedException();
        }

        public override int dbUpdate() {
            throw new NotImplementedException();
        }

        public override void Fill(SqlDataReader dr) {
            throw new NotImplementedException();
        }
    }
}
