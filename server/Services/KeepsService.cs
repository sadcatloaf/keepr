
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

    internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
    {
        Keep keep = GetKeepById(keepId);

        if (keep.CreatorId != userId) throw new Exception("You can not update anothers keep");

        keep.Img = keepData.Img ?? keep.Img;
        keep.Name = keepData.Name ?? keep.Name;
        keep.Description = keepData.Description ?? keep.Description;

        _keepsRepository.UpdateKeep(keep);

        return keep;

    }
}