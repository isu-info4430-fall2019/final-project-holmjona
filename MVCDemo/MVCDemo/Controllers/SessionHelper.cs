using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Controllers {
    /// <summary>
    ///  Use the session in MVC using Json Serialization
    /// </summary>
    /// <remarks> Referenced: http://learningprogramming.net/net/asp-net-core-mvc/build-shopping-cart-with-session-in-asp-net-core-mvc/</remarks>
    public static class SessionHelper {
        
        /// <summary>
        /// Set object to session
        /// </summary>
        /// <param name="sess"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Set(ISession sess,  string key, object obj) {
            sess.SetString(key, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// Get Object from Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sess"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(ISession sess, string key) {
            if (sess.Keys.Contains(key)) {
                return JsonConvert.DeserializeObject<T>(sess.GetString(key));
            } else {
                return default(T);
            }
        }

    }
}
