using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore.InstalledModelGenerators
{
    class Csharp_ACCESSDB_Generator : ILanguageModelGenerator
    {
        protected static string QuotesChar = char.ConvertFromUtf32(34);

        #region Get_GeneratedModel
        public string Get_GeneratedModel(TableDesign CurrentTableWithColumns, string ModelNameSpace = "swagfin.Models")
        {

            string FINALE_DATA = "";
            try
            {
                string IMPORTS_STRING = "";
                // #Determine Imports
                IMPORTS_STRING = "using System;" + Environment.NewLine;
                IMPORTS_STRING += "using System.Data.OleDb;" + Environment.NewLine;
                IMPORTS_STRING += "using System.Data;" + Environment.NewLine;
                IMPORTS_STRING += "using System.Collections;" + Environment.NewLine;
                IMPORTS_STRING += "using System.Collections.Generic;" + Environment.NewLine;

                IMPORTS_STRING += Environment.NewLine + "namespace " + ModelNameSpace.ToString().Trim() + Environment.NewLine + "{";

                string ALLTEXT = Environment.NewLine + "public class " + CurrentTableWithColumns.Model_name + "{";
                // >>>>>SKip #OVERLOAD VARIABLES

                foreach (TableColumn row in CurrentTableWithColumns.Table_Columns)
                {
                    ALLTEXT += Environment.NewLine + "public " + row.Column_datatype_ide + " " + row.Column_name + " {get;set;}";
                }
                //---->>Member Properties

                ALLTEXT += Environment.NewLine + "protected  OleDbConnection " + CurrentTableWithColumns.Db_connvariable + " = new OleDbConnection(" + QuotesChar + "Provider=Microsoft.Jet.OLEDB.4.0; Microsoft.ACE.OLEDB.12.0;Data Source=" + CurrentTableWithColumns.Database_Name + ".accd" + QuotesChar + ");";
                ALLTEXT += Environment.NewLine;

                // #End of Class Model Variables
                ALLTEXT += Environment.NewLine + " #region Get Rows Count";
                ALLTEXT += Environment.NewLine + Generate_ROWCOUNT(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #endregion";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #region Get Data";
                ALLTEXT += Environment.NewLine + Generate_GET(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #endregion";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #region Insert Data to DB";
                ALLTEXT += Environment.NewLine + Generate_CREATE(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #endregion";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #region Update Data";
                ALLTEXT += Environment.NewLine + Generate_UPDATE(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #endregion";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #region Get List of Data";
                ALLTEXT += Environment.NewLine + Generate_READ(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #endregion";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #region Delete Data";
                ALLTEXT += Environment.NewLine + Generate_DELETE(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #endregion";

                ALLTEXT += Environment.NewLine + "}";

                // #FINALLE DATA |End NameSpace
                FINALE_DATA = IMPORTS_STRING + Environment.NewLine + ALLTEXT + Environment.NewLine + "}";

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
                string MYSQL_COMMAND_PARAMS = "OleDbCommand Command = new OleDbCommand(Sql," + TableData.Db_connvariable + ");";

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

        public string Generate_DELETE(TableDesign TableData)
        {
            string feed_back_data = "";
            try
            {
                string FINALE_CREATE_STRING = "//#Delete Record operation for Table " + TableData.Table_name;
                FINALE_CREATE_STRING += Environment.NewLine + "public bool " + TableData.Delete_specific_data + " (" + TableData.Unique_identifier_datatype_ide + " " + TableData.Unique_identifier_param_name + ")" + Environment.NewLine + "{" + Environment.NewLine;
                // #finally 
                string GENERATE_sQL_STRING = "DELETE FROM " + TableData.Table_name + " WHERE " + TableData.Unique_identifier + "=@IdentifierKey;";
                // TRY GEN
                FINALE_CREATE_STRING += Environment.NewLine + "try";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + "string Sql =" + QuotesChar + GENERATE_sQL_STRING + QuotesChar + ";";
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open();";
                FINALE_CREATE_STRING += Environment.NewLine + "OleDbCommand Command = new OleDbCommand(Sql," + TableData.Db_connvariable + ");";
                FINALE_CREATE_STRING += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@IdentifierKey" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name + ";";

                FINALE_CREATE_STRING += Environment.NewLine + "if (Command.ExecuteNonQuery() == 1)";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + "return true;";
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "else";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + "return false;";
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                // @End Try
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

                feed_back_data = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {
            }
            return feed_back_data;
        }

        #endregion

        #region Generate Get
        public string Generate_GET(TableDesign TableData)
        {
            string feed_data = "";
            try
            {

                // #------------------ MAIN GENERATION STARTS HERE-------------
                string FINALE_CREATE_STRING = "//#Get record information By " + TableData.Unique_identifier + " for Table" + TableData.Table_name + Environment.NewLine;
                string GET_PARAMS = "";
                GET_PARAMS += "feedback.Read();";

                // #Generate Querry
                int StatAt = 0;
                int StatFinale = TableData.Table_Columns.Count;
                var GENERATE_sQL_STRING = "SELECT ";
                foreach (TableColumn row in TableData.Table_Columns)
                {
                    StatAt += 1;
                    // #Check Key
                    GENERATE_sQL_STRING += row.Column_name;
                    if (StatAt != StatFinale)
                        GENERATE_sQL_STRING += ",";
                    // #Finale
                    int param_id = StatAt - 1;
                    GET_PARAMS += Environment.NewLine + "if (feedback.IsDBNull(" + param_id.ToString() + ") == false) { this." + row.Column_name + "=feedback." + row.Column_datatype_get + "(" + param_id.ToString() + "); }";
                }
                // #finally 
                GENERATE_sQL_STRING += " FROM " + TableData.Table_name + " WHERE " + TableData.Unique_identifier + "=@Identifier";

                string FUNCTION_STRING = "public bool " + TableData.Get_specific_data + " (" + TableData.Unique_identifier_datatype_ide + " " + TableData.Unique_identifier_param_name + ") {";
                // #Finale Update
                GET_PARAMS += Environment.NewLine + "feedback.Close();";
                FINALE_CREATE_STRING += FUNCTION_STRING + Environment.NewLine;

                // @Commands
                string MYSQL_COMMAND_PARAMS = "OleDbCommand Command = new OleDbCommand(Sql," + TableData.Db_connvariable + ");";

                MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@Identifier" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name + ";";

                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "try" + "{";

                // #Create Command
                string SQlQ = "String Sql =" + QuotesChar + GENERATE_sQL_STRING + QuotesChar + ";";
                FINALE_CREATE_STRING += Environment.NewLine + SQlQ;
                // #Commands
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open();";

                FINALE_CREATE_STRING += Environment.NewLine + MYSQL_COMMAND_PARAMS;

                FINALE_CREATE_STRING += Environment.NewLine + "OleDbDataReader feedback = Command.ExecuteReader();";
                // #Fetch Details
                FINALE_CREATE_STRING += Environment.NewLine + GET_PARAMS;
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "return true;";
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "catch (Exception ex)";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + " throw new Exception(ex.Message);";
                //->>FINALE_CREATE_STRING += Environment.NewLine + "return false;";
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

        #region Generate Read
        public string Generate_READ(TableDesign TableData)
        {
            string feed_data = "";
            try
            {
                string FINALE_CREATE_STRING = "//#Read all records DataTable for Table " + TableData.Table_name;
                FINALE_CREATE_STRING += Environment.NewLine + "public DataTable " + TableData.Get_datatable_data + "(string pageSize = " + QuotesChar + "ALL" + QuotesChar + ", decimal pageNo = 1, decimal totalRecords = 0)" + Environment.NewLine + "{" + Environment.NewLine;


                // #Generate SQL
                int StatAt = 0;
                int StatFinale = TableData.Table_Columns.Count;
                var GENERATE_sQL_STRING = "SELECT ";
                string GENERATED_GET_OBJ = "";
                foreach (var row in TableData.Table_Columns)
                {
                    StatAt += 1;
                    // #Check Key
                    GENERATE_sQL_STRING += row.Column_name;
                    if (StatAt != StatFinale)
                        GENERATE_sQL_STRING += ",";
                    // #Finale
                    GENERATED_GET_OBJ += Environment.NewLine + "if (feedback.IsDBNull(" + (StatAt - 1).ToString() + ") == false) { new_instance." + row.Column_name + "=feedback." + row.Column_datatype_get + "(" + (StatAt - 1).ToString() + "); }";
                }
                // #finally 
                GENERATE_sQL_STRING += " FROM " + TableData.Table_name;
                // TRY GEN
                FINALE_CREATE_STRING += Environment.NewLine + "try";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "string Sql =" + QuotesChar + GENERATE_sQL_STRING + QuotesChar + ";";
                FINALE_CREATE_STRING += Environment.NewLine;

                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open();";
                FINALE_CREATE_STRING += Environment.NewLine + "OleDbCommand Command= new OleDbCommand(Sql," + TableData.Db_connvariable + ");";
                FINALE_CREATE_STRING += Environment.NewLine + "DataTable mytable = new DataTable();";
                FINALE_CREATE_STRING += Environment.NewLine + "OleDbDataAdapter my_adapter = new OleDbDataAdapter(Command);";
                FINALE_CREATE_STRING += Environment.NewLine + "my_adapter.Fill(mytable);";

                FINALE_CREATE_STRING += Environment.NewLine + "return mytable;";
                FINALE_CREATE_STRING += Environment.NewLine + "}";

                FINALE_CREATE_STRING += Environment.NewLine + "catch (Exception ex)";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + " throw new Exception(ex.Message);";
                //->>FINALE_CREATE_STRING += Environment.NewLine + "return new DataTable();";
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

        #region Generate RowCount
        public string Generate_ROWCOUNT(TableDesign TableData)
        {
            string final_feedback = "";
            try
            {
                // #------------------ MAIN GENERATION STARTS HERE-------------
                string FINALE_CREATE_STRING = "//#THIS THE ROW COUNT OPERATION FOR TABLE " + TableData.Table_name + Environment.NewLine;


                string FUNCTION_STRING = "public int " + TableData.Get_rowcount_specific + " (" + TableData.Unique_identifier_datatype_ide + " " + TableData.Unique_identifier_param_name + ")" + "{";
                FUNCTION_STRING += Environment.NewLine + "int data_int =0;";
                FINALE_CREATE_STRING += FUNCTION_STRING + Environment.NewLine;

                string MYSQL_QUERRY = "SELECT count(*) FROM " + TableData.Table_name.Trim() + " WHERE " + TableData.Unique_identifier + "=@Identifier;";
                // @Commands
                string MYSQL_COMMAND_PARAMS = "OleDbCommand Command = new OleDbCommand(Sql," + TableData.Db_connvariable + ");";

                MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@Identifier" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name + ";";
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "try";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                // #Create Command
                string SQlQ = "string Sql =" + QuotesChar + MYSQL_QUERRY + QuotesChar + ";";
                FINALE_CREATE_STRING += Environment.NewLine + SQlQ;
                // #Commands
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open();";

                FINALE_CREATE_STRING += Environment.NewLine + MYSQL_COMMAND_PARAMS;

                FINALE_CREATE_STRING += Environment.NewLine + "OleDbDataReader feedback = Command.ExecuteReader();";
                // #Fetch Details
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.Read();";
                FINALE_CREATE_STRING += Environment.NewLine + "data_int = feedback.GetInt32(0);";
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.Close();";
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "catch (Exception ex)";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + " throw new Exception(ex.Message); ";
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "finally";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close();";
                FINALE_CREATE_STRING += Environment.NewLine + "}";

                FINALE_CREATE_STRING += Environment.NewLine + "return data_int;";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "}";


                // #-------------------------------------GET ALL FUNCTION | GET ALL RECORDS-----------------------------------------------

                FINALE_CREATE_STRING += Environment.NewLine + "//#GET ALL COUNT";
                FINALE_CREATE_STRING += Environment.NewLine + "public int " + TableData.Get_rowcount_all + "()" + Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + "int data_int = 0;";
                FINALE_CREATE_STRING += Environment.NewLine + "try";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                // #Create Command
                FINALE_CREATE_STRING += Environment.NewLine + "string Sql =" + QuotesChar + "SELECT count(*) FROM " + TableData.Table_name + QuotesChar + ";";
                // #Commands
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open();";

                FINALE_CREATE_STRING += Environment.NewLine + "OleDbCommand Command = new OleDbCommand(Sql," + TableData.Db_connvariable + ");";

                FINALE_CREATE_STRING += Environment.NewLine + "OleDbDataReader feedback = Command.ExecuteReader();";
                // #Fetch Details
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.Read();";
                FINALE_CREATE_STRING += Environment.NewLine + "data_int = feedback.GetInt32(0);";
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.Close();";
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "catch (Exception ex)";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + " throw new Exception(ex.Message); ";
                FINALE_CREATE_STRING += Environment.NewLine + "}";
                FINALE_CREATE_STRING += Environment.NewLine + "finally";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close();";
                FINALE_CREATE_STRING += Environment.NewLine + "}";

                FINALE_CREATE_STRING += Environment.NewLine + "return data_int;";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "}";


                final_feedback = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {

            }
            return final_feedback;
        }

        #endregion

        #region Generate Update
        public string Generate_UPDATE(TableDesign TableData)
        {
            string feed_data = "";
            try
            {
                string FINALE_CREATE_STRING = "//#Update Record by " + TableData.Unique_identifier + " for Table " + TableData.Table_name + Environment.NewLine;

                // #Function Name
                string FULL_MYSQL_QUERRY = "UPDATE " + TableData.Table_name.Trim() + " SET ";
                string MYSQL_COMMAND_PARAMS = "OleDbCommand Command =new OleDbCommand(Sql," + TableData.Db_connvariable + ");";

                int StatAt = 0;
                foreach (TableColumn row in TableData.Table_Columns)
                {
                    if (row.Extra != "auto_increment")
                    {
                        StatAt += 1;
                        // #We Dont Need to Update Unique ID
                        if (StatAt != 1)
                            FULL_MYSQL_QUERRY += ",";
                        FULL_MYSQL_QUERRY += row.Column_name + "=" + "@" + row.Column_name;
                        MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@" + row.Column_name + QuotesChar + ", " + row.Column_datatype_driver + ").Value =" + "this." + row.Column_name + ";";
                    }
                }

                string FUNCTION_STRING = "public bool " + TableData.Update_specific_data + " (" + TableData.Unique_identifier_datatype_ide + " " + TableData.Unique_identifier_param_name + ")";

                // #Finally
                FULL_MYSQL_QUERRY += " WHERE " + TableData.Unique_identifier + " =@Identifier";
                MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@Identifier" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name + ";";
                FINALE_CREATE_STRING += Environment.NewLine + FUNCTION_STRING;
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "try";
                FINALE_CREATE_STRING += Environment.NewLine + "{";
                // #Create Command
                string SQlQ = "string Sql =" + QuotesChar + FULL_MYSQL_QUERRY + QuotesChar + ";";
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
                //->>FINALE_CREATE_STRING += Environment.NewLine + "return false;";
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

        #region TableDrivers
        public bool Load_TableColumn_Drivers(ref TableColumn TableColumn)
        {
            try
            {

                if (TableColumn.Data_type == "int" || TableColumn.Data_type == "tinyint" || TableColumn.Data_type == "smallint" || TableColumn.Data_type == "smallint")
                {
                    TableColumn.Column_datatype_ide = "int";
                    TableColumn.Column_datatype_get = "GetInt32";
                    TableColumn.Column_datatype_driver = "OleDbType.Integer";

                }
                else if (TableColumn.Data_type == "datetime" | TableColumn.Data_type == "timestamp" | TableColumn.Data_type == "time")
                {
                    TableColumn.Column_datatype_ide = "DateTime";
                    TableColumn.Column_datatype_get = "GetDateTime";
                    TableColumn.Column_datatype_driver = "OleDbType.DBTimeStamp";

                }
                else if (TableColumn.Data_type == "double")
                {
                    TableColumn.Column_datatype_ide = "double";
                    TableColumn.Column_datatype_get = "GetDouble";
                    TableColumn.Column_datatype_driver = "OleDbType.Double";
                }
                else if (TableColumn.Data_type == "decimal")
                {
                    TableColumn.Column_datatype_ide = "decimal";
                    TableColumn.Column_datatype_get = "GetDecimal";
                    TableColumn.Column_datatype_driver = "OleDbType.Decimal";

                }
                else if (TableColumn.Data_type == "float")
                {
                    TableColumn.Column_datatype_ide = "float";
                    TableColumn.Column_datatype_get = "Double";
                    TableColumn.Column_datatype_driver = "OleDbType.Double";

                }
                else if (TableColumn.Data_type == "date" | TableColumn.Data_type == "year")
                {
                    TableColumn.Column_datatype_ide = "DateTime";
                    TableColumn.Column_datatype_get = "GetDateTime";
                    TableColumn.Column_datatype_driver = "OleDbType.Date";

                }
                else
                {
                    TableColumn.Column_datatype_ide = "string";
                    TableColumn.Column_datatype_get = "GetString";
                    TableColumn.Column_datatype_driver = "OleDbType.VarChar";

                }

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
