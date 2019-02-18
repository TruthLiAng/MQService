using MQEntityModal.Modal;
using MQueueDataService;
using System;
using System.Threading;

namespace MQSendText
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mqHelper = new MQDataManageHelper("127.0.0.1:61616");

            int i = 0;
            while (true)
            {
                User user = new User()
                {
                    guid = Guid.NewGuid(),
                    Name = "t1",
                    Memo = i.ToString()
                };
                mqHelper.sendAMQ(user);
                i++;

                Thread.Sleep(100);

                Console.WriteLine(i);
            }
        }
    }
}