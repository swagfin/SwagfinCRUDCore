namespace SwagfinModelConverter.MySqlNetConverters
{
    class SQLITE_MYSQL : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            // #Imports
            new_data = new_data.Replace("Imports System.Data.SQLite", "Imports MySql.Data.MySqlClient");
            new_data = new_data.Replace("using System.Data.SQLite", "using MySql.Data.MySqlClient");
            new_data = new_data.Replace("SQLiteConnection", "MySqlConnection");

            // #Common
            new_data = new_data.Replace("SQLiteCommand", "MySqlCommand");
            new_data = new_data.Replace("SQLiteDataReader", "MySqlDataReader");
            new_data = new_data.Replace("SQLiteDataAdapter", "MySqlDataAdapter");
            new_data = new_data.Replace("SQLiteTransaction", "MySqlTransaction");
            // #ParamTypes
            new_data = new_data.Replace("System.Data.DbType.Int32", "MySqlDbType.Int32");
            new_data = new_data.Replace("System.Data.DbType.Double", "MySqlDbType.Double");
            new_data = new_data.Replace("System.Data.DbType.Decimal", "MySqlDbType.Decimal");
            new_data = new_data.Replace("System.Data.DbType.String", "MySqlDbType.VarChar");
            new_data = new_data.Replace("System.Data.DbType.DateTime", "MySqlDbType.DateTime");
            new_data = new_data.Replace("System.Data.DbType.Date", "MySqlDbType.Date");
            new_data = new_data.Replace("System.Data.DbType.DateTime", "MySqlDbType.Timestamp");
            new_data = new_data.Replace("System.Data.DbType.Double", "MySqlDbType.Float");
        }
    }
}
