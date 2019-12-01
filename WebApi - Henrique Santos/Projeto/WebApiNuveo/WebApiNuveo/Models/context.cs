using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApiNuveo.Models;

namespace WebApiNuveo.Models
{
    public class context : DbContext
    {
        public context() : base("name=NuveoConn")
        { 
        
        }

        public DbSet<Workflow> WorkFlow { get; set; }
    }
}