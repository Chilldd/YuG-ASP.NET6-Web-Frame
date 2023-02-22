﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    /// <summary>
    /// 开启事务切面的标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class UseTranAttribute : Attribute
    {

    }
}
