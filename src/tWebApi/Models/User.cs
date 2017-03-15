using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tWebApi.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Article> Articles { get; set; }

    }
}
