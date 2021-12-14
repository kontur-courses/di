using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();

            try
            {
                client.CreateCloud();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: " + e);
                throw;
            }
        }
    }
}