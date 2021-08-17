namespace Compentio.Ferragosto.Notes
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetNotes();
        Task<Note> GetNote(string noteId);
        Task<Note> AddNote(Note note);
        Task<Note> UpdateNote(Note note);
        Task DeleteNote(string noteId);
    }

    public class NotesRepository : INotesRepository
    {       
        private readonly IMongoCollection<Note> _notes;

        public NotesRepository(IMongoDbOptions options)
        {
            var mongoClient = new MongoClient(options.ConnectionString);           
            var database = mongoClient.GetDatabase(options.DatabaseName);
            _notes = database.GetCollection<Note>("Notes");
        }

        public async Task<Note> AddNote(Note note)
        {
            await _notes.InsertOneAsync(note).ConfigureAwait(false);
            return note;
        }

        public async Task DeleteNote(string noteId)
        {
            await _notes.DeleteOneAsync(filter => filter.Id == noteId).ConfigureAwait(false);
        }

        public async Task<Note> GetNote(string noteId)
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
