using Hangfire.Client;
using Hangfire.Common;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.Job.Attributes;

public class JobTypeAttribute : JobFilterAttribute, IClientFilter, IElectStateFilter
{
    public string Description { get; }

    public JobTypeAttribute(string description = "") => Description = description;

    public void OnCreated(CreatedContext filterContext)
    {
        throw new NotImplementedException();
    }

    public void OnCreating(CreatingContext filterContext)
    {
        throw new NotImplementedException();
    }

    public void OnStateElection(ElectStateContext context)
    {
        throw new NotImplementedException();
    }
}
