using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore
{
    public class TableColumn
    {
        //#Default Columns
        public int Column_id { get; set; }
        public string Table_name { get; set; }
        public string Column_name { get; set; }
        public string Data_type { get; set; }
        public string Column_key { get; set; }
        public string Is_nullable { get; set; }
        public string Extra { get; set; }

        //#Column Properties
        public string Column_display { get; set; }
        public string Column_param_name { get; set; }
        public string Column_datatype_ide { get; set; }
        public string Column_datatype_get { get; set; }
        public string Column_datatype_driver { get; set; }
        //@New Properties in New Version
        public string Referenced_table_name { get; set; }
        public string Referenced_column_name { get; set; }
        public string Required { get; set; }
        /* DEPRECARED PROPERTY
        public string Column_textbox_name { get; set; }
        public string Column_label { get; set; }
        */


        #region SanitizeDataMap
        public void SanitizeDataMap(ref string DataHeap)
        {
            try
            {
                DataHeap = DataHeap.Replace("{Table_name}", Capitalize_FChar(this.Table_name));
                DataHeap = DataHeap.Replace("{column_id}", this.Column_id.ToString());
                DataHeap = DataHeap.Replace("{table_name}", this.Table_name);
                DataHeap = DataHeap.Replace("{column_name}", this.Column_name);
                DataHeap = DataHeap.Replace("{data_type}", this.Data_type);
                DataHeap = DataHeap.Replace("{column_key}", this.Column_key);
                DataHeap = DataHeap.Replace("{is_nullable}", this.Is_nullable);
                DataHeap = DataHeap.Replace("{extra}", this.Extra);
                //#New Properties
                DataHeap = DataHeap.Replace("{referenced_table_name}", this.Referenced_table_name);
                DataHeap = DataHeap.Replace("{Referenced_table_name}", Capitalize_FChar(this.Referenced_table_name));
                DataHeap = DataHeap.Replace("{referenced_column_name}", this.Referenced_column_name);
                DataHeap = DataHeap.Replace("{required}", this.Required);
                //Others
                DataHeap = DataHeap.Replace("{column_display}", this.Column_display);
                DataHeap = DataHeap.Replace("{column_param_name}", this.Column_param_name);
                DataHeap = DataHeap.Replace("{column_datatype_ide}", this.Column_datatype_ide);
                DataHeap = DataHeap.Replace("{column_datatype_get}", this.Column_datatype_get);
                DataHeap = DataHeap.Replace("{column_datatype_driver}", this.Column_datatype_driver);
                /*DEPRECATED PROPERTY
                DataHeap = DataHeap.Replace("{column_textbox_name}", this.Column_textbox_name);
                DataHeap = DataHeap.Replace("{column_label}", this.Column_label);
                */
            }
            catch (Exception)
            {

            }
        }
        #endregion


        #region Capitalize_FChar First Character
        public string Capitalize_FChar(string nameToRebrand)
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

        #endregion

    }
}
