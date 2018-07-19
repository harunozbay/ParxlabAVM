using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParxlabAVM.Identity
{
    public class ApplicationRole:IdentityRole
    {
        public string Descripton { get; set; }
    }
}