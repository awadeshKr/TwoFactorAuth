using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuth.Services.Abstract;

namespace TwoFactorAuth.Services.Concrete
{
    public class MessageProvider : IMessageProvider
    {
        private readonly ILogger<MessageProvider> _logger;
        public MessageProvider(ILogger<MessageProvider> logger)
        {
            _logger = logger;
        }

        public Task<bool> SendMessage(string phone, string message)
        {
            _logger.LogInformation($@"{message}- is send to {phone}");
            return Task.FromResult(true);
        }
    }
}
