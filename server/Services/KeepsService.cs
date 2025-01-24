
namespace keepr.Services;

public class KeepsService
{
    public KeepsService(KeepsRepository keepsRepository)
    {
        _keepsRepository = keepsRepository;
    }

    private readonly KeepsRepository _keepsRepository;

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _keepsRepository.CreateKeep(keepData);
        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _keepsRepository.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _keepsRepository.GetKeepById(keepId);

        if (keep == null) throw new Exception($"Invalid keep id: {keepId}");
        return keep;
    }
}