namespace Dev_Dashboard.DTO
{
    public class UserMenuDTO
    {
        public int Id { get; set; }

        public string MenuName { get; set; } = null!;

        public string? MenuDescription { get; set; }
        public string? Path { get; set; } = string.Empty;
    }
}
