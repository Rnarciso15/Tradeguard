namespace Tradeguard2.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class UserDto
    {
        public string Id { get; set; }
        public string CC { get; set; }
        public string Nome { get; set; }
        public string Telemovel { get; set; }
        public string Email { get; set; }
        public int Avaliacao { get; set; }
        public int N_validados { get; set; }
        public string AvatarUrl { get; set; }
        public decimal Saldo { get; set; }

    }

}
