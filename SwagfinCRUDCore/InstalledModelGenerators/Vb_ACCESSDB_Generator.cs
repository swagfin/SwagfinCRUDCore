using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore.InstalledModelGenerators
{
    class Vb_ACCESSDB_Generator : ILanguageModelGenerator
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
                IMPORTS_STRING = "Imports System.Data.OleDb";
                string ALLTEXT = Environment.NewLine + "Public Class " + CurrentTableWithColumns.Model_name;
                // >>>>>SKip #OVERLOAD VARIABLES

                foreach (TableColumn row in CurrentTableWithColumns.Table_Columns)
                {
                    ALLTEXT += Environment.NewLine + "Public Property " + row.Column_name + " As " + row.Column_datatype_ide;
                }
                //---->>Member Properties

                ALLTEXT += Environment.NewLine + "Protected " + CurrentTableWithColumns.Db_connvariable + " As OleDbConnection  = New OleDbConnection(" + QuotesChar + "Provider=Microsoft.Jet.OLEDB.4.0; Microsoft.ACE.OLEDB.12.0;Data Source=" + CurrentTableWithColumns.Database_Name +  ".accd" + QuotesChar + ")";
                ALLTEXT += Environment.NewLine;


                // #End of Class Model Variables
                ALLTEXT += Environment.NewLine + " #Region " + QuotesChar + "Get Rows Count" + QuotesChar;
                ALLTEXT += Environment.NewLine + Generate_ROWCOUNT(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #End Region";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #Region " + QuotesChar + "Get Data" + QuotesChar;
                ALLTEXT += Environment.NewLine + Generate_GET(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #End Region";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #Region " + QuotesChar + "Insert Data to DB" + QuotesChar;
                ALLTEXT += Environment.NewLine + Generate_CREATE(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #End Region";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #Region " + QuotesChar + "Update Data" + QuotesChar;
                ALLTEXT += Environment.NewLine + Generate_UPDATE(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #End Region";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #Region " + QuotesChar + "Get List of Data" + QuotesChar;
                ALLTEXT += Environment.NewLine + Generate_READ(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #End Region";

                ALLTEXT += Environment.NewLine;
                ALLTEXT += Environment.NewLine + " #Region " + QuotesChar + "Delete Data" + QuotesChar;
                ALLTEXT += Environment.NewLine + Generate_DELETE(CurrentTableWithColumns);
                ALLTEXT += Environment.NewLine + " #End Region";

                ALLTEXT += Environment.NewLine + Environment.NewLine + "End Class";

                // #FINALLE DATA |End NameSpace
                FINALE_DATA = IMPORTS_STRING + Environment.NewLine + ALLTEXT;

            }
            catch (Exception)
            {
            }

            return FINALE_DATA;
        }

        #endregion

        #region Generate_Create
        public string Generate_CREATE(TableDesign TableData)
        {
            string feed_data = "";
            try
            {
                string FINALE_CREATE_STRING = "'#Insert Record for Table " + TableData.Table_name + Environment.NewLine;

                // #Function Name
                string FUNCTION_STRING = "Public function " + TableData.Insert_data + " () As Boolean";
                string FULL_MYSQL_QUERRY = "INSERT INTO " + TableData.Table_name + " (";
                string INSERTS_STRING = " VALUES (";
                string MYSQL_COMMAND_PARAMS = "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";

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
                        MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@" + row.Column_name + QuotesChar + ", " + row.Column_datatype_driver + ").Value =" + "Me." + row.Column_name;
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
                FINALE_CREATE_STRING += Environment.NewLine + "Try";

                // #Create Command
                string SQlQ = "Dim Sql as string=" + QuotesChar + FULL_MYSQL_QUERRY + QuotesChar;
                FINALE_CREATE_STRING += Environment.NewLine + SQlQ;
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";
                // #Commands
                FINALE_CREATE_STRING += Environment.NewLine + MYSQL_COMMAND_PARAMS;

                FINALE_CREATE_STRING += Environment.NewLine + "If Command.ExecuteNonQuery() = 1 Then";
                FINALE_CREATE_STRING += Environment.NewLine + "Return True";
                FINALE_CREATE_STRING += Environment.NewLine + "Else";
                FINALE_CREATE_STRING += Environment.NewLine + "Return False";
                FINALE_CREATE_STRING += Environment.NewLine + "End If";

                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Return False";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";

                feed_data = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {
            }
            return feed_data;
        }

        #endregion

        #region Generate_DELETE
        public string Generate_DELETE(TableDesign TableData)
        {

            string feed_back_data = "";
            try
            {
                string FINALE_CREATE_STRING = "'Delete Record from Table " + TableData.Table_name;
                FINALE_CREATE_STRING += Environment.NewLine + "Public function " + TableData.Delete_specific_data + " (" + TableData.Unique_identifier_param_name + " As " + TableData.Unique_identifier_datatype_ide + ") As Boolean" + Environment.NewLine;
                // #finally 
                string GENERATE_sQL_STRING = "DELETE FROM " + TableData.Table_name + " WHERE " + TableData.Unique_identifier + "=@IdentifierKey";
                // TRY GEN
                FINALE_CREATE_STRING += Environment.NewLine + "Try";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim Sql As String=" + QuotesChar + GENERATE_sQL_STRING + QuotesChar;
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";
                FINALE_CREATE_STRING += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@IdentifierKey" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name;

                FINALE_CREATE_STRING += Environment.NewLine + "If Command.ExecuteNonQuery() = 1  Then";
                FINALE_CREATE_STRING += Environment.NewLine + "Return True";
                FINALE_CREATE_STRING += Environment.NewLine + "Else";
                FINALE_CREATE_STRING += Environment.NewLine + "Return False";
                FINALE_CREATE_STRING += Environment.NewLine + "End If";

                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "'MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Return False";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";

                feed_back_data = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {
            }
            return feed_back_data;
        }
        #endregion

        #region Generate_GET

        public string Generate_GET(TableDesign TableData)
        {
            string feed_data = "";
            try
            {
                // #------------------ MAIN GENERATION STARTS HERE-------------
                string FINALE_CREATE_STRING = "'#Get Record Information By " + TableData.Unique_identifier + " for Table " + TableData.Table_name + Environment.NewLine;
                string GET_PARAMS = "";
                GET_PARAMS += "feedback.read()";

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
                    GET_PARAMS += Environment.NewLine + "If feedback.IsDBNull(" + (StatAt - 1).ToString() + ") = False Then Me." + row.Column_name + "=feedback." + row.Column_datatype_get + "(" + (StatAt - 1).ToString() + ")";
                }
                // #finally 
                GENERATE_sQL_STRING += " FROM " + TableData.Table_name + " WHERE " + TableData.Unique_identifier + "=@Identifier";

                string FUNCTION_STRING = "Public function " + TableData.Get_specific_data + " (" + TableData.Unique_identifier_param_name + " As " + TableData.Unique_identifier_datatype_ide + ") As Boolean";
                // #Finale Update
                GET_PARAMS += Environment.NewLine + "feedback.close()";
                FINALE_CREATE_STRING += FUNCTION_STRING + Environment.NewLine;

                // @Commands
                string MYSQL_COMMAND_PARAMS = "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";

                MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@Identifier" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name;
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Try";

                // #Create Command
                string SQlQ = "Dim Sql As String=" + QuotesChar + GENERATE_sQL_STRING + QuotesChar;
                FINALE_CREATE_STRING += Environment.NewLine + SQlQ;
                // #Commands
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";

                FINALE_CREATE_STRING += Environment.NewLine + MYSQL_COMMAND_PARAMS;

                FINALE_CREATE_STRING += Environment.NewLine + "Dim feedback As OleDbDataReader = Command.ExecuteReader()";
                // #Fetch Details
                FINALE_CREATE_STRING += Environment.NewLine + GET_PARAMS;
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Return True";
                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Return False";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";

                feed_data = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {
            }
            return feed_data;
        }

        #endregion

        #region Generate_READ
        public string Generate_READ(TableDesign TableData)
        {
            string feed_data = "";
            try
            {
                string FINALE_CREATE_STRING = "'#Read all Records DataTable for Table " + TableData.Table_name;

                // #-------------------THE SAME SAME CODE WILL BE USED TO GENERATE THAT FOR LIST--------------------
                string RE_USABLE_CODE_BLOCK = "";

                RE_USABLE_CODE_BLOCK += Environment.NewLine + " Dim LIMIT_QUERYY As String =" + QuotesChar + ";" + QuotesChar;

                RE_USABLE_CODE_BLOCK += Environment.NewLine + "If pageSize.Trim().ToUpper() <> " + QuotesChar + "ALL" + QuotesChar + " Then";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "Dim New_pageSize As Integer";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "If Integer.TryParse(pageSize, New_pageSize) Then";
                // #Simple Math
                // @CHECK RECORDS
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "If totalRecords < 1 Then";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "totalRecords =Me." + TableData.Get_rowcount_all;
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "End If";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "Dim totalPages As Decimal = (totalRecords / New_pageSize)";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "totalPages = Math.Ceiling(totalPages)";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "'@Check PageNo is Greater";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "If pageNo > totalPages Then";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "pageNo = totalPages";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "End If";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "'@CALCAUTE WHERE RECORDS WILL START : Algorithim Simple Math";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "Dim recordStart As Integer = ((New_pageSize * pageNo) - New_pageSize)";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "'@Check if Start is Less than Zero";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "If recordStart < 0 Then";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "recordStart = 0";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "End If";

                RE_USABLE_CODE_BLOCK += Environment.NewLine + "'#CHANGE LIMIT";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "LIMIT_QUERYY =" + QuotesChar + " limit " + QuotesChar + "& recordStart & " + QuotesChar + ", " + QuotesChar + "& pageSize & " + QuotesChar + ";" + QuotesChar;

                RE_USABLE_CODE_BLOCK += Environment.NewLine + "End If";
                RE_USABLE_CODE_BLOCK += Environment.NewLine + "End If";
                // #-----------------------END OF RE-USABLE CODE------------------------------------------------------

                FINALE_CREATE_STRING += Environment.NewLine + "Public function " + TableData.Get_datatable_data + " (Optional pageSize As String = " + QuotesChar + "ALL" + QuotesChar + ", Optional pageNo As Integer = 1,  Optional totalRecords As Integer = 0) As DataTable" + Environment.NewLine;


                // #Generate SQL
                int StatAt = 0;
                int StatFinale = TableData.Table_Columns.Count;
                var GENERATE_sQL_STRING = "SELECT ";
                string GENERATED_GET_OBJ = "";
                foreach (TableColumn row in TableData.Table_Columns)
                {
                    StatAt += 1;
                    // #Check Key
                    GENERATE_sQL_STRING += row.Column_name;
                    if (StatAt != StatFinale)
                        GENERATE_sQL_STRING += ",";
                    // #Finale
                    GENERATED_GET_OBJ += Environment.NewLine + "If feedback.IsDBNull(" + (StatAt - 1).ToString() + ") = False Then new_instance." + row.Column_name + "=feedback." + row.Column_datatype_get + "(" + (StatAt - 1).ToString() + ")";
                }
                // #finally 
                GENERATE_sQL_STRING += " FROM " + TableData.Table_name;
                // TRY GEN
                FINALE_CREATE_STRING += Environment.NewLine + "Try";
                // #ATTACH RE-USABLE CODE
                FINALE_CREATE_STRING += Environment.NewLine + RE_USABLE_CODE_BLOCK;

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "Dim Sql as string=" + QuotesChar + GENERATE_sQL_STRING + QuotesChar;
                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "Sql &= LIMIT_QUERYY";

                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim mytable As New DataTable";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim my_adapter As New OleDbDataAdapter(Command)";
                FINALE_CREATE_STRING += Environment.NewLine + "my_adapter.Fill(mytable)";

                FINALE_CREATE_STRING += Environment.NewLine + "Return mytable";

                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Return New DataTable()";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";


                // @----------------------------------LETS ADD OBJECTS METHOD OVERLOAD-----------------

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "'#Read all Data Records In List for for Table " + TableData.Table_name;
                FINALE_CREATE_STRING += Environment.NewLine + "Public function " + TableData.Get_list_data + " (Optional pageSize As String = " + QuotesChar + "ALL" + QuotesChar + ", Optional pageNo As Integer = 1,  Optional totalRecords As Integer = 0) As List(Of " + TableData.Model_name + ")" + Environment.NewLine;
                FINALE_CREATE_STRING += Environment.NewLine + " Dim new_List As List(Of " + TableData.Model_name + ") = New List(Of " + TableData.Model_name + ")";
                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "Try";

                // #ATTACH RE-USABLE CODE
                FINALE_CREATE_STRING += Environment.NewLine + RE_USABLE_CODE_BLOCK;

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "Dim Sql As String=" + QuotesChar + GENERATE_sQL_STRING + QuotesChar;
                FINALE_CREATE_STRING += Environment.NewLine + "Sql &= LIMIT_QUERYY";
                FINALE_CREATE_STRING += Environment.NewLine;

                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";

                FINALE_CREATE_STRING += Environment.NewLine + "Dim feedback As OleDbDataReader = Command.ExecuteReader()";
                FINALE_CREATE_STRING += Environment.NewLine + "While feedback.Read()";
                // ##TRY CATCH HERE
                FINALE_CREATE_STRING += Environment.NewLine + "Try";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim new_instance As " + TableData.Model_name + " = New " + TableData.Model_name + "()";
                FINALE_CREATE_STRING += Environment.NewLine + GENERATED_GET_OBJ;
                FINALE_CREATE_STRING += Environment.NewLine + "new_List.Add(new_instance)";
                // #End TryCATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "'#Handle Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";
                // ##end try
                FINALE_CREATE_STRING += Environment.NewLine + "End While";
                FINALE_CREATE_STRING += Environment.NewLine + "'@CLOSE READER";
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.Close()";


                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";

                FINALE_CREATE_STRING += Environment.NewLine + "Return new_List";
                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";


                feed_data = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {
            }
            return feed_data;
        }

        #endregion

        #region Generate_ROWCOUNT
        public string Generate_ROWCOUNT(TableDesign TableData)
        {
            string final_feedback = "";
            try
            {
                // #------------------ MAIN GENERATION STARTS HERE-------------
                string FINALE_CREATE_STRING = "'#Get Row Count for Table " + TableData.Table_name + Environment.NewLine;


                string FUNCTION_STRING = "Public function " + TableData.Get_rowcount_specific + " (" + TableData.Unique_identifier_param_name + " As " + TableData.Unique_identifier_datatype_ide + ") As Integer";
                FUNCTION_STRING += Environment.NewLine + "Dim data_int as Integer=0";
                FINALE_CREATE_STRING += FUNCTION_STRING + Environment.NewLine;

                string MYSQL_QUERRY = "SELECT count(*) FROM " + TableData.Table_name.Trim() + " WHERE " + TableData.Unique_identifier + "=@Identifier";
                // @Commands
                string MYSQL_COMMAND_PARAMS = "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";

                MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@Identifier" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name;
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Try";

                // #Create Command
                string SQlQ = "Dim Sql as string=" + QuotesChar + MYSQL_QUERRY + QuotesChar;
                FINALE_CREATE_STRING += Environment.NewLine + SQlQ;
                // #Commands
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";

                FINALE_CREATE_STRING += Environment.NewLine + MYSQL_COMMAND_PARAMS;

                FINALE_CREATE_STRING += Environment.NewLine + "Dim feedback As OleDbDataReader = Command.ExecuteReader()";
                // #Fetch Details
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.read()";
                FINALE_CREATE_STRING += Environment.NewLine + "data_int = feedback.GetInt32(0)";
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.Close()";
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";
                FINALE_CREATE_STRING += Environment.NewLine + "Return data_int";
                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";

                // #ADDING COUNT FOR ALL RECORDS
                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "'#Get all Records for Table " + TableData.Table_name;
                FINALE_CREATE_STRING += Environment.NewLine + "Public function " + TableData.Get_rowcount_all + " As Integer";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim data_int as Integer=0";
                FINALE_CREATE_STRING += Environment.NewLine + "Try";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim Sql as string=" + QuotesChar + "SELECT count(*) FROM " + TableData.Table_name.Trim() + QuotesChar;
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";
                FINALE_CREATE_STRING += Environment.NewLine + "Dim feedback As OleDbDataReader = Command.ExecuteReader()";
                // #Fetch Details
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.read()";
                FINALE_CREATE_STRING += Environment.NewLine + "data_int = feedback.GetInt32(0)";
                FINALE_CREATE_STRING += Environment.NewLine + "feedback.Close()";
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "'MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";
                FINALE_CREATE_STRING += Environment.NewLine + "Return data_int";
                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";
                // #OUTPUT FINAL

                final_feedback = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {
            }
            return final_feedback;
        }

        #endregion

        #region Generate_UPDATE
        public string Generate_UPDATE(TableDesign TableData)
        {
            string feed_data = "";
            try
            {
                string FINALE_CREATE_STRING = "'#Update Information by " + TableData.Unique_identifier + " for Table " + TableData.Table_name + Environment.NewLine;

                // #Function Name
                string FULL_MYSQL_QUERRY = "UPDATE " + TableData.Table_name.Trim() + " SET ";
                string MYSQL_COMMAND_PARAMS = "Dim Command As New OleDbCommand(Sql," + TableData.Db_connvariable + ")";

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
                        MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@" + row.Column_name + QuotesChar + ", " + row.Column_datatype_driver + ").Value =" + "Me." + row.Column_name;
                    }
                }

                string FUNCTION_STRING = "Public function " + TableData.Update_specific_data + " (" + TableData.Unique_identifier_param_name + " As " + TableData.Unique_identifier_datatype_ide + ") As Boolean";
                // #Finally
                FULL_MYSQL_QUERRY += " WHERE " + TableData.Unique_identifier + " =@Identifier";
                MYSQL_COMMAND_PARAMS += Environment.NewLine + "Command.Parameters.Add(" + QuotesChar + "@Identifier" + QuotesChar + ", " + TableData.Unique_identifier_datatype_driver + ").Value =" + TableData.Unique_identifier_param_name;
                FINALE_CREATE_STRING += Environment.NewLine + FUNCTION_STRING + Environment.NewLine;
                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Try";

                // #Create Command
                string SQlQ = "Dim Sql as string=" + QuotesChar + FULL_MYSQL_QUERRY + QuotesChar;
                FINALE_CREATE_STRING += Environment.NewLine + SQlQ;
                // #Execution
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Open()";
                // #Commands
                FINALE_CREATE_STRING += Environment.NewLine + MYSQL_COMMAND_PARAMS;

                FINALE_CREATE_STRING += Environment.NewLine + "If Command.ExecuteNonQuery() = 1 Then";
                FINALE_CREATE_STRING += Environment.NewLine + "Return True";
                FINALE_CREATE_STRING += Environment.NewLine + "Else";
                FINALE_CREATE_STRING += Environment.NewLine + "Return False";
                FINALE_CREATE_STRING += Environment.NewLine + "End If";

                // #Try CATCH
                FINALE_CREATE_STRING += Environment.NewLine + "Catch ex As Exception";
                FINALE_CREATE_STRING += Environment.NewLine + "MsgBox(ex.Message)";
                FINALE_CREATE_STRING += Environment.NewLine + "Return False";
                FINALE_CREATE_STRING += Environment.NewLine + "Finally";
                FINALE_CREATE_STRING += Environment.NewLine + TableData.Db_connvariable + ".Close()";
                FINALE_CREATE_STRING += Environment.NewLine + "End Try";

                FINALE_CREATE_STRING += Environment.NewLine + Environment.NewLine + "End Function";

                feed_data = FINALE_CREATE_STRING;
            }
            catch (Exception)
            {
            }
            return feed_data;
        }

        #endregion

        #region Load_TableColumn_Drivers
        public bool Load_TableColumn_Drivers(ref TableColumn TableColumn)
        {
            try
            {
                if (TableColumn.Data_type == "int")
                {
                    TableColumn.Column_datatype_ide = "Integer";
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
                    TableColumn.Column_datatype_ide = "Double";
                    TableColumn.Column_datatype_get = "GetDouble";
                    TableColumn.Column_datatype_driver = "OleDbType.Double";
                }
                else if (TableColumn.Data_type == "decimal")
                {
                    TableColumn.Column_datatype_ide = "Decimal";
                    TableColumn.Column_datatype_get = "GetDecimal";
                    TableColumn.Column_datatype_driver = "OleDbType.Decimal";
                }
                else if (TableColumn.Data_type == "float")
                {
                    TableColumn.Column_datatype_ide = "Double";
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
                    TableColumn.Column_datatype_ide = "String";
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
