using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    public static class Hasher {
        //https://monkelite.com/how-to-hash-password-in-asp-net-core/
        public static string HashIt(string pass, string salt) {
           byte[] hash = KeyDerivation.Pbkdf2(pass, Encoding.UTF8.GetBytes(salt),
                KeyDerivationPrf.HMACSHA256, 1000, 256 / 8);
            return Convert.ToBase64String(hash);
        }

        public static string GenerateSalt() {
            byte[] rndBytes = new byte[128 / 8];
           RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(rndBytes);
            return Convert.ToBase64String(rndBytes);
        }


    }
}
