using System;
using System.Collections.Generic;

namespace SwagfinCRUDCore.InstalledModelGenerators
{
    class CSharpPlainEntityGenerator : ILanguageModelGenerator
    {
        protected static string QuotesChar = char.ConvertFromUtf32(34);

        #region Get_GeneratedModel
        public string Get_GeneratedModel(TableDesign CurrentTableWithColumns, string ModelNameSpace = "swagfin.Models")
        {



            string FINALE_DATA = "";
            try
            {
                //Check Name
                string className = CurrentTableWithColumns.Table_name;
                if (ModelGenerator.SingularizeTableNames)
                    className = DataHelpers.ReplaceLastChar(className);


                string IMPORTS_STRING = @"
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace {namespace}.Entity
{

    public class {Table_name}
   {
    //{ClassProperties}
   }

}
";

                string classProperties = string.Empty;
                foreach (TableColumn row in CurrentTableWithColumns.Table_Columns)
                {
                    classProperties += Environment.NewLine + "      public " + row.Column_datatype_ide + " " + row.Column_name + " { get; set; }";
                }

                //Replaces
                IMPORTS_STRING = IMPORTS_STRING.Replace("//{ClassProperties}", classProperties);
                IMPORTS_STRING = IMPORTS_STRING.Replace("{namespace}", ModelNameSpace.ToString().Trim());
                IMPORTS_STRING = IMPORTS_STRING.Replace("{Table_name}", DataHelpers.Capitalize_FChar(className));
                IMPORTS_STRING = IMPORTS_STRING.Replace("{table_name}", className);


                FINALE_DATA = IMPORTS_STRING;

            }
            catch (Exception)
            {
            }

            return FINALE_DATA;
        }

        #endregion

        #region Generate Create
        public string Generate_CREATE(TableDesign TableData)
        {
            string feed_data = "";
            try
            {
                string FINALE_CREATE_STRING = "//#Insert Operation for Table " + TableData.Table_name + Environment.NewLine;

                // #Function Name
                string FUNCTION_STRING = "public bool " + TableData.Insert_data + " () " + Environment.NewLine + "{";
                string FULL_MYSQL_QUERRY = "INSERT INTO " + TableData.Table_name + " (";
                string INSERTS_STRING = " VALUES (";
                string MYSQL_COMMAND_PARAMS = "MySqlCommand Command = new MySqlCommand(Sql," + TableData.Db_connvariable + ");";

                int StatAt = 0;
                int StatFinale = TableData.Table_Columns.Count;
                foreach (TableColumn row in TableData.Table_Columns)
                {
                    StatAt += 1;
                    if (row.Extra != "auto_increment")
                    {
                        // #Check Key
                        FULL_MYSQL_QUERRY += row.Column_name;
                        // #Inserts
                        INSERTS_STRING += "@" + row.Column_name;
                        // #FunctionParam
                        MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@" + row.Column_name + QuotesChar + ", " + row.Column_datatype_driver + ").Value =" + "this." + row.Column_name + ";";
                        if (StatAt != StatFinale)
                        {
                            FULL_MYSQL_QUERRY += ",";
                            INSERTS_STRING += ",";
                        }
                    }
                }
                // #Finally
                FULL_MYSQL_QUERRY += ")";
                INSERTS_STRING += ")";
                // #MERGE INSERT TO MAIN
                FULL_MYSQL_QUERRY += INSERTS_STRING;

                FINALE_CREATE_STRING += FUNCTION_STRING + Environment.NewLine;
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "try";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                // #Create Command
                string SQlQ = "string Sql=" + QuotesChar + FULL_MYSQL_QUERRY + QuotesChar + ";";
                FINALE_CREATE_STRING += Environment.NewLine + SQlQ;
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open();";
                // #Commands
                FINALE_CREATE_STRING += Environment.NewLine + MYSQL_COMMAND_PARAMS;

                FINALE_CREATE_STRING += Environment.NewLine + "if (Command.ExecuteNonQuery() == 1)";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + "return true;";
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "else";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + "return false;";
                FINALE_CREATE_STRING += Environment.NewLine + "}";

                FINALE_CREATE_STRING += Environment.NewLine + "}";
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "catch (Exception ex)";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + " throw new Exception(ex.Message); ";
                //->> FINALE_CREATE_STRING += Environment.NewLine + "return false;";
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "finally";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close();";
                FINALE_CREATE_STRING += Environment.NewLine + "}";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "}";

                feed_data = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {

            }
            return feed_data;
        }

        #endregion

        #region Generate Delete

        public string Generate_DELETE(TableDesign TableData) => string.Empty;

        #endregion

        #region Generate Get
        public string Generate_GET(TableDesign TableData) => string.Empty;
        #endregion

        #region Generate Read
        public string Generate_READ(TableDesign TableData) => string.Empty;
        #endregion

        #region Generate RowCount
        public string Generate_ROWCOUNT(TableDesign TableData) => string.Empty;
        #endregion

        #region Generate Update
        public string Generate_UPDATE(TableDesign TableData) => string.Empty;

        #endregion
        #region TableDrivers
        public bool Load_TableColumn_Drivers(ref TableColumn TableColumn)
        {
            try
            {
                //TINYINT,SMALLINT,MEDIUMINT

                if (TableColumn.Data_type == "int" || TableColumn.Data_type == "smallint" || TableColumn.Data_type == "smallint")
                {
                    TableColumn.Column_datatype_ide = "int";
                    TableColumn.Column_datatype_get = "GetInt32";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Int32";

                }
                else if (TableColumn.Data_type == "tinyint")
                {
                    TableColumn.Column_datatype_ide = "bool";
                    TableColumn.Column_datatype_get = "GetInt32";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Int32";
                }
                else if (TableColumn.Data_type == "datetime" | TableColumn.Data_type == "timestamp" | TableColumn.Data_type == "time")
                {
                    TableColumn.Column_datatype_ide = "DateTime";
                    TableColumn.Column_datatype_get = "GetDateTime";
                    TableColumn.Column_datatype_driver = "MySqlDbType.DateTime";

                }
                else if (TableColumn.Data_type == "double")
                {
                    TableColumn.Column_datatype_ide = "double";
                    TableColumn.Column_datatype_get = "GetDouble";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Double";
                }
                else if (TableColumn.Data_type == "decimal")
                {
                    TableColumn.Column_datatype_ide = "decimal";
                    TableColumn.Column_datatype_get = "GetDecimal";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Decimal";

                }
                else if (TableColumn.Data_type == "float")
                {
                    TableColumn.Column_datatype_ide = "float";
                    TableColumn.Column_datatype_get = "Double";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Float";

                }
                else if (TableColumn.Data_type == "date" | TableColumn.Data_type == "year")
                {
                    TableColumn.Column_datatype_ide = "DateTime";
                    TableColumn.Column_datatype_get = "GetDateTime";
                    TableColumn.Column_datatype_driver = "MySqlDbType.DateTime";

                }
                else
                {
                    TableColumn.Column_datatype_ide = "string";
                    TableColumn.Column_datatype_get = "GetString";
                    TableColumn.Column_datatype_driver = "MySqlDbType.VarChar";

                }

                if (TableColumn.Is_nullable == "YES" && TableColumn.Column_datatype_ide != "string")
                    TableColumn.Column_datatype_ide += "?";

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
