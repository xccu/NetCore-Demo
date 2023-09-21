using SqlSugar;

namespace Services.Entities;

[SugarTable("T_ULOCK")]
public class ULock
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "LOCK_ID")]
    public int Id { get; set; }
    [SugarColumn(ColumnName = "LOCK_NAME")]
    public string Name { get; set; }
    [SugarColumn(IsEnableUpdateVersionValidation = true, ColumnName = "VERSION")] //tag version
    public long Ver { get; set; }
}
