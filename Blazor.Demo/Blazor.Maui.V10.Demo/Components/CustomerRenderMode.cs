namespace Microsoft.AspNetCore.Components.Web;

public static class CustomerRenderMode
{
    public static IComponentRenderMode InteractiveServerWithoutPrerender { get; } = new InteractiveServerRenderMode(prerender: false);
    public static IComponentRenderMode InteractiveAutoWithoutPrerender { get; } = new InteractiveAutoRenderMode(prerender: false);
    public static IComponentRenderMode InteractiveWebAssemblyWithoutPrerender { get; } = new InteractiveWebAssemblyRenderMode(prerender: false);
}
