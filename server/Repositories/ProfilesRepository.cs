namespace keepr.Repositories;

public class ProfilesRepository
{
    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;


    internal Profile GetProfile(string profileId)
    {
        string sql = @"
        SELECT
        accounts.*,
        accounts.id AS Id
        FROM accounts
        WHERE accounts.id = @profileId;";

        Profile profiles = _db.Query(sql, (Profile profile, Account account) =>
        {
            profile.Id = account.Id;
            return profile;
        }, new { profileId }, splitOn: "Id").SingleOrDefault();
        return profiles;
    }

}