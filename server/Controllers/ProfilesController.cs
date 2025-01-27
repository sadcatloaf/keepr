namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]

public class ProfilesController : ControllerBase
{
    public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, KeepsService keepsService, VaultsService vaultsService)
    {
        _auth0Provider = auth0Provider;
        _profilesService = profilesService;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }

    private readonly Auth0Provider _auth0Provider;
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    // [Authorize]
    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfile(string profileId)
    {
        try
        {

            Profile profiles = _profilesService.GetProfile(profileId);
            return Ok(profiles);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
    {
        try
        {
            List<Keep> keeps = _keepsService.GetProfileKeeps(profileId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    // [HttpGet("{profileId}/keeps")]
    // public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
    // {
    //     try
    //     {
    //         // Fetch keeps for the profile using the service
    //         List<Keep> keeps = _keepsService.GetProfileKeeps(profileId);
    //         return Ok(keeps);
    //     }
    //     catch (Exception exception)
    //     {
    //         return BadRequest(exception.Message);
    //     }
    // }


}






// [HttpGet("{profileId}/vaults")]
// public ActionResult<List<Profile>> GetProfileVaults()
// {
//     try
//     {
//         List<Profile> profileKeeps = _profilesService.GetProfileVaults();
//         return Ok(profileKeeps);
//     }
//     catch (Exception exception)
//     {
//         return BadRequest(exception.Message);
//     }
// }