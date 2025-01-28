
namespace keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  private Account GetAccount(string accountId)
  {
    Account account = _repo.GetById(accountId);
    if (account == null)
    {
      throw new Exception("Invalid Account Id");
    }
    return account;
  }

  internal Account GetOrCreateAccount(Account userInfo)
  {
    Account account = _repo.GetById(userInfo.Id);
    if (account == null)
    {
      return _repo.Create(userInfo);
    }
    return account;
  }

  internal Account Edit(Account editData, string accountId)
  {
    Account original = GetAccount(accountId);
    original.Name = editData.Name ?? editData.Name;
    original.Picture = editData.Picture ?? editData.Picture;
    return _repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string accountId)
  {
    List<Vault> accounts = _repo.GetMyVaults(accountId);
    return accounts;
  }

  internal Account UpdateAccount(string userId, Account accountData)
  {
    Account account = GetAccount(userId);

    if (account.Id != userId) throw new Exception("You can not update another account");

    account.Name = accountData.Name ?? account.Name;
    account.Picture = accountData.Picture ?? account.Picture;
    account.CoverImg = accountData.CoverImg ?? account.CoverImg;

    _repo.UpdateAccount(account);

    return account;

  }
}
