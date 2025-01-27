
using Microsoft.AspNetCore.Http;

namespace keepr.Services;

public class KeepsService
{
    public KeepsService(KeepsRepository repository)
    {
        _repository = repository;
    }

    private readonly KeepsRepository _repository;

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repository.CreateKeep(keepData);
        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repository.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);

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

        _repository.UpdateKeep(keep);

        return keep;

    }

    internal string DeleteKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);

        if (keep.CreatorId != userId) throw new Exception("You can not delete users Keep");
        _repository.DeleteKeep(keepId);
        return $"Deleted {keep.Name}";
    }

    internal List<Keep> GetProfileKeeps(string profileId)
    {
        List<Keep> keeps = _repository.GetProfileKeeps(profileId);
        return keeps;
    }
}