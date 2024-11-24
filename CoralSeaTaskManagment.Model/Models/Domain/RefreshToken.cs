using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Expires { get; set; }
        public bool Enabled { get; set; }
        //public User User { get; set; }
        public string UserEmail { get; set; }
    }
}
