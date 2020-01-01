namespace SwagfinModelConverter.MySqlNetConverters
{
    class SQLITE_ACCESSDB : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            SQLITE_MYSQL convertMe = new SQLITE_MYSQL();
            convertMe.Convert(ref new_data);
            MYSQL_ACCESSDB  ConvertAgain = new MYSQL_ACCESSDB();
            ConvertAgain.Convert(ref new_data);

        }
    }
}
