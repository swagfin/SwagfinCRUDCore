using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinCRUDCore
{
    public class DatabaseConfiguration
    {
        public string MySQL_server { get; set; }
        public string MySQL_username { get; set; }
        public string MySQL_password { get; set; }
       //->Skip public string MySQL_database { get; set; }
        public int MySQL_Port { get; set; }



        //-->Database Name is not required
        //->>Default
        public DatabaseConfiguration()
        {
            this.MySQL_server = "localhost";
            this.MySQL_username = "root";
            this.MySQL_password = "";
            this.MySQL_Port = 3306;
        }


        public DatabaseConfiguration(string server_name, string server_username, string server_password, int server_port=3306)
        {
            this.MySQL_server = server_name;
            this.MySQL_username = server_username;
            this.MySQL_password = server_password;
            this.MySQL_Port = server_port;
        }

        public string Get_ConnectionString()
        {
            return String.Format("server={0};userid={1};password={2};port={3};sslmode=none", this.MySQL_server, this.MySQL_username, this.MySQL_password, this.MySQL_Port.ToString());
        }
        public string Get_ConnectionString(string AssignedDatabase)
        {
            return String.Format("server={0};userid={1};password={2};port={3};database={4};sslmode=none", this.MySQL_server, this.MySQL_username, this.MySQL_password, this.MySQL_Port.ToString(), AssignedDatabase);
        }


    }
}
