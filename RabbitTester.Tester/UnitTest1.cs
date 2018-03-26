using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;

namespace RabbitTester.Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConnection()
        {
            var factory = new ConnectionFactory();
            factory.UserName = "rabbit";
            factory.Password = "rabbit";
            factory.VirtualHost = "testHost";
            factory.HostName = "192.168.56.100";
            factory.Port = 5672;
            //var factory = new ConnectionFactory() {
            //    Uri = new Uri("amqp://rabbit:rabbit@192.168.56.100:5672/testHost")
            //};           
            var con = factory.CreateConnection();

            var m1 = con.CreateModel();
            var m2 = con.CreateModel();
        }
    }
}
