
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Geteway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //读取Ocelot的配置文件
            builder.Configuration.AddJsonFile("ocelot.json", true, true);


            //一. 网关的使用
            //1. nuget引入: Ocelot程序包
            //2. 需要配置文件来参与配置----新建一个配置文件--读取配置文件
            //3. IOC注册Ocelot+ Use 使用Ocelot
            //4. 配置Ocelot的配置信息


            #region IOC扩展负载均衡策略
            //Func<IServiceProvider, DownstreamRoute, IServiceDiscoveryProvider, CustomPollingLoadBalancer> loadBalancerFactoryFunc = (serviceProvider, Route, serviceDiscoveryProvider) => new CustomPollingLoadBalancer(serviceDiscoveryProvider.Get);
            #endregion





            // Add services to the container.


            builder.Services.AddOcelot();
            //注意: nuget引入: Ocelot.Provider.Consul
            // .AddConsul() //Ocelot注册到IOC容器
            //扩展 Ocelot-缓存
            //Nuet: Ocelot.Cache.CacheManager
            //.AddCacheManager(x =>
            //{
            //    x.WithDictionaryHandle();//字典缓存
            //    //如果我自己来扩展下, 可能会使用redis  Mongo  memaryCache 
            //})
            //.AddCustomLoadBalancer<CustomPollingLoadBalancer>  (loadBalancerFactoryFunc);  //配置生效自定义负载均衡策略


            //自定义的Ocelot支持的缓存扩展 ---生效
            // builder.Services.AddSingleton<IOcelotCache<CachedResponse>, CustomCacheExtend>();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseSwagger();
            app.UseSwaggerUI(
            c =>
            {
          var serviceNames = new List<string> { "cas" , "acis" };
              serviceNames.ForEach(x => {
                  c.SwaggerEndpoint($"{x}", $"{x}-webapi");
              });
      });


            app.UseOcelot();  //使用


            //这里是网关---用来做请求的转发

            //app.UseHttpsRedirection(); 
            //app.UseAuthorization(); 
            //app.MapControllers();

            app.Run();
        }
    }
}
