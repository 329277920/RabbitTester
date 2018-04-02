using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTester.TesterWin
{
    class Program
    {
        static void Main(string[] args)
        {
            //var tester = new DirectExchangeTester();

            //tester.Test();

            //var tester = new FanoutExchangeTester();

            //tester.Test();

            var tester = new TopicExchangeTester();

            tester.Test(); 

            Console.ReadKey();
        }                    
    }
}
