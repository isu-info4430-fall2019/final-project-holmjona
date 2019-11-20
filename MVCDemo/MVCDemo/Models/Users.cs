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
                ,Password = "HbfdqFOoL2qXmOs2QVcC0DwyAHk4GAGzzWlhR/BilPo="}); // tester
            _List.Add(new User() { ID = 2, UserName = "tested", Salt = "FptIqr/HROmmDE/BAJXblA=="
                ,Password = "McJVYpypkS1zTkQ3DcdqNLaVjdiGukUxtXO8ZvQpGLU=" }); // tester
            _List.Add(new User() { ID = 3, UserName = "retest", Salt = "8xB3bhEtpq4ADaTsVKglUg=="
                ,Password = "GEJ66bhiyYH/a871hLHWDnkIZbBR1aaHjU9i9sMeeM0=" }); // tester
            _List.Add(new User() { ID = 4, UserName = "retset", Salt = "DsEmUsECxWip7yg89t/k+w=="
                ,Password = "7IDcjEVvr8XZmWpV/np4rH1KYvsrx2Fa0A8OT0TVekg=" }); // tester
            _List.Add(new User() { ID = 5, UserName = "west", Salt = "mbuoRIOV4no4m58niZ/EKA=="
                ,Password = "H4gu1BEHl+9iBlOShmUvjxnaOSGHGZ677tvbEfVtCLE=" }); // tester
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
    }
}
