﻿using System.Collections.Generic;

namespace CommonCode.StaticData
{
    public interface ILoginInfo
    {
        string LoginUrl { get; set; }
        string WashingtonId { get; set; }
        string WashingtonPassword { get; set; }
        string IdahoId { get; set; }
        string IdahoPassword { get; set; }

        string MailerAddress { get; set; }
        string MailerPassword { get; set; }
        List<string> EmailRecipients { get; set; }
    }
}