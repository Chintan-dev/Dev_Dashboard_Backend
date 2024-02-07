namespace Dev_Dashboard.DTO
{
    public class UserDetailDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        //public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool Active { get; set; }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

    }
}
