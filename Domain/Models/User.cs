using System.Collections.Generic;

namespace JwtApi.Domain.Models {
    public class User {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public string Token { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role {get; set;}
        public IList<Project> Projects {get; set; } = new List<Project>();
    }
}