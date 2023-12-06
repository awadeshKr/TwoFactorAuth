using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Services.Abstract
{
    public interface ITFAService
    {
        public Task<bool> SendCode(VerificationCodeRequest request);
        public Task<bool> VerifyCode(VerifyConfirmationCodeRequest request);
    }
}
