using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore
{
    public static class DataHelpers
    {
        public static string Capitalize_FChar(string nameToRebrand)
        {
            try
            {
                char[] array = nameToRebrand.ToCharArray();
                // Uppercase first character.
                array[0] = char.ToUpper(array[0]);
                // Return new string.
                nameToRebrand = new string(array);
                return nameToRebrand;
            }
            catch (Exception)
            {
                return nameToRebrand;
            }
        }

    }
}
