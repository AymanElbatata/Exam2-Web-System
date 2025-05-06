namespace AYMDating.API.DTO
{
    public class UserDTO
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? UserRole { get; set; }
        public bool IsActivated { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpirationDate { get; set; } = DateTime.Now;


    }
}
