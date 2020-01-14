using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt1.net.Models
{
    public class PagingViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public int TotalPage { get; set; }
    }
}
