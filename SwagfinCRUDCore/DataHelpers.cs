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
        public static string UnCapitalize_FChar(string nameToRebrand)
        {
            try
            {
                char[] array = nameToRebrand.ToCharArray();
                // Uppercase first character.
                array[0] = char.ToLower(array[0]);
                // Return new string.
                nameToRebrand = new string(array);
                return nameToRebrand;
            }
            catch (Exception)
            {
                return nameToRebrand;
            }
        }



        public static string ReplaceLastChar(string nameToReplace, string SearchFor = "s")
        {
            try
            {
                SearchFor = SearchFor.ToLower();
                //Check if has S
                string result = nameToReplace.Substring(nameToReplace.Length - 1);
                if (result == SearchFor)
                {
                    //Has an s
                    nameToReplace = nameToReplace.Remove(nameToReplace.Length - 1, 1);
                }
            }
            catch (Exception)
            {

            }

            return nameToReplace;

        }


    }
}
