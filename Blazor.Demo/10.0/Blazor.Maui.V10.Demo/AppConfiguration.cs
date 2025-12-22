using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Maui.V10.Demo;
internal class AppConfiguration : IAppConfiguration
{
    public string DatabaseConnectionString => throw new NotImplementedException();

    public bool IsConfigured => throw new NotImplementedException();

    public void SaveConfiguration(Dictionary<string, string> config)
    {
        throw new NotImplementedException();
    }
}


public interface IAppConfiguration
{
    string DatabaseConnectionString { get; }
    // others
    bool IsConfigured { get; }
    void SaveConfiguration(Dictionary<string, string> config);
}