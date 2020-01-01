using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore.InstalledModelGenerators
{
    class EmptyModelGenerator : ILanguageModelGenerator
    {
        public string Generate_CREATE(TableDesign TableData)
        {
            return string.Empty;
        }

        public string Generate_DELETE(TableDesign TableData)
        {
            return string.Empty;
        }

        public string Generate_GET(TableDesign TableData)
        {
            return string.Empty;
        }

        public string Generate_READ(TableDesign TableData)
        {
            return string.Empty;
        }

        public string Generate_ROWCOUNT(TableDesign TableData)
        {
            return string.Empty;
        }

        public string Generate_UPDATE(TableDesign TableData)
        {
            return string.Empty;
        }

        public string Get_GeneratedModel(TableDesign CurrentTableWithColumns, string ModelNameSpace = "swagfin.Models")
        {
            return "NOTE: Empty Model Content Generated";
        }

        public bool Load_TableColumn_Drivers(ref TableColumn TableColumn)
        {
            return true;
        }
    }
}
