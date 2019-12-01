using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiNuveo.Models
{
    [Table("Workflow")]
    public class Workflow
    {
        [Key]
        public Guid UUID { get; set; }
        public int status { get; set; }
        public string data { get; set; }
        public string steps { get; set; }
    }
}