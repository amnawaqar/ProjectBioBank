using BioBank.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BioBank.Shared.Models
{
    public class Tissue : IBioBank
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DieaseTerm { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
