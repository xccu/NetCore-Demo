using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Custom.Extensions;

public static class PageConventionCollectionExtensions
{
    public static IPageApplicationModelConvention AddHandlerApplicationModelConvention(string pageName, Action<PageApplicationModel> action)
    {
        return null;
       
        //return Add(new PageApplicationModelConvention(pageName, action));
    }
}
