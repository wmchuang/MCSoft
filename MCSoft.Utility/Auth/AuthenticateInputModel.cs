using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Utility.Auth
{
    public class AuthenticateInput
    {
        public string Code { get; set; }

        public Guid? HeadId { get; set; }
    }

    public class TeatAuthenticateInput
    {
        public string OpenId { get; set; }
        public Guid? HeadId { get; set; }
    }

    public class WxAuthenticateInputModel
    {
        public string NickName { get; set; }

        public string HeadImg { get; set; }
    }
}
