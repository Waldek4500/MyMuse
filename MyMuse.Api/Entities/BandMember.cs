namespace MyMuse.Api.Entities;

public class BandMember
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public int BandId { get; set; }
    public Band? Band { get; set; }
}
