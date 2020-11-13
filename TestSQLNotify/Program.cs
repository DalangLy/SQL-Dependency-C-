using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLNotify
{
    class Program
    {
        private static string connectionString =@"Data Source=(local);Database=aspnet-TestAPI-20201110100000;Persist Security Info=false;Integrated Security=false;User Id=sa;Password=123";

        static void Main(string[] args)
        {
            // Starting the listener infrastructure...
            SqlDependency.Start(connectionString);

            // Registering for changes... 
            RegisterForChanges();

            // Waiting...
            Console.WriteLine("At this point, you should start the Sql Server ");
            Console.WriteLine("Management Studio and make ");
            Console.WriteLine("some changes to the Users table that you'll find");
            Console.WriteLine(" in the SqlDependencyTest ");
            Console.WriteLine("database. Every time a change happens in this ");
            Console.WriteLine("table, this program should be ");
            Console.WriteLine("notified.\n");
            Console.WriteLine("Press enter to quit this program.");
            Console.ReadLine();

            // Quitting...
            SqlDependency.Stop(connectionString);
        }

        public static void RegisterForChanges()
        {
            // Connecting to the database using our subscriber connection string 
            // and waiting for changes...
            SqlConnection oConnection = new SqlConnection(connectionString);
            oConnection.Open();
            try
            {
                SqlCommand oCommand = new SqlCommand("SELECT ID, Name, Surname FROM dbo.Customers", oConnection);
                SqlDependency oDependency = new SqlDependency(oCommand);
                oDependency.OnChange += new OnChangeEventHandler(OnNotificationChange);
                SqlDataReader objReader = oCommand.ExecuteReader();
                try
                {
                    while (objReader.Read())
                    {
                        // Doing something here...
                    }
                }
                finally
                {
                    objReader.Close();
                }
            }
            finally
            {
                oConnection.Close();
            }
        }

        public static void OnNotificationChange(object caller,
                                                SqlNotificationEventArgs e)
        {
            Console.WriteLine(e.Info.ToString() + ": " + e.Type.ToString());
            RegisterForChanges();
        }
    }
}
