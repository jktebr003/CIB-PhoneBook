using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class PhoneBook
    {
        public int Id { get; set; }
        public IQueryable<Entry> Entries { get; set; }
    }
}
