using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTester.TesterWin
{
    /// <summary>
    /// 直连交换机测试
    /// 说明:根据消息携带的路由键，将消息转发给对应的队列。同时，一个消息只会发送给一个消费者，如果匹配到多个消费者，则随机发送
    /// 场景：启动2个消息发布者，每个消费者启动时，会创建唯一的一个消息队列。将消息发送给交换机，并转发给消息队列。
    ///           该队列在服务端持久化，由多个消费者共享。
    ///       启动2个消费者，在共享队列中读取消息。
    ///       未测试，不同的队列绑定到同一个交换机的场景。
    /// </summary>
    public class DirectExchangeTester : BaseTester
    {
        public override void Test()
        {
            //var publiser1 = new DirectExchangePublisher(1, CreateConnection());
            //publiser1.Start();

            //var publiser2 = new DirectExchangePublisher(2, CreateConnection());
            //publiser2.Start();

           var consumer1 = new DirectExchangeConsumer(1, CreateConnection());
           consumer1.Start();

            //var consumer2 = new DirectExchangeConsumer(2, CreateConnection());
            //consumer2.Start();
        }        
     }
}
