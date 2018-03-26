using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTester.TesterWin
{
    /// <summary>
    /// 直连交换机消费者，N个消费者共享同一个消息队列。 
    /// </summary>
    public class DirectExchangeConsumer
    {
        protected int ConsumerId;

        protected IConnection Connection;

        public DirectExchangeConsumer(int consumerId, IConnection con)
        {
            ConsumerId = consumerId;
            Connection = con;
        }

        public void Start()
        {
            // 推模式
            var channel = Connection.CreateModel();
            // 定义一个消费者
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {                
                var body = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine("客户：{0}，消息：{1}", ConsumerId, body);
                // 通知
                channel.BasicAck(ea.DeliveryTag, false);
            };
            // 将消费者绑定到消息队列，并设置自动应答为False
            String consumerTag = channel.BasicConsume(RabbitSettings.DirectQueue,
                false, consumer);

            // 取消绑定
            // channel.BasicCancel(consumerTag);

            // 拉模式
            //new TaskFactory().StartNew(() =>
            //{              
            //    var channel = Connection.CreateModel();              
            //    bool noAck = false;                
            //    while (true)
            //    {
            //        // 获取消息，默认不给出应答                
            //        BasicGetResult result = channel.BasicGet(RabbitSettings.DirectQueue, noAck);
            //        if (result == null)
            //        {
            //            // No message available at this time.
            //        }
            //        else
            //        {
            //            IBasicProperties props = result.BasicProperties;
            //            var body = Encoding.UTF8.GetString(result.Body);

            //            Console.WriteLine("客户：{0}，消息：{1}", ConsumerId, body);

            //            // 通知删除消息
            //            channel.BasicAck(result.DeliveryTag, false);
            //        }
            //        System.Threading.Thread.Sleep(500);
            //    }
            //});
        }
    }
}
