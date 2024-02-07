namespace Dev_Dashboard.DTO
{
    public class UserAssignMenuDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int MenuId { get; set; }

        public string MenuName { get; set; } = string.Empty;
        public string? MenuDescription { get; set; } =string.Empty;

    }
}
