using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Pasword { get; set; }
        public string RndNum { get; set; }
        public int IsAdmin { get; set; }
        public int IsLock { get; set; }
        public string Name { get; set; }
    }
}
