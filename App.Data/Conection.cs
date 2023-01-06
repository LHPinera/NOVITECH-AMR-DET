using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace App.Data
{
    public class Conection
    {
        public static string GetConLocal()
        {
            //string con = System.IO.File.ReadAllText(@"SERVERSQL.TXT");
            //Servidor local arco movil
            //string con = @"Data Source=SQL5107.site4now.net;Initial Catalog=db_a79839_arcomovilrepuve;User Id=db_a79839_arcomovilrepuve_admin;Password=Cybolt22";
            //string con = @"Data Source=MXB\SQLEXPRESS;Initial Catalog=arcomovilrepuve;User Id=sa;Password=Cybolt22";
            string con = @"Data Source=CYBOLT-RPV-SRV\SQLEXPRESS;Initial Catalog=arcomovilrepuve;User Id=sa;Password=Admin123";
            return con;
        }

        public static string GetConNube()
        {
            //Original
            //string con = @"Data Source=SQL5107.site4now.net;Initial Catalog=db_a79839_arcomovilrepuve;User Id=db_a79839_arcomovilrepuve_admin;Password=Cybolt22";
            string con = @"Data Source=MXB\SQLEXPRESS;Initial Catalog=arcomovilrepuve;User Id=sa;Password=Cybolt22";
            return con;
        }
    }
}
