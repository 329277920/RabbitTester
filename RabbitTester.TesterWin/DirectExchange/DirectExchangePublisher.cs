using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTester.TesterWin
{
    /// <summary>
    /// 测试直连交换机发布者
    /// </summary>
    public class DirectExchangePublisher
    {
        public int PubliserId { get; set; }

        protected IConnection Connection { get; set; }

        public DirectExchangePublisher(int publiserId, IConnection con)
        {
            PubliserId = publiserId;
            Connection = con;
        }

        public static int MessageId = 0;

        /// <summary>
        /// 启动一个发布者，并启动一个唯一的消息队列，其余消费者共享该队列
        /// </summary>
        public void Start()
        {
            var channel = Connection.CreateModel();

            //var result = channel.QueueDelete(RabbitSettings.DirectQueue);
            //channel.ExchangeDelete(RabbitSettings.DirectExChange);

            // 定义一个直连交换机，并持久化保存
            channel.ExchangeDeclare(RabbitSettings.DirectExChange,
                ExchangeType.Direct, durable: true);

            // 定义一个消息队列(durable: 持久化，autoDelete:不自动删除)
            channel.QueueDeclare(RabbitSettings.DirectQueue,
                durable: true,
                autoDelete: false,
                exclusive: false);

            // channel.QueueDeclare(RabbitSettings.DirectQueue);
            // 将消息队列绑定到交换机
            channel.QueueBind(RabbitSettings.DirectQueue,
                RabbitSettings.DirectExChange,
                RabbitSettings.DirectExChangeRoutingKey, null);

            // 每隔一秒发送消息至交换机            
            new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        byte[] messageBodyBytes = Encoding.UTF8.GetBytes(string.Format("发送者：{0}，消息Id：{1}。", PubliserId, ++MessageId));
                        IBasicProperties props = channel.CreateBasicProperties();
                        props.ContentType = "text/plain";
                        // 持久化消息
                        props.DeliveryMode = 2;
                        props.Expiration = "36000000";
                        channel.BasicPublish(
                            RabbitSettings.DirectExChange,
                            RabbitSettings.DirectExChangeRoutingKey,
                            props,
                            messageBodyBytes);
                        System.Threading.Thread.Sleep(1000);
                    }
                    // 发送失败，将抛出异常，恢复后，自动重连
                    catch (Exception ex)
                    {
                        Console.WriteLine("异常...");
                        System.Threading.Thread.Sleep(5000);
                    }
                }
            });
        }
    }
}
