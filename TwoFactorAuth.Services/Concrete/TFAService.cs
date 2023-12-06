using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwoFactorAuth.Domain.Models;
using TwoFactorAuth.Domain.Providers;
using TwoFactorAuth.Services.Abstract;

namespace TwoFactorAuth.Services.Concrete
{
    public class TFAService: ITFAService
    {
        private readonly InMemoryStorage storage;
        private readonly IMessageProvider _messageProvider;
        private readonly IOptions<TwoFactorAuthConfig> _authConfig;
        private readonly ILogger<TFAService> _logger;

        public TFAService(IMessageProvider messageProvider, IOptions<TwoFactorAuthConfig> authConfig, ILogger<TFAService> logger)
        {
            storage = InMemoryStorage.Instance;
            _messageProvider = messageProvider;
            this._authConfig = authConfig;
            _logger = logger;
        }

        
        /// <summary>
        /// Method to send verification code to number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SendCode(VerificationCodeRequest request)
        {
            if(storage.TwoFactorCodes is not null && storage.TwoFactorCodes.Count > 0)
            {
                var codes = storage.TwoFactorCodes.Where(x => x.PhoneNumber == request.PhoneNumber)?.ToList();
                if(codes is not null && codes.Count >= _authConfig.Value.MaxConcurrentCodesPerPhone)
                {
                    _logger.LogError($"Max concurrent codes per phone reached for {request.PhoneNumber}");
                    return false; 
                }
            }
            
            TwoFactorCodes code = new()
            {
                Code = new Random().Next(10000, 100000).ToString(),
                PhoneNumber = request.PhoneNumber,
                GenreatedDateTime = DateTime.UtcNow,
            };
            storage.TwoFactorCodes?.Add(code);
            return await _messageProvider.SendMessage(request.PhoneNumber, $"Use verification code - {code.Code} ").ConfigureAwait(false);
        }

        /// <summary>
        /// Method to verify verification code to number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<bool> VerifyCode(VerifyConfirmationCodeRequest request)
        {
            if (storage.TwoFactorCodes is not null && storage.TwoFactorCodes.Count > 0)
            {
                var codes = storage.TwoFactorCodes.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber && x.Code.Equals(request.Code) && (x.GenreatedDateTime - DateTime.UtcNow).TotalMinutes < _authConfig.Value.CodeLifetimeMinutes);
                if (codes is not null )
                {
                    _logger.LogInformation($"Confirmation code {request.Code} validated for {request.PhoneNumber}");
                    storage.TwoFactorCodes.Remove(codes);
                    return Task.FromResult(true);
                }                
            }
            _logger.LogError($"Invalid confirmation code {request.Code} for {request.PhoneNumber}");

            return Task.FromResult(false);
        }
    }
}
