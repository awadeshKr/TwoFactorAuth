namespace TwoFactorAuth.Domain.Models
{
    public sealed class TwoFactorAuthConfig
    {
        public int CodeLifetimeMinutes { get; set; }
        public int MaxConcurrentCodesPerPhone { get; set; }
    }
}
