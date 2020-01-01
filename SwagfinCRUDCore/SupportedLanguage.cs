using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore
{
    #region SupportedLanguage Class
    public class SupportedLanguage
    {
        public string Language_Key { get; set; }

        #region Get_Supported_Languages | Registering Supported Languages
        public List<SupportedLanguage> Get_Supported_Languages()
        {
            return new List<SupportedLanguage>
            {
                new SupportedLanguage()
                {
                    Language_Key = "VB.NET"
                },
                new SupportedLanguage()
                {
                    Language_Key = "C#"
                },
            };
        }

        #endregion
    }

    #endregion
}
