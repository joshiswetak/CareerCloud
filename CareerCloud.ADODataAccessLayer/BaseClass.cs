using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class BaseClass
    {
        protected string _connString;

        public BaseClass()
        {
            _connString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        }
    }
}
