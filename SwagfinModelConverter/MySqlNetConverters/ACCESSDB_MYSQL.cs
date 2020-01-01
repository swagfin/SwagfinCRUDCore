namespace SwagfinModelConverter.MySqlNetConverters
{
    class ACCESSDB_MYSQL : IModelConverter
    {

        public void Convert(ref string new_data)
        {
            // #Imports
            new_data = new_data.Replace("Imports System.Data.OleDb", "Imports MySql.Data.MySqlClient");
            new_data = new_data.Replace("using System.Data.OleDb", "using MySql.Data.MySqlClient");
            new_data = new_data.Replace("OleDbConnection", "MySqlConnection");
            // #Common
            new_data = new_data.Replace("OleDbCommand", "MySqlCommand");
            new_data = new_data.Replace("OleDbDataReader", "MySqlDataReader");
            new_data = new_data.Replace("OleDbDataAdapter", "MySqlDataAdapter");
            new_data = new_data.Replace("OleDbTransaction", "MySqlTransaction");
            // #ParamTypes
            new_data = new_data.Replace("OleDbType.Integer", "MySqlDbType.Int32");
            new_data = new_data.Replace("OleDbType.Double", "MySqlDbType.Double");
            new_data = new_data.Replace("OleDbType.Decimal", "MySqlDbType.Decimal");
            new_data = new_data.Replace("OleDbType.VarChar", "MySqlDbType.VarChar");
            new_data = new_data.Replace("OleDbType.Date", "MySqlDbType.DateTime");
            // >>SKIP  new_data = Replace(new_data, "OleDbType.Date", "MySqlDbType.Date")
            new_data = new_data.Replace("OleDbType.DBTimeStamp", "MySqlDbType.Timestamp");
        }
    }
}
