using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTester.TesterWin
{

    public class FanoutExchangePublisher
    {
        public int PubliserId { get; set; }

        protected IConnection Connection { get; set; }

        public FanoutExchangePublisher(int publiserId, IConnection con)
        {
            PubliserId = publiserId;
            Connection = con;
        }

        public static int MessageId = 0;

        /// <summary>
        /// 启动一个发布者
        /// </summary>
        public void Start()
        {
            var channel = Connection.CreateModel();

            //var result = channel.QueueDelete(RabbitSettings.DirectQueue);
            //channel.ExchangeDelete(RabbitSettings.DirectExChange);

            // 定义一个扇形交换机，并持久化保存
            channel.ExchangeDeclare(RabbitSettings.FanoutExChange,
                ExchangeType.Fanout, durable: true);           

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
                            RabbitSettings.FanoutExChange,  
                            "",
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
