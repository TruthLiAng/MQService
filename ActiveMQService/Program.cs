using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQDataService.BussinessLogicService;
using MQEntityModal.Modal;
using MQueueDataService;
using System;

namespace ActiveMQService
{
    internal class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        public Program()
        {
        }

        private static void Main(string[] args)
        {
            Startup startup = new Startup();
            Configuration = Startup.Configuration;

            var services = new ServiceCollection();
            var serviceProvider = startup.ConfigureServices(services);

            var userService = serviceProvider.GetService<UserService>();

            User user = new User()
            {
                guid = Guid.NewGuid(),
                Name = "t1"
            };

            //userService.CreateUser(user);

            var mqHelper = serviceProvider.GetService<MQDataManageHelper>();

            mqHelper.initAMQ();
            //mqHelper.Send(user);

            Console.WriteLine("success");
            Console.ReadLine();
        }
    }
}