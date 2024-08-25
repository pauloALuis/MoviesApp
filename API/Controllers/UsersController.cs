using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{

    // [ApiController]
    //[Route("api/[Controller]")] // route: api/User
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Get users all/ get user by id - ASYNC 
        // GET: api/<ValuesController> // api/values
        // https://localhost:500(5/6)/api/Users/  
        [AllowAnonymous]
        [HttpGet(Name = "Users")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _dataContext.Users.ToListAsync();
           // if(users == null) return Bad
            return users;
        }

        // GET api/<ValuesController>/5
        //https://localhost:500(5/6)/api/User/6   
        [HttpGet("{id}", Name = "User")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            
            try
            {
                if (id <= 0 || id >  _dataContext.Users.ToList().Count())
                    return BadRequest("User Id mismatch");


                var user = await _dataContext.Users.FindAsync(id);

                if (user == null)
                    return NotFound($"User with Id = {id} not found");

                return user;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error fetch user");
            }
        }

        #endregion ----------------------------------------------------------------------------------
        #region     sync service

        /*
        [AllowAnonymous]
        [HttpGet( Name = "Users")]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users =  _dataContext.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "User")]
        public ActionResult<AppUser> GetUser(int id)
        {
            if (id < 0 || id > _dataContext.Users.ToList().Count())
                return BadRequest(string.Format("there is no user with this ID {0}", id));

            var movies = _dataContext.Users.Find(id);
            return movies != null ? Ok(movies) : BadRequest("No user available"); //_context.Movies.ToList();
        }
        
        */
        #endregion -----------------------

    }
}