using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace Programming_Task
{
    class Program
    {
        internal UserQueries UserQueries
        {
            get => default;
            set
            {
            }
        }

        static void Main(string[] args)
        {
            UserQueries menu = new UserQueries();
            menu.QueriesMenu();
        }
    }
}
