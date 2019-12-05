using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    // fake Data Store of Users
    public static class Users {
        private static List<User> _List = null;

        public static List<User> List {
            get {
                if (_List == null) FillList();
                return _List; 
            }
        }
        private static void FillList() {
            _List = new List<User>();
            _List.Add(new User() { ID = 1, UserName = "tester", Salt = "NU7OqacPNlFCzwfgytskAg=="
                ,RoleID = 3, Password = "HbfdqFOoL2qXmOs2QVcC0DwyAHk4GAGzzWlhR/BilPo="}); //admin - tester
            _List.Add(new User() { ID = 2, UserName = "tested", Salt = "FptIqr/HROmmDE/BAJXblA=="
                ,RoleID = 2, Password = "McJVYpypkS1zTkQ3DcdqNLaVjdiGukUxtXO8ZvQpGLU=" }); //data entry - tester
            _List.Add(new User() { ID = 3, UserName = "retest", Salt = "8xB3bhEtpq4ADaTsVKglUg=="
                ,RoleID = 4, Password = "GEJ66bhiyYH/a871hLHWDnkIZbBR1aaHjU9i9sMeeM0=" }); //power user - tester
            _List.Add(new User() { ID = 4, UserName = "retset", Salt = "DsEmUsECxWip7yg89t/k+w=="
                ,RoleID = 2, Password = "7IDcjEVvr8XZmWpV/np4rH1KYvsrx2Fa0A8OT0TVekg=" }); //data entry - tester
            _List.Add(new User() { ID = 5, UserName = "west", Salt = "mbuoRIOV4no4m58niZ/EKA=="
                ,RoleID = 1, Password = "H4gu1BEHl+9iBlOShmUvjxnaOSGHGZ677tvbEfVtCLE=" }); //Anonymous - tester
        }

        internal static User GetByID(int id) {
            User found = null;
            foreach (User usr in List) {
                if(usr.ID == id) {
                    found = usr;
                    break;
                }
            }
            return found;
        }
        internal static User GetByUserName(string uName) {
            User found = null;
            foreach (User usr in List) {
                if (usr.UserName == uName) {
                    found = usr;
                    break;
                }
            }
            return found;
        }

        public static int AddUser(User newUser) {
            newUser.ID = List.Max(u => u.ID) +1;
            List.Add(newUser);
            return newUser.ID;
        }

        public static int UpdateUser(User newUser) {
            User toUpdate = GetByID(newUser.ID);
            toUpdate.UserName = newUser.UserName;
            toUpdate.Password = newUser.Password;
            toUpdate.Salt = newUser.Salt;
            toUpdate.RoleID = newUser.RoleID;
            return 1; // success
        }

    }
}
