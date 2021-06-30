using Application.Profiles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile (string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { UserName = username }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedProfile (Update.Command updatedProfile)
        {
            return HandleResult(await Mediator.Send(new Update.Command 
            {  
                DisplayName = updatedProfile.DisplayName, 
                Bio = updatedProfile.Bio 
            }));
        }
    }
}
