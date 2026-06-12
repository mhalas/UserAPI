namespace Application.Users.Common
{
    /// <summary>
    /// Data transfer object for user information.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The birth date of the user.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Indicates if the user is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
