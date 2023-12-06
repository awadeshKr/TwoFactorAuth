using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain.Providers
{
    public class InMemoryStorage
    {
        public List<TwoFactorCodes> TwoFactorCodes;

        private InMemoryStorage()
        {
            TwoFactorCodes = new();
        }

        private static readonly Lazy<InMemoryStorage> InstanceValue = new Lazy<InMemoryStorage>(() => new InMemoryStorage());
        public static InMemoryStorage Instance => InstanceValue.Value;
    }
}
