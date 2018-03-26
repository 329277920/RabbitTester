using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitTester.TesterWin
{
    public class TopicExchangeConsumer
    {
        protected int ConsumerId;

        protected IConnection Connection;

        protected string RoutingKey;

        public TopicExchangeConsumer(int consumerId, IConnection con, string routingKey)
        {
            ConsumerId = consumerId;
            Connection = con;
            RoutingKey = routingKey;
        }

        public void Start()
        {
            var queue = string.Format("{0}_{1}", RabbitSettings.TopicQueue, ConsumerId);

            var channel = Connection.CreateModel();

            // 定义一个消息队列(durable: 持久化，autoDelete:不自动删除，exclusive:断开后自动删除)
            channel.QueueDeclare(queue,
                durable: true,
                autoDelete: false,
                exclusive: true);

            // channel.QueueDeclare(RabbitSettings.DirectQueue);
            // 将消息队列绑定到交换机
            channel.QueueBind(queue,
                RabbitSettings.TopicExChange,
                RoutingKey, null);

            // 推模式          
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
            String consumerTag = channel.BasicConsume(queue,
                false, consumer);
        }
    }
}
