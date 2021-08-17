namespace Compentio.Ferragosto.Notes
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetNotes();
        Task<Note> GetNote(Guid noteId);
        Task<Note> AddNote(Note note);
        Task<Note> UpdateNote(Note note);
        Task DeleteNote(Guid noteId);
    }

    public class NotesRepository : INotesRepository
    {       
        private readonly IMongoCollection<Note> _notes;

        public NotesRepository()
        {
            var mongoClient = new MongoClient("mongodb://admin:p%40ssw0rd@127.0.0.1:27017/NotesDatabase/?authSource=admin");           
            var database = mongoClient.GetDatabase("NotesDatabase");
            _notes = database.GetCollection<Note>("Notes");
        }

        public async Task<Note> AddNote(Note note)
        {
            await _notes.InsertOneAsync(note).ConfigureAwait(false);
            return note;
        }

        public async Task DeleteNote(Guid noteId)
        {
            await _notes.DeleteOneAsync(filter => filter.Id == noteId).ConfigureAwait(false);
        }

        public async Task<Note> GetNote(Guid noteId)
        {
            return await _notes.Find(note => note.Id == noteId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Note>> GetNotes() => await _notes.Find(note => true).ToListAsync();

        public async Task<Note> UpdateNote(Note note)
        {
            await _notes.ReplaceOneAsync(n => n.Id == note.Id, note);
            return await Task.FromResult(note);
        }
    }
}
