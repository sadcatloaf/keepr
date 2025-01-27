namespace keepr.Services;

public class ProfilesService
{
    public ProfilesService(ProfilesRepository repository)
    {
        _repository = repository;
    }

    private readonly ProfilesRepository _repository;


    internal Profile GetProfile(string profileId)
    {
        Profile profiles = _repository.GetProfile(profileId);
        return profiles;
    }

}