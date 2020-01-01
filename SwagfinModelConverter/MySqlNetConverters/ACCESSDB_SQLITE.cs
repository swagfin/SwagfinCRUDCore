namespace SwagfinModelConverter.MySqlNetConverters
{
    class ACCESSDB_SQLITE : IModelConverter
    {
        public void Convert(ref string new_data)
        {
            ACCESSDB_MYSQL convertMe = new ACCESSDB_MYSQL();
            convertMe.Convert(ref new_data);
            MYSQL_SQLITE ConvertAgain = new MYSQL_SQLITE();
            ConvertAgain.Convert(ref new_data);

        }
    }
}
