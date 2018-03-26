using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitTester.TesterWin
{
    /// <summary>
    /// 主题交换机    
    /// 场景：2个发布者，分别使用不同的路由Key绑定到同一个交换机。
    ///       4个消费者，启动时，创建4个队列，按路由Key分别读取各个主题的消息。
    ///           在消费者断开连接后，队列被删除。
    /// </summary>
    public class TopicExchangeTester : BaseTester
    {
        public override void Test()
        {
            var route1 = "room_1";
            var publiser1 = new TopicExchangePublisher(1, CreateConnection(), route1);
            publiser1.Start();
             
            var consumer1 = new TopicExchangeConsumer(1, CreateConnection(), route1);
            consumer1.Start();

            var consumer2 = new TopicExchangeConsumer(2, CreateConnection(), route1);
            consumer2.Start();

            

            var route2 = "room_2";
            var publiser2 = new TopicExchangePublisher(2, CreateConnection(), route2);
            publiser2.Start();

            var consumer3 = new TopicExchangeConsumer(3, CreateConnection(), route2);
            consumer3.Start();

            var consumer4 = new TopicExchangeConsumer(4, CreateConnection(), route2);
            consumer4.Start();
        }
    }
}
