using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNotesApi.Models.NoteModels;
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

            int userId = await FindUser();

            

            //var notes = _context.NoteUsers.Include(u => u.Notes).ToListAsync().Result.Find(x => x.Id == noteUser.Id);
            var notes = await _context.Notes.Where(x => x.UserId == userId).ToListAsync();
            return notes;
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }

            int userId = await FindUser();

            //var note = await _context.Notes.Include(u => u.Users).FirstOrDefaultAsync(n => n.Id == id);
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == userId);
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
            int userId = await FindUser();

            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == userId);
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

            int userId = await FindUser();

            
            

            var note = new Note
            {
                NoteName = model.NoteName,
                Text = model.Text,
                CategoryId = model.CategoryId,
                CreatedDate = DateTime.Now.ToString("dd.MM.yyyy"),
                UpdatedDate = DateTime.Now.ToString("dd.MM.yyyy"),
                UserId = userId
            };
            _context.Notes.Add(note);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        private Task<int> FindUser()
        {
            int.TryParse(User.Identities.First().Claims.First().Value, out int id);

            
            return Task.FromResult(id);
        }

        //private async Task<NoteUser?> FindNoteUser(int userId)
        //{
        //    return await _context.NoteUsers.FirstOrDefaultAsync(u => u.UserId == userId);
        //}

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }
            int userId = await FindUser();

            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == userId);
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
