using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuth.Services.Abstract
{
    public interface IMessageProvider
    {
       Task<bool> SendMessage(string phone,string message);
    }
}
