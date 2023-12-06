namespace TwoFactorAuth.Domain.Models
{
    public record VerifyConfirmationCodeRequest
    {
        public required string PhoneNumber { get; set; }
        public required string Code { get; set; }
    }
}
