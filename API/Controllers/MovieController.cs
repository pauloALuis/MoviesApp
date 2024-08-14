using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{

    [ApiController]
    //[Route("api/[Controller]")] // api/Movies
    [Route("api/[Controller]")]
    public class MoviesController : ControllerBase //BaseApiController
    {
        private readonly DataContext _context;

        public MoviesController(DataContext dataContext)
        {
            _context = dataContext;
        }
        
        #region     sync code 
        /*
        [HttpGet]
        public ActionResult<IEnumerable<AppMovie>> GetMovies(){
           var movies =  _context.Movies.ToList();
            return movies != null ? Ok(movies): BadRequest("No movies available"); //_context.Movies.ToList();
        }


        [HttpGet("{id}", Name = "Movies")]
        public ActionResult<AppMovie> GetMovie(int id)
        {
            if (id < 0 || id > _context.Movies.ToList().Count())
                return BadRequest(string.Format("there is no movie with this ID {0}", id));

            var movies = _context.Movies.Find(id);
            return movies != null ? Ok(movies) : BadRequest("No movies available"); //_context.Movies.ToList();
        }
        */
        #endregion ------------------------------

        // GET api/<ValuesController>/all 
          //https://localhost:5224/api/Movies   http://localhost:5223/api/Movies
          [AllowAnonymous]
          [HttpGet]
          public async Task<ActionResult<IEnumerable<AppMovie>>> GetMovies()
          {
              var movies = await _context.Movies.ToListAsync();
              return movies;
          }
          // GET api/<ValuesController>/5 
          // https://localhost:5224/api/Movie/6  http://localhost:5223/api/Movies/6
          [HttpGet("{id}", Name = "Movies")]
          public async Task<ActionResult<AppMovie>> GetMovies(int id)
          {
              var moveis = await _context.Movies.FindAsync(id);
              return moveis;
          }
/*
        // GET api/<ValuesController>/5 
        // https://localhost:5224/api/Movie/6  http://localhost:5223/api/Movies/6
        [HttpGet("{id}", Name = "MovieTest")]
          public ActionResult<AppMovie> GetMovieTest(int id)
          {
             // var moveis = _context.Movies.Find(id);
              if (moveis == null) return NotFound();
              return moveis;
          }


          // GET api/<ValuesController>/5 
          // https://localhost:5224/api/Movie/6  http://localhost:5223/api/Movies/6
          [HttpGet(Name = "MovieTest")]
          public IEnumerable<AppMovie> GetMoveiTest()
          {
              //var moveis =;
              //if (moveis == null) return NotFound();
              return _context.Movies.ToList<AppMovie>();
          }*/

    }
}