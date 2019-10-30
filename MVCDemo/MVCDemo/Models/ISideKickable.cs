using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo {
    interface ISideKickable<T> {
        /// <summary>
        /// Calls the sidekick
        /// </summary>
        /// <returns>Sidekick Call Text</returns>
        T callSideKick();

        /// <summary>
        /// Calls for help
        /// </summary>
        /// <param name="Loudness">How loud in decibles</param>
        string callForHelp(int Loudness);

        

    }
}
