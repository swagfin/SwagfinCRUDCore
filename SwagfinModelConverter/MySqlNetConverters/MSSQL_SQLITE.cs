namespace SwagfinModelConverter.MySqlNetConverters
{
    class MSSQL_SQLITE : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            MSSQL_MYSQL convertMe = new MSSQL_MYSQL();
            convertMe.Convert(ref new_data);
            MYSQL_SQLITE ConvertAgain = new MYSQL_SQLITE();
            ConvertAgain.Convert(ref new_data);

        }
    }
}
