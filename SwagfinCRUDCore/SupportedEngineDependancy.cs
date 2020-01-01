using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore
{
    public class SupportedEngineDependancy
    {
        public int Dependancy_id { get; set; }
        public string Engine_Key { get; set; }
        public string Engine_Name { get; set; }
        public string Dependancy_name { get; set; }
        public string Download_link { get; set; }
        public string More_info { get; set; }   
    }
}
