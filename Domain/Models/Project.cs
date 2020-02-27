using System.Collections.Generic;

namespace JwtApi.Domain.Models {
    public class Project {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public User Creator {get; set;}
        public int CreatorId {get; set;}
    }
}