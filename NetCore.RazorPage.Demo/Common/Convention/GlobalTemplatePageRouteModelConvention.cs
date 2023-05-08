﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Common.Convention;

//see:
//https://learn.microsoft.com/en-us/aspnet/core/razor-pages/razor-pages-conventions?view=aspnetcore-6.0
public class GlobalTemplatePageRouteModelConvention : IPageRouteModelConvention
{
    public void Apply(PageRouteModel model)
    {
        var selectorCount = model.Selectors.Count;
        for (var i = 0; i < selectorCount; i++)
        {
            var selector = model.Selectors[i];
            model.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel
                {
                    Order = 1,
                    Template = AttributeRouteModel.CombineTemplates(
                        selector.AttributeRouteModel!.Template,
                        "{globalTemplate?}"),
                }
            });
        }
    }
}
