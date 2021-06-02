using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace TestTanyah
{
    public class DataBaseConnexion
    {
        private static DataBaseConnexion singletonDbCon;
        private readonly OleDbConnection oledbconnection =
            new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\TestTanyah.mdb;Persist Security Info=False;");
        private static readonly object locker = new object();
        public static DataBaseConnexion SingletonDbCon
        {
            get
            {
                lock (locker)
                {
                    if (singletonDbCon == null)
                    {
                        singletonDbCon = new DataBaseConnexion();
                    }
                    return singletonDbCon;
                }
            }
        }

        public OleDbConnection getDbConnection()
        {
            return oledbconnection;
        }
    }
}
