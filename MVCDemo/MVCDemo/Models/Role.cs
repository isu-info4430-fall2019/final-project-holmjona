using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace MVCDemo.Models {
    public class Role : DatabaseNamedRecord {
        private bool _SuperHeroAdd = false;
        private bool _SuperHeroEdit = false;
        private bool _SuperHeroDelete = false;
        private bool _SuperPetAdd = false;
        private bool _SuperPetEdit = false;
        private bool _SuperPetDelete = false;

        [JsonConstructor]
        public Role() {
        }

            public Role(Role rleToClone = null) {
            if (rleToClone != null) Fill(rleToClone);
        }

        public Role(SqlDataReader dr) {
            Fill(dr);
        }

        public bool SuperHeroAdd {
            get { return _SuperHeroAdd; }
            set { _SuperHeroAdd = value; }
        }

        public bool SuperHeroEdit {
            get { return _SuperHeroEdit; }
            set { _SuperHeroEdit = value; }
        }

        public bool SuperHeroDelete {
            get { return _SuperHeroDelete; }
            set { _SuperHeroDelete = value; }
        }

        public bool SuperPetAdd {
            get { return _SuperPetAdd; }
            set { _SuperPetAdd = value; }
        }

        public bool SuperPetEdit {
            get { return _SuperPetEdit; }
            set { _SuperPetEdit = value; }
        }

        public bool SuperPetDelete {
            get { return _SuperPetDelete; }
            set { _SuperPetDelete = value; }
        }



        public override int dbAdd() {
            throw new NotImplementedException();
        }

        public override int dbUpdate() {
            throw new NotImplementedException();
        }

        public override void Fill(SqlDataReader dr) {
            this.ID = (int)dr["ID"];
            this.Name = (string)dr["Name"];
            this.SuperHeroAdd = (bool)dr["SuperHeroAdd"];
            this.SuperHeroEdit = (bool)dr["SuperHeroEdit"];
            this.SuperHeroDelete = (bool)dr["SuperHeroDelete"];
            this.SuperPetAdd = (bool)dr["SuperPetAdd"];
            this.SuperPetEdit = (bool)dr["SuperPetEdit"];
            this.SuperPetDelete = (bool)dr["SuperPetDelete"];
        }

        public void Fill(Role otherRole) {
            this.ID = otherRole.ID;
            this.Name = otherRole.Name;
            this.SuperHeroAdd = otherRole.SuperHeroAdd;
            this.SuperHeroEdit = otherRole.SuperHeroEdit;
            this.SuperHeroDelete = otherRole.SuperHeroDelete;
            this.SuperPetAdd = otherRole.SuperPetAdd;
            this.SuperPetEdit = otherRole.SuperPetEdit;
            this.SuperPetDelete = otherRole.SuperPetDelete;
        }
    }
}
