using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Maui.V10.Demo;
public class DynamicServiceProvider
{
    private IServiceProvider _serviceProvider;
    private IServiceCollection _services;
    private readonly IAppConfiguration _configuration;

    public DynamicServiceProvider(IAppConfiguration configuration)
    {
        _configuration = configuration;
        _services = new ServiceCollection();
        RegisterCoreServices();
    }

    private void RegisterCoreServices()
    {
        // 注册不依赖配置的核心服务
        _services.AddSingleton<IAppConfiguration>(_configuration);
        _services.AddSingleton(this);
    }

    public void RegisterConfiguredServices()
    {
        if (!_configuration.IsConfigured)
            throw new InvalidOperationException("应用未配置");

        // 动态注册依赖配置的服务


        // 注册其他依赖配置的服务
        _services.AddScoped(sp =>  new MyService() { Name = "Bar" });

        // 构建ServiceProvider
        _serviceProvider = _services.BuildServiceProvider();
    }

    public IServiceProvider GetServiceProvider() =>
        _serviceProvider ?? throw new InvalidOperationException("服务未初始化");

    public T GetService<T>() => GetServiceProvider().GetService<T>();
}
