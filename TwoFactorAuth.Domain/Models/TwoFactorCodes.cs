using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuth.Domain.Models
{
    public class TwoFactorCodes
    {
        public required string PhoneNumber { get; set; }
        public required string Code { get; set; }
        public DateTime GenreatedDateTime { get; set; }
    }
}
