using Apache.NMS.ActiveMQ;
using System;
using Microsoft.Extensions.Configuration;
using Apache.NMS;
using Apache.NMS.Util;
using MQEntityModal.Modal;
using Newtonsoft.Json;
using Apache.NMS.ActiveMQ.Commands;
using System.Threading;

namespace MQueueDataService
{
    public class MQDataManageHelper
    {
        public static string URL { get; set; }

        private static IConnectionFactory connFac;

        private static IConnection connection;

        private static ISession session;

        private static IDestination destination;

        private static IMessageProducer producer;

        private static IMessageConsumer consumer;

        public MQDataManageHelper(string _url)
        {
            URL = _url;
        }

        /// <summary>

        /// 初始化ActiveMQ

        /// </summary>

        public void initAMQ()

        {
            string strsendTopicName = "A";//推送方topic名

            string strreceiveTopicName = "B";//接受方toptic名

            //var url = "localhost:61616";//activemq地址

            var userid = "admin";//帐户

            var pwd = "admin";//密码

            try

            {
                //connFac = new NMSConnectionFactory(new Uri("activemq:failover:(tcp://" + URL + ")")); //new NMSConnectionFactory(new Uri("activemq:failover:(tcp://localhost:61616)"));

                connFac = new NMSConnectionFactory(new Uri("activemq:failover:(tcp://DESKTOP-MVA6IN2:61616)"));
                //新建连接

                connection = connFac.CreateConnection();//connFac.CreateConnection("oa", "oa");//设置连接要用的用户名、密码

                //如果你要持久“订阅”，则需要设置ClientId，这样程序运行当中被停止，恢复运行时，能拿到没接收到的消息！

                connection.ClientId = "ClientId_" + strsendTopicName;

                //connection = connFac.CreateConnection();//如果你是缺省方式启动Active MQ服务，则不需填用户名、密码

                //创建Session

                session = connection.CreateSession();

                //发布/订阅模式，适合一对多的情况

                //destination = SessionUtil.GetDestination(session, "topic://" + strreceiveTopicName);

                //新建生产者对象

                //producer = session.CreateProducer(destination);

                //producer.DeliveryMode = MsgDeliveryMode.Persistent;//ActiveMQ服务器停止工作后，消息不再保留

                //新建消费者对象:普通“订阅”模式

                //consumer = session.CreateConsumer(destination);//不需要持久“订阅”

                //启动来自Active MQ的消息侦听
                connection.Start();

                //新建消费者对象:持久"订阅"模式：

                //    持久“订阅”后，如果你的程序被停止工作后，恢复运行，

                //从第一次持久订阅开始，没收到的消息还可以继续收

                consumer = session.CreateDurableConsumer(

                    session.GetTopic(strreceiveTopicName)

                    , connection.ClientId, null, false);

                //设置消息接收事件
                while (true)
                {
                    var message = consumer.Receive(new TimeSpan(1000));
                    if (message != null)
                    {
                        var ssd = message as ActiveMQObjectMessage;
                        if (ssd.Body is User)
                        {
                            Thread.Sleep(2000);
                            Console.WriteLine(JsonConvert.SerializeObject(ssd.Body));
                            //SysErrorLog.SaveErrorInfo("ActiveMQModel=" + );
                        }
                    }
                }
            }
            catch (Exception e)

            {
                Console.WriteLine($"初始化ActiveMQ失败,------{e.Message}");
            }
            finally
            {
                connection?.Close();
            }
        }

        public void sendAMQ(User user)

        {
            string strsendTopicName = "A";//推送方topic名

            string strreceiveTopicName = "B";//接受方toptic名

            //var url = "localhost:61616";//activemq地址

            var userid = "admin";//帐户

            var pwd = "admin";//密码

            try

            {
                //connFac = new NMSConnectionFactory(new Uri("activemq:failover:(tcp://" + URL + ")")); //new NMSConnectionFactory(new Uri("activemq:failover:(tcp://localhost:61616)"));

                connFac = new NMSConnectionFactory(new Uri("activemq:failover:(tcp://DESKTOP-MVA6IN2:61616)"));
                //新建连接

                connection = connFac.CreateConnection();//connFac.CreateConnection("oa", "oa");//设置连接要用的用户名、密码

                //如果你要持久“订阅”，则需要设置ClientId，这样程序运行当中被停止，恢复运行时，能拿到没接收到的消息！

                //connection.ClientId = "ClientId_" + strsendTopicName;

                //connection = connFac.CreateConnection();//如果你是缺省方式启动Active MQ服务，则不需填用户名、密码

                //创建Session

                session = connection.CreateSession();

                //发布/订阅模式，适合一对多的情况

                destination = SessionUtil.GetDestination(session, "topic://" + strreceiveTopicName);

                //新建生产者对象

                producer = session.CreateProducer(destination);

                producer.DeliveryMode = MsgDeliveryMode.Persistent;//ActiveMQ服务器停止工作后，消息不再保留

                //新建消费者对象:普通“订阅”模式

                //consumer = session.CreateConsumer(destination);//不需要持久“订阅”

                //启动来自Active MQ的消息侦听
                connection.Start();

                var i = session.CreateObjectMessage(user);

                producer.Send(i);
            }
            catch (Exception e)

            {
                Console.WriteLine($"初始化ActiveMQ失败,------{e.Message}");
            }
            finally
            {
                connection?.Close();
            }
        }

        /// <summary>

        /// 推送ActiveMQ

        /// </summary>

        /// <param name="guid"></param>

        /// <param name="t"></param>

        /// <param name="method"></param>

        public void Send(User user)

        {
            if (producer == null)

            {
                initAMQ();
            }

            if (session == null)

            {
                throw new Exception("请初始化ActiveMQ！");
            }

            if (producer == null)

            {
                throw new Exception("请初始化ActiveMQ！");
            }
        }

        /// <summary>

        /// 接收ActiveMQ消息

        /// </summary>

        /// <param name="receivedMsg"></param>

        protected static void OnMessage(IMessage receivedMsg)

        {
            if (receivedMsg is IObjectMessage)

            {
                var message = receivedMsg as IObjectMessage;

                if (message.Body is User)

                {
                    Console.WriteLine(JsonConvert.SerializeObject(message.Body));
                    //SysErrorLog.SaveErrorInfo("ActiveMQModel=" + );
                }
            }
        }
    }
}