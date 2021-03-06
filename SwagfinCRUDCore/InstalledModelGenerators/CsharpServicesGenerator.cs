﻿using System;
using System.Collections.Generic;

namespace SwagfinCRUDCore.InstalledModelGenerators
{
    class CSharpServicesGenerator : ILanguageModelGenerator
    {
        protected static string QuotesChar = char.ConvertFromUtf32(34);

        #region Get_GeneratedModel
        public string Get_GeneratedModel(TableDesign CurrentTableWithColumns, string ModelNameSpace = "SwagfinCrud")
        {

            string FINALE_DATA = "";
            try
            {
                //Check Name
                string className = CurrentTableWithColumns.Table_name;
                string IMPORTS_STRING = @"
using {namespace}.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace {namespace}.Services
{
    public interface I{Table_name}Service
    {
        void Add({Table_name} {table_name});
        Task AddAsync({Table_name} {table_name});
        IEnumerable<{Table_name}> GetAll();
        Task<IEnumerable<{Table_name}>> GetAllAsync();
        {Table_name} Get({unique_identifier_datatype_ide} {unique_identifier});
        Task<{Table_name}> GetAsync({unique_identifier_datatype_ide} {unique_identifier}); 
        void Update({Table_name} {table_name});
        Task UpdateAsync({Table_name} {table_name});
        void Remove({unique_identifier_datatype_ide} {unique_identifier});
        Task RemoveAsync({unique_identifier_datatype_ide} {unique_identifier});
        void Remove({Table_name} {table_name});
        Task RemoveAsync({Table_name} {table_name});	
		
	}
	
}	

";

                //Replacing
                IMPORTS_STRING = IMPORTS_STRING.Replace("{namespace}", ModelNameSpace.ToString().Trim());
                IMPORTS_STRING = IMPORTS_STRING.Replace("{Table_name}", DataHelpers.Capitalize_FChar(className));
                IMPORTS_STRING = IMPORTS_STRING.Replace("{table_name}", DataHelpers.UnCapitalize_FChar(className));
                IMPORTS_STRING = IMPORTS_STRING.Replace("{unique_identifier_datatype_ide}", CurrentTableWithColumns.Unique_identifier_datatype_ide);
                IMPORTS_STRING = IMPORTS_STRING.Replace("{unique_identifier}", CurrentTableWithColumns.Unique_identifier);

                FINALE_DATA = IMPORTS_STRING;

            }
            catch (Exception)
            {
            }

            return FINALE_DATA;
        }

        #endregion


        #region Generate Create
        public string Generate_CREATE(TableDesign TableData) => string.Empty;

        #endregion

        #region Generate Delete

        public string Generate_DELETE(TableDesign TableData) => string.Empty;

        #endregion

        #region Generate Get
        public string Generate_GET(TableDesign TableData) => string.Empty;
        #endregion

        #region Generate Read
        public string Generate_READ(TableDesign TableData) => string.Empty;
        #endregion

        #region Generate RowCount
        public string Generate_ROWCOUNT(TableDesign TableData) => string.Empty;
        #endregion

        #region Generate Update
        public string Generate_UPDATE(TableDesign TableData) => string.Empty;
        #endregion

        #region TableDrivers
        public bool Load_TableColumn_Drivers(ref TableColumn TableColumn)
        {
            try
            {
                //TINYINT,SMALLINT,MEDIUMINT

                if (TableColumn.Data_type == "int" || TableColumn.Data_type == "smallint" || TableColumn.Data_type == "smallint")
                {
                    TableColumn.Column_datatype_ide = "int";
                    TableColumn.Column_datatype_get = "GetInt32";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Int32";

                }
                else if (TableColumn.Data_type == "tinyint")
                {
                    TableColumn.Column_datatype_ide = "bool";
                    TableColumn.Column_datatype_get = "GetInt32";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Int32";
                }

                else if (TableColumn.Data_type == "datetime" | TableColumn.Data_type == "timestamp" | TableColumn.Data_type == "time")
                {
                    TableColumn.Column_datatype_ide = "DateTime";
                    TableColumn.Column_datatype_get = "GetDateTime";
                    TableColumn.Column_datatype_driver = "MySqlDbType.DateTime";

                }
                else if (TableColumn.Data_type == "double")
                {
                    TableColumn.Column_datatype_ide = "double";
                    TableColumn.Column_datatype_get = "GetDouble";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Double";
                }
                else if (TableColumn.Data_type == "decimal")
                {
                    TableColumn.Column_datatype_ide = "decimal";
                    TableColumn.Column_datatype_get = "GetDecimal";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Decimal";

                }
                else if (TableColumn.Data_type == "float")
                {
                    TableColumn.Column_datatype_ide = "float";
                    TableColumn.Column_datatype_get = "Double";
                    TableColumn.Column_datatype_driver = "MySqlDbType.Float";

                }
                else if (TableColumn.Data_type == "date" | TableColumn.Data_type == "year")
                {
                    TableColumn.Column_datatype_ide = "DateTime";
                    TableColumn.Column_datatype_get = "GetDateTime";
                    TableColumn.Column_datatype_driver = "MySqlDbType.DateTime";

                }
                else
                {
                    TableColumn.Column_datatype_ide = "string";
                    TableColumn.Column_datatype_get = "GetString";
                    TableColumn.Column_datatype_driver = "MySqlDbType.VarChar";

                }

                if (TableColumn.Is_nullable == "YES" && TableColumn.Column_datatype_ide != "string")
                    TableColumn.Column_datatype_ide += "?";

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
