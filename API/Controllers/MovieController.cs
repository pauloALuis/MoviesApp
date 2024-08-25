using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{

    //[ApiController]
    [Route("api/[Controller]")] // api/Movies
    [Authorize]
    public class MoviesController : BaseApiController
    {
        private readonly DataContext _context;

        public MoviesController(DataContext dataContext)
        {
            _context = dataContext;
        }
        
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
          [HttpGet("{id}", Name = "Movie")]
          public async Task<ActionResult<AppMovie>> GetMovie(int id)
          {
              var moveis = await _context.Movies.FindAsync(id);
              return moveis;
          }

        //update movie
        [HttpPut("{id:int}", Name = "UpdateMovie")]
        public async Task<ActionResult<AppMovie>> UpdateMovie(int id, AppMovie movie)
        {
            try
            {
                if (id <= 0 ||  id > _context.Movies.ToList().Count() ||
                 id != movie.Id )
                    return BadRequest("Movie ID mismatch");


                var movieToUpdate = await _context.Movies.FindAsync(id);

                if (movieToUpdate == null)
                    return NotFound($"Movie with Id = {id} not found");

                return movieToUpdate;// _context.Movies.Update(movieToUpdate);
                //Movies.Update

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
            }
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