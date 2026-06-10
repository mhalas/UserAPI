namespace Application.Users.Common
{
    public class UserDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
