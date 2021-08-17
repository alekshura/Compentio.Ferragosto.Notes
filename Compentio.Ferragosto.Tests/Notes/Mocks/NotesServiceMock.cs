namespace Compentio.Ferragosto.Tests.Notes.Mocks
{
    using Moq;
    using Compentio.Ferragosto.Notes;
    using System.Collections.Generic;
    using System;

    public class NotesServiceMock : Mock<INotesService>
    {
        public NotesServiceMock MockGetNotes(IEnumerable<Note> notes)
        {
            Setup(service => service.GetNotes()).ReturnsAsync(notes);
            return this;
        }

        public NotesServiceMock MockGetNote(Guid noteId, Note note)
        {
            Setup(service => service.GetNote(It.Is<Guid>(i => i == noteId))).ReturnsAsync(note);
            return this;
        }

        public NotesServiceMock MockAddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            Setup(service => service.AddNote(note)).ReturnsAsync(note);
            return this;
        }

        public NotesServiceMock MockModifyNote(Note note)
        {
            Setup(service => service.GetNote(It.Is<Guid>(i => i == note.Id))).ReturnsAsync(note);
            return this;
        }
    }
}
