namespace SwagfinModelConverter.MySqlNetConverters
{
    class MSSQL_MYSQL : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            // #Imports
            new_data = new_data.Replace("Imports System.Data.SqlClient", "Imports MySql.Data.MySqlClient");
            new_data = new_data.Replace("using System.Data.SqlClient", "using MySql.Data.MySqlClient");
            new_data = new_data.Replace("SqlConnection", "MySqlConnection");
            // @Skip
            // #Common
            new_data = new_data.Replace("SqlCommand", "MySqlCommand");
            new_data = new_data.Replace("SqlDataReader", "MySqlDataReader");
            new_data = new_data.Replace("SqlDataAdapter", "MySqlDataAdapter");
            new_data = new_data.Replace("SqlTransaction", "MySqlTransaction");
            // #ParamTypes
            new_data = new_data.Replace("SqlDbType.Int", "MySqlDbType.Int32");
            new_data = new_data.Replace("SqlDbType.Decimal", "MySqlDbType.Double");
            new_data = new_data.Replace("SqlDbType.Decimal", "MySqlDbType.Decimal");
            new_data = new_data.Replace("SqlDbType.VarChar", "MySqlDbType.VarChar");
            new_data = new_data.Replace("SqlDbType.DateTime", "MySqlDbType.DateTime");
            new_data = new_data.Replace("SqlDbType.Date", "MySqlDbType.Date");
            new_data = new_data.Replace("SqlDbType.Timestamp", "MySqlDbType.Timestamp");
            new_data = new_data.Replace("SqlDbType.Float", "MySqlDbType.Float");
        }
    }
}
