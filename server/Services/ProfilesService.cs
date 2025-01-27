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
    // internal List<Profile> GetProfileKeeps()
    // {
    //     List<Profile> keeps = _repository.GetProfileKeeps();
    //     return keeps;
    // }
    // internal List<Profile> GetProfileVaults()
    // {
    //     List<Profile> keeps = _repository.GetProfileVaults();
    //     return keeps;
    // }



}