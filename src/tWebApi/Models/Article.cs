using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tWebApi.Models
{
    public class Article
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
    }
}
