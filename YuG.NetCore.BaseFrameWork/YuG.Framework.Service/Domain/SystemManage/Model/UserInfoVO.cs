using System;
using YuG.Framework;

namespace YuG.Framework.Service.Domain.SystemManage
{
    public class UserInfoVO
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Status { get; set; }

    }
}