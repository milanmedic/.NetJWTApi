using System.ComponentModel.DataAnnotations;

namespace JwtApi.Domain.Models {
    public class ProjectDto {
        [Required]
        [StringLength(150)]
        public string Name {get; set;}
        [Required]
        [StringLength(150)]
        public string Description {get; set;}
        [Required]
        public int CreatorId {get; set;}
    }
}