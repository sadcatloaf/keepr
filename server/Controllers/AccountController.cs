using System.Threading.Tasks;

namespace keepr.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> accounts = _accountService.GetMyVaults(userInfo.Id);
      return Ok(accounts);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [HttpPut]
  public async Task<ActionResult<Account>> UpdateAccount([FromBody] Account accountData)
  {
    try
    {
      Account userinfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Account account = _accountService.UpdateAccount(userinfo.Id, accountData);
      return Ok(account);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}
