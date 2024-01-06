using BioBank.Shared.Interfaces;

namespace BioBank.Shared.Models
{
    public class Samples : ISamples
    {
        public int BioBankId { get; set; }
        public int Sample_Id { get; set; }
        public int Donor_Count { get; set; }
        public string Material_Type { get; set; }
        public DateTime Last_updated { get; set; }
    }
}
