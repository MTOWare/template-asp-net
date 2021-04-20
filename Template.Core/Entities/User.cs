using System;

namespace Template.Core.Entities
{
    public partial class User : BaseEntity
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
