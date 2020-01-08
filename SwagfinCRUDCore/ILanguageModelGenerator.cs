using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore
{
    public interface ILanguageModelGenerator
    {

        string Get_GeneratedModel(TableDesign CurrentTableWithColumns, string ModelNameSpace = "SwagfinCrud");
        string Generate_GET(TableDesign TableData);
        string Generate_CREATE(TableDesign TableData);
        string Generate_READ(TableDesign TableData);
        string Generate_UPDATE(TableDesign TableData);
        string Generate_DELETE(TableDesign TableData);
        string Generate_ROWCOUNT(TableDesign TableData);
        //---->>
        bool Load_TableColumn_Drivers(ref TableColumn TableColumn);

    }

}
