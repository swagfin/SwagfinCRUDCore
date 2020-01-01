namespace SwagfinModelConverter.MySqlNetConverters
{
    class ACCESSDB_MSSQL : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            ACCESSDB_MYSQL convertMe = new ACCESSDB_MYSQL();
            convertMe.Convert(ref new_data);
            MYSQL_MSSQL ConvertAgain = new MYSQL_MSSQL();
            ConvertAgain.Convert(ref new_data);

        }
    }
}
