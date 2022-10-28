using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNotesApi.Models;
using WebNotesApi.Services;

namespace WebNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public NotesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }

            User? user = await FindUser();

            if (user == null)
            {
                return BadRequest();
            }

            var notes = _context.Users.Include(u => u.Notes).ToListAsync().Result.Find(x => x.Id == user.Id);
            return notes.Notes;
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }

            User? user = FindUser().Result;

            if (user == null)
            {
                return BadRequest();
            }

            var note = await _context.Notes.Include(u => u.Users).FirstOrDefaultAsync(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        //// PUT: api/Notes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, NoteModel model)
        {
            User? user = FindUser().Result;

            if (user == null)
            {
                return BadRequest();
            }

            var note = await _context.Notes.Include(u => u.Users).FirstOrDefaultAsync(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            note.CategoryId = model.CategoryId;
            note.Text = model.Text;
            note.NoteName = model.NoteName;
            note.UpdatedDate = DateTime.Now.ToString("dd.MM.yyyy");

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(NoteModel model)
        {
            if (_context.Notes == null)
            {
                return Problem("Entity set 'ApplicationContext.Notes'  is null.");
            }

            User? user = FindUser().Result;

            if (user == null)
            {
                return BadRequest();
            }

            var note = new Note()
            {
                NoteName = model.NoteName,
                Text = model.Text,
                CategoryId = model.CategoryId,
                CreatedDate = DateTime.Now.ToString("dd.MM.yyyy"),
                UpdatedDate = DateTime.Now.ToString("dd.MM.yyyy")
            };

            note.Users.Add(user);

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        private async Task<User?> FindUser()
        {
            int.TryParse(User.Identities.First().Claims.First().Value, out int id);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }

            User? user = FindUser().Result;

            if (user == null)
            {
                return BadRequest();
            }

            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return (_context.Notes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
