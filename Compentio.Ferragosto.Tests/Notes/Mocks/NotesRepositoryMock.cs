namespace Compentio.Ferragosto.Tests.Notes.Mocks
{
    using Moq;
    using Compentio.Ferragosto.Notes;
    using System.Collections.Generic;

    public class NotesRepositoryMock : Mock<INotesRepository>
    {
        public NotesRepositoryMock MockGetNotes(IEnumerable<Note> notes)
        {
            Setup(service => service.GetNotes()).ReturnsAsync(notes);
            return this;
        }

        public NotesRepositoryMock MockGetNote(long noteId, Note note)
        {
            Setup(service => service.GetNote(It.Is<long>(i => i == noteId))).ReturnsAsync(note);
            return this;
        }
    }
}
