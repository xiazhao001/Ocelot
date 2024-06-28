
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

            //��ȡOcelot�������ļ�
            builder.Configuration.AddJsonFile("ocelot.json", true, true);


            //һ. ���ص�ʹ��
            //1. nuget����: Ocelot�����
            //2. ��Ҫ�����ļ�����������----�½�һ�������ļ�--��ȡ�����ļ�
            //3. IOCע��Ocelot+ Use ʹ��Ocelot
            //4. ����Ocelot��������Ϣ


            #region IOC��չ���ؾ������
            //Func<IServiceProvider, DownstreamRoute, IServiceDiscoveryProvider, CustomPollingLoadBalancer> loadBalancerFactoryFunc = (serviceProvider, Route, serviceDiscoveryProvider) => new CustomPollingLoadBalancer(serviceDiscoveryProvider.Get);
            #endregion





            // Add services to the container.


            builder.Services.AddOcelot();
            //ע��: nuget����: Ocelot.Provider.Consul
            // .AddConsul() //Ocelotע�ᵽIOC����
            //��չ Ocelot-����
            //Nuet: Ocelot.Cache.CacheManager
            //.AddCacheManager(x =>
            //{
            //    x.WithDictionaryHandle();//�ֵ仺��
            //    //������Լ�����չ��, ���ܻ�ʹ��redis  Mongo  memaryCache 
            //})
            //.AddCustomLoadBalancer<CustomPollingLoadBalancer>  (loadBalancerFactoryFunc);  //������Ч�Զ��帺�ؾ������


            //�Զ����Ocelot֧�ֵĻ�����չ ---��Ч
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


            app.UseOcelot();  //ʹ��


            //����������---�����������ת��

            //app.UseHttpsRedirection(); 
            //app.UseAuthorization(); 
            //app.MapControllers();

            app.Run();
        }
    }
}
