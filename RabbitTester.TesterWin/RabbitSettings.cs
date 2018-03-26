using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitTester.TesterWin
{
    /// <summary>
    /// 消息队列设置
    /// </summary>
    public class RabbitSettings
    {
        public const string User = "rabbit";
        public const string Pass = "rabbit";
        public const string VHost = "testHost";
        public const int Port = 5672;
        public const string HostName = "192.168.56.100";

        // 直连交换机
        public const string DirectQueue = "testDirectQueue";
        public const string DirectExChange = "TestDirectExChange";
        public const string DirectExChangeRoutingKey = "TestDirectExChangeRouingKey";

        // 扇形交换机
        public const string FanoutQueue = "testFanoutQueue";
        public const string FanoutExChange = "TestFanoutExChange";

        // 主题交换机
        public const string TopicQueue = "testTopicQueue";
        public const string TopicExChange = "TestTopicExChange";
    }
}
