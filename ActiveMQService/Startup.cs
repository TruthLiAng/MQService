using Autofac;
using Autofac.Extensions.DependencyInjection;
using EntityFrameworkCore.DbContextScope;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQDataService.BussinessLogicService;
using MQDataService.Repositories.UserRep;
using MQEntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;

namespace ActiveMQService
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MQDbcontext>(options =>
            options.UseNpgsql(Configuration.GetValue("ConnectionStrings:PostgreSql", "Debug")));

            return RegisterAutofac(services);
        }

        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
            //实例化Autofac容器
            var builder = new ContainerBuilder();
            //将Services中的服务填充到Autofac中
            builder.Populate(services);
            //新模块组件注册
            builder.RegisterModule<AutofacModuleRegister>();
            //创建容器
            var Container = builder.Build();
            //第三方IOC接管 core内置DI容器
            return new AutofacServiceProvider(Container);
        }

        public class AutofacModuleRegister : Autofac.Module
        {
            //重写Autofac管道Load方法，在这里注册注入
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>();
                builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>();

                builder.Register(c => new UserRepository(c.Resolve<IAmbientDbContextLocator>())).As<IUserRepository>();
                builder.Register(c => new UserService(c.Resolve<IDbContextScopeFactory>(), c.Resolve<IUserRepository>()));
            }

            /// <summary>
            /// 根据程序集名称获取程序集
            /// </summary>
            /// <param name="AssemblyName">程序集名称</param>
            /// <returns></returns>
            public static Assembly GetAssemblyByName(String AssemblyName)
            {
                return Assembly.Load(AssemblyName);
            }
        }
    }
}