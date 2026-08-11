public class MyUserRole : BaseEntity
{
    public string UserId { get; set; }
    public MyUser Person { get; set; }
    public UserRolesEnum Role { get; set; }
}