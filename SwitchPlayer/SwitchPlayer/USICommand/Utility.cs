using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    public static class Utility
    {
        public static string FirstToken(this string str)
        {
            if (str == null)
                return null;
            string[] split = str.Split(' ');
            if (split.Length == 0)
                return null;
            return split[0];
        }
    }
}
