namespace SwagfinModelConverter.MySqlNetConverters
{
    class MYSQL_SQLITE : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            new_data = new_data.Replace("Imports MySql.Data.MySqlClient", "Imports System.Data.SQLite");
            new_data = new_data.Replace("Imports MySql.Data.MySqlClient", "Imports System.Data.SQLite");
            new_data = new_data.Replace("using MySql.Data.MySqlClient", "using System.Data.SQLite");
            new_data = new_data.Replace("MySqlConnection", "SQLiteConnection");
            // #Common
            new_data = new_data.Replace("MySqlCommand", "SQLiteCommand");
            new_data = new_data.Replace("MySqlDataReader", "SQLiteDataReader");
            new_data = new_data.Replace("MySqlDataAdapter", "SQLiteDataAdapter");
            new_data = new_data.Replace("MySqlTransaction", "SQLiteTransaction");
            // #ParamTypes
            new_data = new_data.Replace("MySqlDbType.Int32", "System.Data.DbType.Int32");
            new_data = new_data.Replace("MySqlDbType.Double", "System.Data.DbType.Double");
            new_data = new_data.Replace("MySqlDbType.Decimal", "System.Data.DbType.Decimal");
            new_data = new_data.Replace("MySqlDbType.VarChar", "System.Data.DbType.String");
            new_data = new_data.Replace("MySqlDbType.DateTime", "System.Data.DbType.DateTime");
            new_data = new_data.Replace("MySqlDbType.Date", "System.Data.DbType.Date");
            new_data = new_data.Replace("MySqlDbType.Timestamp", "System.Data.DbType.DateTime");
            new_data = new_data.Replace("MySqlDbType.Float", "System.Data.DbType.Double");
        }

      
    }
}
