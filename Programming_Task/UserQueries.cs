using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Programming_Task
{
    /*Menu of Queries that are represented to the user*/
    class UserQueries
    {
        private WriteCsvToSql writer;
        private ReadFromSql reader;

        //default c'tor
        public UserQueries()
        {
            writer = new WriteCsvToSql();
            writer.ImportCSVFilesToSql();

            reader = new ReadFromSql();
        }

        internal SqlHandlerBase SqlHandlerBase
        {
            get => default;
            set
            {
            }
        }

        //user's Query menu to choose a query that interests him
        public void QueriesMenu()
        {
            while (true)
            {
                Console.WriteLine("Covid19 Virus Data");
                Console.WriteLine("Please select a Query by enter a number:");
                Console.WriteLine("1. Choose a country that you are interested in knowing the amount of new cases that have been discovered in it");
                Console.WriteLine("2. The number of patients and the total number of deaths in the ten countries with the largest number of patients");
                string number = Console.ReadLine();

                try
                {
                    if (Int32.Parse(number) == 1)
                    {
                        Console.WriteLine("Please write a country name");
                        string country = Console.ReadLine();
                        reader.AnswerQuery_1(country);

                        break;
                    }
                    else if (Int32.Parse(number) == 2)
                    {
                        reader.AnswerQuery_2();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That is not an option dude,try again!");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Please enter 1 or 2.");
                }
            }
        }
    }
}
