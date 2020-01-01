using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore
{
   public class DatabaseDesign 
    {
        public string Database_Name { get; set; }
        public string Server_Address { get; set; }
        public int Database_Port { get; set; }
        public DateTime Last_Sync { get; set; }
      
        //List of Tables
       // public List<TableDesign> Tables { get; set; }
    }
}
