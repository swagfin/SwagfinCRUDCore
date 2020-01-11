using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SwagfinCRUDCore
{
    public class ModelGenerator
    {
        protected DatabaseConfiguration DBConfiguration { get; set; }
        protected SupportedEngine SupportedDBEngine { get; set; }
        protected string[] DatabaseNameExceptions { get; set; }
        public static string ModelNamespace { get; set; }
        public static bool SingularizeTableNames { get; set; } = false;
        public static bool AddAsNoTracking { get; set; } = false;


        public ModelGenerator(DatabaseConfiguration databaseConfig, SupportedEngine supportedDBEngine, string modelNameSpace = "SwagfinGrud", string[] databaseNamesExceptions = null, bool singularizeTableNames = false, bool addAsNoTracking = false)
        {
            this.DBConfiguration = databaseConfig;
            this.SupportedDBEngine = supportedDBEngine;

            ModelNamespace = modelNameSpace;
            SingularizeTableNames = singularizeTableNames;
            AddAsNoTracking = addAsNoTracking;
            //Load Exceptions Received
            if (databaseNamesExceptions == null) { this.DatabaseNameExceptions = Get_DefaultDBName_Exceptions(); }
            else { this.DatabaseNameExceptions = databaseNamesExceptions; }
        }



        //---------------------------DATABASE ASSOCIATION----------------



        #region Get_Databases
        public List<DatabaseDesign> Get_Databases()
        {
            MySqlConnection appConnection = new MySqlConnection(this.DBConfiguration.Get_ConnectionString());
            List<DatabaseDesign> all_db = new List<DatabaseDesign>();
            try
            {
                string MySqlQuery = "SHOW DATABASES";
                appConnection.Open();
                MySqlCommand command = new MySqlCommand(MySqlQuery, appConnection);
                MySqlDataReader feedback = command.ExecuteReader();

                while (feedback.Read())
                {
                    try
                    {
                        DatabaseDesign new_db = new DatabaseDesign
                        {
                            Database_Name = feedback.GetString("Database"),
                            Server_Address = DBConfiguration.MySQL_server,
                            Database_Port = DBConfiguration.MySQL_Port,
                            Last_Sync = DateTime.Now
                        };

                        if (this.DatabaseNameExceptions.Contains(new_db.Database_Name) == false)
                            all_db.Add(new_db);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
                feedback.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                appConnection.Close();
            }

            return all_db;
        }

        #endregion

        #region Get_SingleTableWithColumns
        public TableDesign Get_SingleTableWithColumns(string DatabaseName, string TableName)
        {
            try
            {
                TableDesign new_table = new TableDesign();
                new_table.Database_Name = DatabaseName;
                new_table.Table_name = TableName;
                new_table.Origin_Table_name = TableName;
                //Check Singular or Multiple
                if (SingularizeTableNames)
                    new_table.Table_name = DataHelpers.ReplaceLastChar(new_table.Table_name);
                //->> Properties
                new_table.Model_name = new_table.Table_name.Replace(" ", "");
                new_table.Model_name = new_table.Model_name.Replace("_", "");
                //new_table.Model_name = Capitalize_FChar(new_table.Model_name).Trim() + "Model";
                new_table.Model_name = Capitalize_FChar(new_table.Model_name).Trim();
                //----LAST-------> Will Find Last new_table.unique_identifier = UniqueIdentifier;
                //....LAST.....>new_table.unique_identifier_param_name = UniqueIdentifier_Param;
                //....LAST.....>unique_identifier_datatype_ide = UniqueIdentifier_type_IDE;
                //....LAST...>new_table.unique_identifier_datatype_driver = UniqueIdentifier_Type_Driver

                string tablename_spaced = new_table.Table_name.Replace("_", " ");
                // #METHOD NAMING CONVENTIONS
                new_table.Db_connvariable = "AppConnection";
                new_table.Insert_data = "Insert_" + new_table.Table_name;
                new_table.Get_datatable_data = "Read_" + new_table.Table_name;
                new_table.Get_list_data = "Read_" + new_table.Table_name + "_LIST";
                new_table.Get_specific_data = "Get_" + new_table.Table_name;
                new_table.Update_specific_data = "Update_" + new_table.Table_name;
                new_table.Delete_specific_data = "Delete_" + new_table.Table_name;
                new_table.Get_rowcount_specific = "Get_RowCount_" + new_table.Table_name;
                new_table.Get_rowcount_all = "Get_RowCount_" + new_table.Table_name + "_ALL";
                new_table.Display_table_name = Capitalize_FChar(tablename_spaced);
                //----->>Find Columns Keys | Find Primary Key, Unique Identifier, DataType ETC
                new_table.FindTableIdentifier_PrimaryKeys(this.Get_DatabaseTableColumnsPrimaryKeys(DatabaseName, TableName));

                new_table.Table_Columns = this.Get_DatabaseTableColumns(DatabaseName, TableName);

                return new_table;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        #endregion

        #region Get_DatabaseTablesWithOutColumns
        public List<TableDesign> Get_DatabaseTablesWithOutColumns(string DatabaseNameSchema)
        {
            List<TableDesign> new_tables = new List<TableDesign>();
            MySqlConnection appConnection = new MySqlConnection(this.DBConfiguration.Get_ConnectionString());
            try
            {
                string MySqlQuery = "SHOW TABLES FROM " + DatabaseNameSchema.Trim();
                appConnection.Open();
                MySqlCommand command = new MySqlCommand(MySqlQuery, appConnection);
                MySqlDataReader feedback = command.ExecuteReader();
                while (feedback.Read())
                {
                    try
                    {
                        TableDesign new_table = new TableDesign();
                        new_table.Database_Name = DatabaseNameSchema;
                        new_table.Origin_Table_name = feedback.GetString("Tables_in_" + DatabaseNameSchema);
                        new_table.Table_name = new_table.Origin_Table_name;
                        //Check Singular or Multiple
                        string TableName = new_table.Table_name;
                        if (SingularizeTableNames)
                            new_table.Table_name = DataHelpers.ReplaceLastChar(new_table.Table_name);
                        //->> Properties
                        new_table.Model_name = new_table.Table_name.Replace(" ", "");
                        new_table.Model_name = new_table.Model_name.Replace("_", "");
                        //new_table.Model_name = Capitalize_FChar(new_table.Model_name).Trim() + "Model";
                        new_table.Model_name = Capitalize_FChar(new_table.Model_name).Trim();
                        //----LAST-------> Will Find Last new_table.unique_identifier = UniqueIdentifier;
                        //....LAST.....>new_table.unique_identifier_param_name = UniqueIdentifier_Param;
                        //....LAST.....>unique_identifier_datatype_ide = UniqueIdentifier_type_IDE;
                        //....LAST...>new_table.unique_identifier_datatype_driver = UniqueIdentifier_Type_Driver;

                        string tablename_spaced = new_table.Table_name.Replace("_", " ");
                        // #METHOD NAMING CONVENTIONS
                        new_table.Db_connvariable = "AppConnection";
                        new_table.Insert_data = "Insert_" + new_table.Table_name;
                        new_table.Get_datatable_data = "Read_" + new_table.Table_name;
                        new_table.Get_list_data = "Read_" + new_table.Table_name + "_LIST";
                        new_table.Get_specific_data = "Get_" + new_table.Table_name;
                        new_table.Update_specific_data = "Update_" + new_table.Table_name;
                        new_table.Delete_specific_data = "Delete_" + new_table.Table_name;
                        new_table.Get_rowcount_specific = "Get_RowCount_" + new_table.Table_name;
                        new_table.Get_rowcount_all = "Get_RowCount_" + new_table.Table_name + "_ALL";
                        new_table.Display_table_name = Capitalize_FChar(tablename_spaced);

                        //----->>Find Columns Keys | Find Primary Key, Unique Identifier, DataType ETC
                        new_table.FindTableIdentifier_PrimaryKeys(this.Get_DatabaseTableColumnsPrimaryKeys(DatabaseNameSchema, TableName));

                        new_tables.Add(new_table);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);

                    }

                }
                feedback.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                appConnection.Close();
            }

            return new_tables;
        }

        #endregion

        #region Get_DatabaseTableColumns 
        public List<TableColumn> Get_DatabaseTableColumns(string DatabaseName, string TableNameReceived)
        {
            List<TableColumn> new_columns = new List<TableColumn>();
            MySqlConnection appConnection = new MySqlConnection(this.DBConfiguration.Get_ConnectionString());
            try
            {
                string Querry = "SELECT a.COLUMN_NAME,a.DATA_TYPE,a.COLUMN_KEY,a.IS_NULLABLE,a.EXTRA,b.REFERENCED_TABLE_NAME,b.REFERENCED_COLUMN_NAME FROM information_schema.columns a  LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE b  ON b.COLUMN_NAME =a.COLUMN_NAME WHERE a.TABLE_NAME =@TABLE_NAME AND a.TABLE_SCHEMA=@TABLE_SCHEMA GROUP BY a.COLUMN_NAME ORDER BY a.ORDINAL_POSITION ASC;";
                //@Determine if AutoIncrement Fild will be required | It will stil lbe visible in Primary Keys
                appConnection.Open();
                MySqlCommand command = new MySqlCommand(Querry, appConnection);
                command.Parameters.Add("@TABLE_NAME", MySqlDbType.VarChar).Value = TableNameReceived;
                command.Parameters.Add("@TABLE_SCHEMA", MySqlDbType.VarChar).Value = DatabaseName;
                MySqlDataReader feedback = command.ExecuteReader();
                int startAt = 0;
                while (feedback.Read())
                {

                    try
                    {
                        //Get Columns
                        TableColumn new_column = new TableColumn
                        {
                            Column_id = startAt,
                            Table_name = TableNameReceived,
                            Column_name = feedback.GetString(0),
                            Data_type = feedback.GetString(1),
                            Column_key = feedback.GetString(2),
                            Is_nullable = feedback.GetString(3),
                            Extra = feedback.GetString(4)
                        };
                        //@Check
                        if (feedback.IsDBNull(5) == false) { new_column.Referenced_table_name = feedback.GetString(5); } else { new_column.Referenced_table_name = string.Empty; }
                        if (feedback.IsDBNull(6) == false) { new_column.Referenced_column_name = feedback.GetString(6); } else { new_column.Referenced_column_name = string.Empty; }
                        //@Required Fields
                        if (feedback.GetString(3) == "NO") { new_column.Required = "required"; } else { new_column.Required = string.Empty; }
                        //->>Properties
                        //#Loop All Columns
                        new_column.Column_display = new_column.Column_name.Replace("_", " ");
                        new_column.Column_display = this.Capitalize_FChar(new_column.Column_display);
                        new_column.Column_param_name = new_column.Column_name + "_Value";
                        //->>IDE
                        this.SupportedDBEngine.ModelGenerator.Load_TableColumn_Drivers(ref new_column);
                        new_columns.Add(new_column);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    startAt += 1;
                }
                feedback.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                appConnection.Close();
            }

            return new_columns;
        }

        #endregion

        #region Get_DatabaseTableColumnsPrimaryKeys
        public List<TableColumn> Get_DatabaseTableColumnsPrimaryKeys(string DatabaseName, string TableNameReceived)
        {
            List<TableColumn> new_columns = new List<TableColumn>();
            MySqlConnection appConnection = new MySqlConnection(this.DBConfiguration.Get_ConnectionString());
            try
            {

                string Querry = "SELECT COLUMN_NAME,DATA_TYPE,COLUMN_KEY,IS_NULLABLE,EXTRA FROM information_schema.columns WHERE table_name =@TABLE_NAME AND TABLE_SCHEMA=@TABLE_SCHEMA AND COLUMN_KEY ='PRI' OR table_name = @TABLE_NAME AND TABLE_SCHEMA=@TABLE_SCHEMA AND COLUMN_KEY ='UNI';";
                appConnection.Open();
                MySqlCommand command = new MySqlCommand(Querry, appConnection);
                command.Parameters.Add("@TABLE_NAME", MySqlDbType.VarChar).Value = TableNameReceived;
                command.Parameters.Add("@TABLE_SCHEMA", MySqlDbType.VarChar).Value = DatabaseName;
                MySqlDataReader feedback = command.ExecuteReader();
                int StartAt = 0;
                while (feedback.Read())
                {

                    try
                    {
                        //Get Columns
                        //Get Columns
                        TableColumn new_column = new TableColumn
                        {
                            Column_id = StartAt,
                            Table_name = TableNameReceived,
                            Column_name = feedback.GetString(0),
                            Data_type = feedback.GetString(1),
                            Column_key = feedback.GetString(2),
                            Is_nullable = feedback.GetString(3),
                            Extra = feedback.GetString(4)
                        };
                        //->>Properties
                        new_column.Referenced_column_name = string.Empty;
                        new_column.Referenced_table_name = string.Empty;
                        //@Required Fields
                        if (feedback.GetString(3) == "NO") { new_column.Required = "true"; } else { new_column.Required = "false"; }

                        //#Loop All Columns
                        new_column.Column_display = new_column.Column_name.Replace("_", " ");
                        new_column.Column_display = this.Capitalize_FChar(new_column.Column_display);
                        new_column.Column_param_name = new_column.Column_name + "_Value";
                        //->>IDE
                        this.SupportedDBEngine.ModelGenerator.Load_TableColumn_Drivers(ref new_column);
                        new_columns.Add(new_column);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    //@Incremen
                    StartAt += 1;
                }
                feedback.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                appConnection.Close();
            }

            return new_columns;
        }

        #endregion

        #region Get_DatabaseTablesWithColumns
        public List<TableDesign> Get_DatabaseTablesWithColumns(string DatabaseName)
        {
            List<TableDesign> db_tables = new List<TableDesign>();
            try
            {
                db_tables = this.Get_DatabaseTablesWithOutColumns(DatabaseName);

                //#Loop Ech Table
                foreach (TableDesign table in db_tables)
                {
                    table.Table_Columns = this.Get_DatabaseTableColumns(DatabaseName, table.Origin_Table_name);
                    //----->>Find Columns Keys | Find Primary Key, Unique Identifier, DataType ETC
                    table.FindTableIdentifier_PrimaryKeys(this.Get_DatabaseTableColumnsPrimaryKeys(DatabaseName, table.Origin_Table_name));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return db_tables;

        }

        #endregion

        //-----------------END OF DATABASE INTERACTION--------------------------------


        #region Get_DefaultDBName_Exceptions
        public string[] Get_DefaultDBName_Exceptions()
        {
            string[] db_exceptions =
            {
                "information_schema",
                "mysql",
                "performance_schema"
                //-->Add More Exceptions
            };
            return db_exceptions;
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

        #region Get_GeneratedTableModel | One Table With Columns

        public GeneratedModelTemplate Get_GeneratedTableModel(TableDesign CurrentTableWithColumns)
        {
            try
            {
                var objectSupport = new GeneratedModelTemplate
                {
                    SanitizedModelDesign = this.SupportedDBEngine.ModelGenerator.Get_GeneratedModel(CurrentTableWithColumns, ModelNamespace),
                    SupportedEngineName = this.SupportedDBEngine.Engine_Name,
                    TargetedDatabase = CurrentTableWithColumns.Database_Name,
                    SanitizedFileName = SupportedDBEngine.ModelSaveSubFolder + CurrentTableWithColumns.Model_name + SupportedDBEngine.ModelSaveExtension,
                    DateTimeGenerated = DateTime.Now
                };
                return objectSupport;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Get_GeneratedTableModel | List of Table With Columns

        public List<GeneratedModelTemplate> Get_GeneratedTableModel(List<TableDesign> ListOfTablesWithItsColumns)
        {
            List<GeneratedModelTemplate> all_models = new List<GeneratedModelTemplate>();
            try
            {
                //Loop Tables
                foreach (TableDesign CurrentTableWithColumns in ListOfTablesWithItsColumns)
                {
                    var objectSupport = new GeneratedModelTemplate
                    {
                        SanitizedModelDesign = this.SupportedDBEngine.ModelGenerator.Get_GeneratedModel(CurrentTableWithColumns, ModelNamespace),
                        SupportedEngineName = this.SupportedDBEngine.Engine_Name,
                        TargetedDatabase = CurrentTableWithColumns.Database_Name,
                        SanitizedFileName = SupportedDBEngine.ModelSaveSubFolder + CurrentTableWithColumns.Model_name + SupportedDBEngine.ModelSaveExtension,
                        DateTimeGenerated = DateTime.Now
                    };

                    all_models.Add(objectSupport);



                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return all_models;
        }

        #endregion
    }
}
