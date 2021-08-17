namespace Compentio.Ferragosto.Notes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public interface INotesService
    {
        Task<IEnumerable<Note>> GetNotes();
        Task<Note> GetNote(Guid noteId);
        Task<Note> AddNote(Note note);
        Task<Note> UpdateNote(Note note);
        Task DeleteNote(Guid noteId);
    }

    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly ILogger<NotesService> _logger;

        public NotesService(INotesRepository notesRepository, ILogger<NotesService> logger)
        {
            _notesRepository = notesRepository;
            _logger = logger;
        }

        public async Task<Note> AddNote(Note note)
        {
            _logger.LogInformation($"Add note: '{note}' requested.");

            return await _notesRepository.AddNote(note).ConfigureAwait(false);
        }

        public async Task DeleteNote(Guid noteId)
        {
            _logger.LogInformation($"Delete note with noteId: '{noteId}' requested.");

            await _notesRepository.DeleteNote(noteId).ConfigureAwait(false);
        }

        public async Task<Note> GetNote(Guid noteId)
        {
            _logger.LogInformation("Get note requested.");

            return await _notesRepository.GetNote(noteId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            _logger.LogInformation("Get notes requested.");

            return await _notesRepository.GetNotes().ConfigureAwait(false);
        }

        public async Task<Note> UpdateNote(Note note)
        {
            _logger.LogInformation($"Update note: '{note}' requested.");

            return await _notesRepository.UpdateNote(note).ConfigureAwait(false);
        }
    }
}
