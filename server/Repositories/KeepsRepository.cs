namespace keepr.Repositories;

public class KeepsRepository
{
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    public Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO
        keeps(name, description, img, views, creator_id)
        VALUES(@Name, @Description, @Img, @Views, @CreatorId);

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        WHERE keeps.id = LAST_INSERT_ID();";

        Keep keep = _db.Query(sql, (Keep keep, Account account) =>
        {
            keep.Creator = account;
            return keep;
        }, keepData).SingleOrDefault();

        return keep;
    }
}

//   public int Id { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public DateTime UpdatedAt { get; set; }
//     public string Name { get; set; }
//     public string Description { get; set; }
//     public string Img { get; set; }
//     public int Views { get; set; }
//     public string CreatorId { get; set; }

//     public Account Creator { get; set; }
//     public int Kept { get; set; }