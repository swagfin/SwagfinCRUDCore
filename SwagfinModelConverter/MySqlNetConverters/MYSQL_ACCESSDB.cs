namespace SwagfinModelConverter.MySqlNetConverters
{
    class MYSQL_ACCESSDB : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            // #Imports
            new_data = new_data.Replace("Imports MySql.Data.MySqlClient", "Imports System.Data.OleDb");
            new_data = new_data.Replace("using MySql.Data.MySqlClient", "using System.Data.OleDb");
            new_data = new_data.Replace("MySqlConnection", "OleDbConnection");
            // #Common
            new_data = new_data.Replace("MySqlCommand", "OleDbCommand");
            new_data = new_data.Replace("MySqlDataReader", "OleDbDataReader");
            new_data = new_data.Replace("MySqlDataAdapter", "OleDbDataAdapter");
            new_data = new_data.Replace("MySqlTransaction", "OleDbTransaction");
            // #ParamTypes
            new_data = new_data.Replace("MySqlDbType.Int32", "OleDbType.Integer");
            new_data = new_data.Replace("MySqlDbType.Double", "OleDbType.Double");
            new_data = new_data.Replace("MySqlDbType.Decimal", "OleDbType.Decimal");
            new_data = new_data.Replace("MySqlDbType.VarChar", "OleDbType.VarChar");
            new_data = new_data.Replace("MySqlDbType.DateTime", "OleDbType.Date");
            new_data = new_data.Replace("MySqlDbType.Date", "OleDbType.Date");
            new_data = new_data.Replace("MySqlDbType.Timestamp", "OleDbType.DBTimeStamp");
            new_data = new_data.Replace("MySqlDbType.Float", "OleDbType.Double");
        }

     
    }
}
