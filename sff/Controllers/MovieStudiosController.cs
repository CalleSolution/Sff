using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sff.Models;

namespace sff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieStudiosController : ControllerBase
    {
        private readonly SffContext _context;
        Movie m = new Movie();

        public MovieStudiosController(SffContext context)
        {
            _context = context;
        }

        // GET: api/MovieStudios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieStudio>>> GetMovieStudios()
        {
            return await _context.MovieStudios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MovieStudio>>> GetMovieStudio(int id)
        {
            var movieStudio = await _context.MovieStudios.Where(x => x.StudioId == id).ToListAsync();

            if (movieStudio == null)
            {
                return NotFound();
            }

            return movieStudio;
        }

        // PUT: api/MovieStudios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieStudio(int id, MovieStudio movieStudio)
        {
            if (id != movieStudio.Id)
            {
                return BadRequest();
            }

            _context.Entry(movieStudio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieStudioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MovieStudios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MovieStudio>> PostMovieStudio(MovieStudio movieStudio)
        {
            var m = await _context.Movies.FindAsync(movieStudio.MovieId);
            if (m.RentLimit > 0)
            {
                m.RentMovie();
                _context.Entry(movieStudio).State = EntityState.Modified;

                _context.MovieStudios.Add(movieStudio);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMovieStudio), new { id = movieStudio.Id }, movieStudio);
            }
            else
                return null;
        }

        // DELETE: api/MovieStudios/
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieStudio>> DeleteMovieStudio(int id)
        {
            
            var movieStudio = await _context.MovieStudios.FindAsync(id);
            if (movieStudio == null)
            {
                return NotFound();
            }

            var m = await _context.Movies.FindAsync(movieStudio.MovieId);
            m.ReturnMovie();
            _context.Entry(movieStudio).State = EntityState.Modified;

            _context.MovieStudios.Remove(movieStudio);
            await _context.SaveChangesAsync();

            return movieStudio;
        }

        private bool MovieStudioExists(int id)
        {
            return _context.MovieStudios.Any(e => e.Id == id);
        }
    }
}
