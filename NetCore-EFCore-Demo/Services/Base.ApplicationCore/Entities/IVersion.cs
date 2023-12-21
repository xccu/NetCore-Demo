using System.ComponentModel.DataAnnotations;

namespace Base.ApplicationCore.Entities;

public interface IVersion
{
    [ConcurrencyCheck]
    public int VersionNo { get; set; }
}
