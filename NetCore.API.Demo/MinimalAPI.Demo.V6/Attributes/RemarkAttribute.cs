namespace MinimalAPI.Demo.V6.Attributes;

public class RemarkAttribute: Attribute
{
    public int Remark { get; set; }

    public RemarkAttribute(int remark)
    {
        Remark = remark;
    }


}
