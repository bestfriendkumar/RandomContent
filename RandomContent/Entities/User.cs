namespace RandomContent.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }

    public enum Role
    {
        /// <summary>
        /// Regular User
        /// </summary>
        User,

        /// <summary>
        /// Privileged user that gets access to more content
        /// </summary>
        PrivilegedUser
    }
}