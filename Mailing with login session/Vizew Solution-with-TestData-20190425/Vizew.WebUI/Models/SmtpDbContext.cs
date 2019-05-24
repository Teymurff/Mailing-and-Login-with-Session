using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Vizew.WebUI.Models.Entity;

namespace Vizew.WebUI.Models
{
    public class SmtpDbContext:DbContext
    {
   
        public SmtpDbContext()
            :base("name=cString")
        {
    
          
        }


    
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }

    }
}