namespace SwagfinModelConverter.MySqlNetConverters
{
    class MYSQL_MSSQL : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            // #Imports
            new_data = new_data.Replace("Imports MySql.Data.MySqlClient", "Imports System.Data.SqlClient");
            new_data = new_data.Replace("using MySql.Data.MySqlClient", "using System.Data.SqlClient");
            new_data = new_data.Replace("MySqlConnection", "SqlConnection");
            // #Common
            new_data = new_data.Replace("MySqlCommand", "SqlCommand");
            new_data = new_data.Replace("MySqlDataReader", "SqlDataReader");
            new_data = new_data.Replace("MySqlDataAdapter", "SqlDataAdapter");
            new_data = new_data.Replace("MySqlTransaction", "SqlTransaction");
            // #ParamTypes
            new_data = new_data.Replace("MySqlDbType.Int32", "SqlDbType.Int");
            new_data = new_data.Replace("MySqlDbType.Double", "SqlDbType.Decimal");
            new_data = new_data.Replace("MySqlDbType.Decimal", "SqlDbType.Decimal");
            new_data = new_data.Replace("MySqlDbType.VarChar", "SqlDbType.VarChar");
            new_data = new_data.Replace("MySqlDbType.DateTime", "SqlDbType.DateTime");
            new_data = new_data.Replace("MySqlDbType.Date", "SqlDbType.Date");
            new_data = new_data.Replace("MySqlDbType.Timestamp", "SqlDbType.Timestamp");
            new_data = new_data.Replace("MySqlDbType.Float", "SqlDbType.Float");
        }

    
    }
}
