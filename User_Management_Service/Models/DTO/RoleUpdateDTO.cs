namespace User_Management_Service.Models.DTO
{
    public class RoleUpdateDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
