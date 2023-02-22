using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class NotRegisterAttribute : Attribute
    {
    }
}
