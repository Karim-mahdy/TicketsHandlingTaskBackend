using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsHandling.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string PhoneNumber { get; set; }
        public int Governorate { get; set; }
        public int City { get; set; }
        public int District { get; set; }
        public bool IsHandled { get; set; }
        public string StatusColor { get; set; } = string.Empty;
    }
}
