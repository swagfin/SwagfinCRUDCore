using System;
using System.Collections.Generic;

namespace SwagfinCRUDCore
{
    public class TableDesign
    {
        public List<TableColumn> Table_Columns { get; set; }
        // #MANDATORY FIELDS
        public string Database_Name { get; set; }
        public string Table_name { get; set; }
        public string Model_name { get; set; }
        public string Unique_identifier { get; set; }
        public string Unique_identifier_param_name { get; set; }
        public string Unique_identifier_datatype_ide { get; set; }
        public string Unique_identifier_datatype_driver { get; set; }
        public string Unique_identifier_datatype_get { get; set; }
        // Optional Value
        //DEPRECATED Now Uses Global Static
        // public string Namespace_name { get; set; }
        // #Added Display Name
        public string Display_table_name { get; set; }
        public string Db_connvariable { get; set; }
        public string Insert_data { get; set; }
        public string Get_datatable_data { get; set; }
        public string Get_list_data { get; set; }
        public string Get_specific_data { get; set; }
        public string Update_specific_data { get; set; }
        public string Delete_specific_data { get; set; }
        public string Get_rowcount_specific { get; set; }
        public string Get_rowcount_all { get; set; }

        /*DEPRECATED PROPERTY
        public string Register_form_name { get; set; }
        public string Register_form_text { get; set; }
        public string Manage_form_name { get; set; }
        public string Manage_form_text { get; set; }
        public string Register_menu_text { get; set; }
        public string Manage_menu_text { get; set; }
        public string Register_save_message_success { get; set; }
        public string Register_save_message_error { get; set; }
        public string Save_button_name { get; set; }
        public string Save_button_text { get; set; }
        */

        #region FindTableIdentifier PrimaryKeys
        public bool FindTableIdentifier_PrimaryKeys(List<TableColumn> PrimaryKeyListColumns)
        {
            try
            {
                //Get First Column
                if (PrimaryKeyListColumns.Count > 0)
                {
                    TableColumn primaryColumn = PrimaryKeyListColumns[0];

                    this.Unique_identifier = primaryColumn.Column_name;
                    this.Unique_identifier_param_name = primaryColumn.Column_param_name;
                    this.Unique_identifier_datatype_ide = primaryColumn.Column_datatype_ide;
                    this.Unique_identifier_datatype_driver = primaryColumn.Column_datatype_driver;
                    this.Unique_identifier_datatype_get = primaryColumn.Column_datatype_get;

                }


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region SantizieDataMap
        public void SanitizeDataMap(ref string DataHeap)
        {
            try
            {
                DataHeap = DataHeap.Replace("{database_name}", this.Database_Name);
                DataHeap = DataHeap.Replace("{namespace}", ModelGenerator.ModelNamespace);
                DataHeap = DataHeap.Replace("{Table_name}", Capitalize_FChar(this.Table_name));
                DataHeap = DataHeap.Replace("{table_name}", this.Table_name);
                DataHeap = DataHeap.Replace("{model_name}", this.Model_name);
                DataHeap = DataHeap.Replace("{unique_identifier}", this.Unique_identifier);
                DataHeap = DataHeap.Replace("{unique_identifier_param_name}", this.Unique_identifier_param_name);
                DataHeap = DataHeap.Replace("{unique_identifier_datatype_ide}", this.Unique_identifier_datatype_ide);
                DataHeap = DataHeap.Replace("{unique_identifier_datatype_get}", this.Unique_identifier_datatype_get);
                DataHeap = DataHeap.Replace("{unique_identifier_datatype_driver}", this.Unique_identifier_datatype_driver);
                DataHeap = DataHeap.Replace("{db_connvariable}", this.Db_connvariable);
                DataHeap = DataHeap.Replace("{display_table_name}", this.Display_table_name);
                //Model Methods
                DataHeap = DataHeap.Replace("{insert_data}", this.Insert_data);
                DataHeap = DataHeap.Replace("{get_datatable_data}", this.Get_datatable_data);
                DataHeap = DataHeap.Replace("{get_list_data}", this.Get_list_data);
                DataHeap = DataHeap.Replace("{get_specific_data}", this.Get_specific_data);
                DataHeap = DataHeap.Replace("{update_specific_data}", this.Update_specific_data);
                DataHeap = DataHeap.Replace("{delete_specific_data}", this.Delete_specific_data);
                DataHeap = DataHeap.Replace("{get_rowcount_specific}", this.Get_rowcount_specific);
                DataHeap = DataHeap.Replace("{get_rowcount_all}", this.Get_rowcount_all);

                /* DEPRECATED PROPERTY
                DataHeap = DataHeap.Replace("{register_form_name}", this.Register_form_name);
                DataHeap = DataHeap.Replace("{register_form_text}", this.Register_form_text);
                DataHeap = DataHeap.Replace("{manage_form_name}", this.Manage_form_name);
                DataHeap = DataHeap.Replace("{manage_form_text}", this.Manage_form_text);
                DataHeap = DataHeap.Replace("{register_menu_text}", this.Register_menu_text);
                DataHeap = DataHeap.Replace("{manage_menu_text}", this.Manage_menu_text);
                DataHeap = DataHeap.Replace("{register_save_message_success}", this.Register_save_message_success);
                DataHeap = DataHeap.Replace("{register_save_message_error}", this.Register_save_message_error);
                DataHeap = DataHeap.Replace("{save_button_name}", this.Save_button_name);
                DataHeap = DataHeap.Replace("{save_button_text}", this.Save_button_text);
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
