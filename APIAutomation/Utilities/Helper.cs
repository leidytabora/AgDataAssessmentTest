using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Utilities
{
    //Retrieve Method Name
    public class Helper
    {
        public static string GetMethodName([CallerMemberName] string name = "") => name;


    }

}
