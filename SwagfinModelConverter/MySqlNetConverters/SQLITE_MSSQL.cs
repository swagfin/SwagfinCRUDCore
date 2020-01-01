namespace SwagfinModelConverter.MySqlNetConverters
{
    class SQLITE_MSSQL : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            SQLITE_MYSQL  convertMe = new SQLITE_MYSQL();
            convertMe.Convert(ref new_data);
            MYSQL_MSSQL ConvertAgain = new MYSQL_MSSQL();
            ConvertAgain.Convert(ref new_data);

        }
    }
}
