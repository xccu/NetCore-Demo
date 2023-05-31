using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom;

public enum Lifetime
{
    Root,       //生命周期为单例
    Self,       //生命周期与Cat容器一致
    Transient   //生命周期瞬时
}