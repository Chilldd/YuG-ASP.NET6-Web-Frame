using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Generators
{
    public static class BuildCodeHelper
    {
        public static SqlSugarClient GetDbClient(DbOption option)
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = option.Connection,
                DbType = option.DbType
            });
        }

        public static string UpperEntity(string str)
        {
            return UpperStr(str) + "Entity";
        }

        public static string UpperStr(string str)
        {
            if (str == "id")
                return str.ToUpper();
            // 字符串缓冲区
            StringBuilder sbf = new StringBuilder();
            // 如果字符串包含 下划线
            if (str.Contains("_"))
            {
                // 按下划线来切割字符串为数组
                string[] split = str.Split("_");
                // 循环数组操作其中的字符串
                for (int i = 0, index = split.Length; i < index; i++)
                {
                    // 递归调用本方法
                    string upperTable = UpperStr(split[i]);
                    // 添加到字符串缓冲区
                    sbf.Append(upperTable);
                }
            }
            else
            {
                // 字符串不包含下划线
                // 转换成字符数组
                char[] ch = str.ToCharArray();
                // 判断首字母是否是字母
                if (ch[0] >= 'a' && ch[0] <= 'z')
                {
                    // 利用ASCII码实现大写
                    ch[0] = (char)(ch[0] - 32);
                }
                // 添加进字符串缓存区
                sbf.Append(ch);
            }
            // 返回
            return sbf.ToString();
        }

        public static List<string> IgnoreDbColumn()
        {
            return new List<string>(6)
            {
                "id","create_time","create_user","update_time","update_user","del_flag"
            };
        }
    }
}
