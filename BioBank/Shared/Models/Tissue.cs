using BioBank.Shared.Interfaces;

namespace BioBank.Shared.Models
{
    public class Tissue : IBioBank
    {
        public int Id { get; set; }
        public string DieaseTerm { get; set; }
        public string Title { get; set; }
    }
}
