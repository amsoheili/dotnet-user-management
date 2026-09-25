public class MyUser : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public List<MyUserRole> Roles { get; set; }

    public string? Username { get; set; }
    public string? HashedPassword { get; set; }
}