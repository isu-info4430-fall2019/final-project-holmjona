using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    // fake Data Store of User Roles
    public static class Roles {
        private static List<Role> _List = null;

        public static List<Role> List {
            get {
                if (_List == null) FillList();
                return _List;
            }
        }
        private static void FillList() {
            _List = new List<Role>();
            _List.Add(new Role() {
                ID = 1, Name = "Anonymous",
                SuperHeroAdd = false, SuperHeroEdit = false, SuperHeroDelete = false,
                SuperPetAdd = false, SuperPetEdit = false, SuperPetDelete = false
            });
            _List.Add(new Role() {
                ID = 2, Name = "Data Entry",
                SuperHeroAdd = true, SuperHeroEdit = false, SuperHeroDelete = false,
                SuperPetAdd = true, SuperPetEdit = false, SuperPetDelete = false
            });
            _List.Add(new Role() {
                ID = 3, Name = "Admin",
                SuperHeroAdd = true, SuperHeroEdit = true, SuperHeroDelete = true,
                SuperPetAdd = true, SuperPetEdit = true, SuperPetDelete = true
            });
            _List.Add(new Role() {
                ID = 3, Name = "Power User",
                SuperHeroAdd = true, SuperHeroEdit = true, SuperHeroDelete = false,
                SuperPetAdd = true, SuperPetEdit = true, SuperPetDelete = false
            });
        }

        internal static Role GetByID(int id) {
            Role found = null;
            foreach (Role rle in List) {
                if (rle.ID == id) {
                    found = rle;
                    break;
                }
            }
            return found;
        }
    }
}
