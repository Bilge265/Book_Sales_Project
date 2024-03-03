using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BasketItem
    {
        public int Id { get; set; }
        public virtual Book Books { get; set; }
    }
}
