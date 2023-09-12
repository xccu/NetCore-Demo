using SqlSugar;

namespace Services.Entities;

[SugarTable("T_USER")]
public class User
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "USER_ID")]//数据库是自增才配自增
    public int id { get; set; }
    [SugarColumn(ColumnName = "USER_NAME")]
    public String name { get; set; }
    [SugarColumn(ColumnName = "PASSWORD")]
    public String password { get; set; }
    [SugarColumn(ColumnName = "AGE")]
    public int age { get; set; }
    [SugarColumn(ColumnName = "GENDER")]
    public String gender { get; set; }
    [SugarColumn(ColumnName = "RACE")]
    public String race { get; set; }

}