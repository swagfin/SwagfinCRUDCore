namespace SwagfinModelConverter.MySqlNetConverters
{
    class MSSQL_ACCESSDB : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            MSSQL_MYSQL convertMe = new MSSQL_MYSQL();
            convertMe.Convert(ref new_data);
            MYSQL_ACCESSDB ConvertAgain = new MYSQL_ACCESSDB();
            ConvertAgain.Convert(ref new_data);

        }
    }
}
