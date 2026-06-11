using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("AppUser")]
    public class AppUser: IId
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
