namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]

public class ProfilesController : ControllerBase
{
    public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider)
    {
        _auth0Provider = auth0Provider;
        _profilesService = profilesService;
    }

    private readonly Auth0Provider _auth0Provider;
    private readonly ProfilesService _profilesService;

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

    // [HttpGet("{profileId}/keeps")]
    // public ActionResult<List<Profile>> GetProfileKeeps()
    // {
    //     try
    //     {
    //         List<Profile> profileKeeps = _profilesService.GetProfileKeeps();
    //         return Ok(profileKeeps);
    //     }
    //     catch (Exception exception)
    //     {
    //         return BadRequest(exception.Message);
    //     }
    // }

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
}