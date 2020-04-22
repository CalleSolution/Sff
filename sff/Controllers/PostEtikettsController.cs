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
    public class PostEtikettsController : ControllerBase
    {
        private readonly SffContext _context;

        public PostEtikettsController(SffContext context)
        {
            _context = context;
        }

        // GET: api/PostEtiketts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostEtikett>>> GetPostEtiketts()
        {
            return await _context.PostEtiketts.ToListAsync(); 
        }


        // GET: api/PostEtiketts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostEtikett>> GetPostEtikett(int id)
        {
            var postEtikettxml = await _context.PostEtiketts.FindAsync(id);

            if (postEtikettxml == null)
            {
                return NotFound();
            }

            var postToXml = $"<Etikettdata><Film>{postEtikettxml.Movie}</Film><Ort>{postEtikettxml.City}</Ort><Datum>{postEtikettxml.Date}</Datum></Etikettdata>";

            return  new ContentResult { Content = postToXml, ContentType = "application/xml" };
        }


        // PUT: api/PostEtiketts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostEtikett(int id, PostEtikett postEtikett)
        {
            if (id != postEtikett.Id)
            {
                return BadRequest();
            }

            _context.Entry(postEtikett).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostEtikettExists(id))
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

        // POST: api/PostEtiketts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        public async Task<ActionResult<PostEtikett>> PostPostEtikett(PostEtikett postEtikett)
        {
            _context.PostEtiketts.Add(postEtikett);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostEtikett", new { id = postEtikett.Id }, postEtikett);
        }

        // DELETE: api/PostEtiketts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostEtikett>> DeletePostEtikett(int id)
        {
            var postEtikett = await _context.PostEtiketts.FindAsync(id);
            if (postEtikett == null)
            {
                return NotFound();
            }

            _context.PostEtiketts.Remove(postEtikett);
            await _context.SaveChangesAsync();

            return postEtikett;
        }

        private bool PostEtikettExists(int id)
        {
            return _context.PostEtiketts.Any(e => e.Id == id);
        }
    }
}
