using MCSoft.Utility.Encrypt;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 后台管理员
    /// </summary>
    public class Manager : FullAuditedAggregateRoot<Guid>
    {
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual bool IsDisabled { get; set; }

        private Manager()
        {
        }

        public Manager(Guid id, string userName, string password, string passwordSalt)
            : base(id)
        {
            PasswordSalt = Guid.NewGuid().ToString();
            UserName = userName;
            Password = MD5Util.GetMD5(password + PasswordSalt);
        }


        public void Disable()
        {
            this.IsDisabled = true;
        }

        public void Enable()
        {
            this.IsDisabled = false;
        }

        public bool VerifyPassword(string password)
        {
            if (MD5Util.GetMD5(password + PasswordSalt) == Password)
            {
                return true;
            }
            return false;
        }
    }
}
