using Hangfire.Job.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.Job;

public class AttributeInfoProvider
{
    public AttributeInfoProvider()
    {
        var allPublicTypes = AssemblyLoadContext.Default.Assemblies
            .Where(assembley => !assembley.IsDynamic)
            .SelectMany(assembley => assembley.ExportedTypes);

        var jobTypes = allPublicTypes.Where(type => type.IsDefined(typeof(JobTypeAttribute)));

        foreach (var jobtype in jobTypes)
        {
            var runMethod = jobtype.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                   .FirstOrDefault(x => x.Name == "RunAsync");
            var jobDescription = jobtype.GetCustomAttribute<JobTypeAttribute>()!.Description;
        }
    }
}
