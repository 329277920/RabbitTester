using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitTester.TesterWin
{
    /// <summary>
    /// 扇形交换机测试类
    /// 场景：启动2个消息发布者，将消息发送给交换机，在消费者未启动时，消息不会被转发。
    ///       启动2个消费者，每个消费者启动时，会创建一个消息队列，用于接收消息，该消费者断开连接，将删除队列
    /// </summary>
    public class FanoutExchangeTester : BaseTester
    {
        public override void Test()
        {
            var publiser1 = new FanoutExchangePublisher(1, CreateConnection());
            publiser1.Start();

            var publiser2 = new FanoutExchangePublisher(2, CreateConnection());
            publiser2.Start();

            var consumer1 = new FanoutExchangeConsumer(1, CreateConnection());
            consumer1.Start();

            var consumer2 = new FanoutExchangeConsumer(2, CreateConnection());
            consumer2.Start();
        }
    }
}
