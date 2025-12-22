using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Maui.Web.V10.Demo.Shared;

//https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui-blazor-web-app?view=aspnetcore-10.0#per-pagecomponent-server-interactivity
public static class InteractiveRenderSettings
{
	public static IComponentRenderMode? InteractiveServer { get; set; } =RenderMode.InteractiveServer;
	public static IComponentRenderMode? InteractiveAuto { get; set; } =RenderMode.InteractiveAuto;
	public static IComponentRenderMode? InteractiveWebAssembly { get; set; } =RenderMode.InteractiveWebAssembly;
	public static IComponentRenderMode? InteractiveServerWithoutPrerender { get; set; } = new InteractiveServerRenderMode(prerender: false);
	public static IComponentRenderMode? InteractiveAutoWithoutPrerender { get; set; } = new InteractiveAutoRenderMode(prerender: false);
	public static IComponentRenderMode? InteractiveWebAssemblyWithoutPrerender { get; set; } = new InteractiveWebAssemblyRenderMode(prerender: false);
	
	public static void ConfigureBlazorHybridRenderModes()
	{
		InteractiveServer = null;
		InteractiveAuto = null;
		InteractiveWebAssembly = null;
		InteractiveServerWithoutPrerender = null;
		InteractiveAutoWithoutPrerender = null;
		InteractiveWebAssemblyWithoutPrerender = null;
	}
}