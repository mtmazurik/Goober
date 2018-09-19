using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;

namespace ECA.Services.Goober.DAL.Models
{
    [Table("PeanutButter")]
    public partial class PeanutButter
    {
        public int PeanutButterId { get; set; }
        public string Brand { get; set; }
        public string IsChunky { get; set; }
        public string JsonData { get; set; }
    }
}
