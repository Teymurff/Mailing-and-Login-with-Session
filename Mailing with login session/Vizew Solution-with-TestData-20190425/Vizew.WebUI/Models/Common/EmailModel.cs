using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vizew.WebUI.Models.Common
{
    public class EmailModel
    {
        public int Id { get; set; }
        public string ToMails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}