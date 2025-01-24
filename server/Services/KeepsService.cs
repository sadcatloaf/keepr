
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
}