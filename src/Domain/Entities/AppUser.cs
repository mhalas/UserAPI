using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    [Table("AppUser")]
    public class AppUser: IId
    {
        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string Username { get; set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Indicates if the user has been soft-deleted.
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// The birth date of the user.
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
