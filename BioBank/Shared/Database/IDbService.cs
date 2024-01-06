using BioBank.Shared.Models;

namespace BioBank.Shared.Database
{
    public interface IDbService
    {
        List<Models.Tissue> GetBioBankCollections();
        List<Samples> GetSamples(int id);
        bool AddNewCollection(Models.Tissue bioBankService);
        bool AddNewSample(Samples samplesService);
    }
}
