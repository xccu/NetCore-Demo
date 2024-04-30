
using Microsoft.JSInterop;

namespace Blazor.WebAssembly.Demo.Services;

public class JSCacheService
{
    private readonly IJSInProcessRuntime _JS;

    public JSCacheService(IJSRuntime jSRuntime) => _JS = (IJSInProcessRuntime)jSRuntime;

    public void Set(string key, string value) => _JS.InvokeVoid("localStorage.setItem", key, value);
    public void Clean() => _JS.InvokeVoid("localStorage.clear");
    public string Get(string key) => _JS.Invoke<string>("localStorage.getItem", key);
    public void Remove(string key) => _JS.InvokeVoid("localStorage.removeItem", key);
}
