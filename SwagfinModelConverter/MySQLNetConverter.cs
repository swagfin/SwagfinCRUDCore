using SwagfinModelConverter.MySqlNetConverters;
using System.Collections.Generic;

namespace SwagfinModelConverter
{
    public class MySQLNetConverter
    {
        public int ConverterID { get; set; }
        public string ConverterName { get; set; }
        public string ConverterInfo { get; set; }
        public IModelConverter Converter { get; set; }

        public List<MySQLNetConverter> GetSupportedConverters()
        {
            /*
             * Register Supported Converter Here            
             */
            return new List<MySQLNetConverter>
        {
            new MySQLNetConverter
            {
                ConverterID=101,
                ConverterName="MySQL to SQLite",
                Converter= new MYSQL_SQLITE(),
                ConverterInfo ="Convert an Existing MySQL Model to SQLite Database Support"
            },
            new MySQLNetConverter
            {
                ConverterID =102,
                ConverterName="MySQL to Microsoft SQL",
                Converter= new MYSQL_MSSQL(),
                ConverterInfo ="Convert an Existing MySQL Model to Microsoft SQL Server Database Support"

            },
            new MySQLNetConverter
            {
                ConverterID=103,
                ConverterName="MySQL to Access DB (*.acdb, *.mdb)",
                Converter = new MYSQL_ACCESSDB(),
                ConverterInfo ="Convert an Existing MySQL Model to Access local Database Support"

            },
            new MySQLNetConverter
            {
                ConverterID =104,
                ConverterName="SQLite to MySQL",
                Converter = new SQLITE_MYSQL(),
                ConverterInfo ="Convert an Existing SQlite Model to MySQL Server Database Support"
            },
            new MySQLNetConverter
            {
                ConverterID =105,
                ConverterName="SQLite to Microsoft SQL",
                Converter = new SQLITE_MSSQL(),
                ConverterInfo ="Convert an Existing SQlite Model to Microsoft SQL Server Database Support"

            },
            new MySQLNetConverter
            {
                ConverterID =106,
                ConverterName="SQLite to Access DB (*.acdb, *.mdb)",
                Converter= new SQLITE_ACCESSDB(),
                ConverterInfo ="Convert an Existing SQlite Model to AccessDB Database Support",
            },
            new MySQLNetConverter
            {
                ConverterID =107,
                ConverterName="Microsoft SQL to MySQL",
                Converter= new MYSQL_MSSQL(),
                ConverterInfo ="Convert an Existing Microsoft SQL Model to MySQL Server Database Support"
            },
            new MySQLNetConverter
            {
                ConverterID=108,
                ConverterName="Microsoft SQL to SQLite",
                Converter= new MYSQL_SQLITE(),
                ConverterInfo ="Convert an Existing Microsoft SQL Model to SQLite Database Support"
            },
             new MySQLNetConverter
            {
                ConverterID=109,
                ConverterName="Microsoft SQL to Access DB (*.acdb, *.mdb)",
                Converter =new MSSQL_ACCESSDB() ,
                ConverterInfo ="Convert an Existing Microsoft SQL Model to Access Database Support"

            },
            new MySQLNetConverter
            {
                ConverterID=110,
                ConverterName="Access DB (*.acdb, *.mdb) to MySQL",
                Converter= new ACCESSDB_MYSQL(),
                ConverterInfo ="Convert an Existing Access Database Model to MySQL Server Database Support"
            },
            new MySQLNetConverter
            {
                ConverterID=111,
                ConverterName="Access DB (*.acdb, *.mdb) to SQLite",
                Converter= new ACCESSDB_SQLITE(),
                ConverterInfo ="Convert an Existing Access Database Model to SQLite Database Support"
            },
            new MySQLNetConverter
            {
                ConverterID=112,
                ConverterName="Access DB (*.acdb, *.mdb) to Microsoft SQL",
                Converter= new ACCESSDB_MSSQL(),
                ConverterInfo ="Convert an Existing Access Database Model to Microsoft SQL Server Database Support"

            }

        };

        }

    }

}
