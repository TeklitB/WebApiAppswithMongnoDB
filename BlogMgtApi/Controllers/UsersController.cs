using BlogMgtApi.Models;
using BlogMgtApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogMgtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository) 
        {
            _usersRepository = usersRepository;
        }

        [HttpPost]
        [Route(nameof(AddUser))]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            await _usersRepository.InsertUser(user);
            return Ok();
        }
    }
}
