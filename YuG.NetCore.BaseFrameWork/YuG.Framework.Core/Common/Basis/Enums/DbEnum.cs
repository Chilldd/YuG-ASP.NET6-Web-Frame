using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    public static class DbEnum
    {
        public enum DelFlag
        {
            Delete = 0,
            UnDelete = 1
        }

        public enum Status
        {
            Fail = 0,
            Valid = 1
        }
    }
}
