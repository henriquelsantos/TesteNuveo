using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiNuveo.Models
{
    public class Enumera
    {
        public enum WorkflowEnums
        {
            inserted = 1,
            consumed = 2
        }
    }
}