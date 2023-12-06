namespace TwoFactorAuth.Domain.Models
{
    public record VerificationCodeRequest
    {
        public required string PhoneNumber { get; set; }        
    }
}
