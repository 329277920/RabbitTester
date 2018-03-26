using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitTester.TesterWin
{
    /// <summary>
    /// 测试父类
    /// </summary>
    public abstract class BaseTester
    {
        /// <summary>
        /// 创建一个连接对象
        /// </summary>
        /// <returns></returns>
        protected virtual IConnection CreateConnection()
        {
            var factory = new ConnectionFactory();
            factory.UserName = RabbitSettings.User;
            factory.Password = RabbitSettings.Pass;
            factory.VirtualHost = RabbitSettings.VHost;
            factory.HostName = RabbitSettings.HostName;
            factory.Port = RabbitSettings.Port;
            //var factory = new ConnectionFactory()
            //{
            //    Uri = new Uri("amqp://rabbit:rabbit@192.168.56.100:5672/testHost")
            //};
            return factory.CreateConnection();
        }

        public abstract void Test();        
    }
}
