
namespace keepr.Repositories;

public class AccountsRepository
{
  private readonly IDbConnection _db;

  public AccountsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetByEmail(string userEmail)
  {
    string sql = "SELECT * FROM accounts WHERE email = @userEmail";
    return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
  }

  internal Account GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Account>(sql, new { id });
  }

  internal Account Create(Account newAccount)
  {
    string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
    _db.Execute(sql, update);
    return update;
  }

  internal List<Vault> GetMyVaults(string accountId)
  {
    string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM accounts
        JOIN vaults ON vaults.creator_id = accounts.id
        WHERE accounts.id = @accountId;";
    List<Vault> vaults = _db.Query(sql, (Vault vault, Account account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { accountId }).ToList();
    return vaults;
  }


  internal void UpdateAccount(Account updateData)
  {
    string sql = @"
        UPDATE accounts
        SET
        name = @Name,
        picture = @Picture,
        cover_img = @CoverImg
        WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, updateData);

    if (rowsAffected != 1) throw new Exception($"{rowsAffected} were updated and that bad juju");
  }
}

