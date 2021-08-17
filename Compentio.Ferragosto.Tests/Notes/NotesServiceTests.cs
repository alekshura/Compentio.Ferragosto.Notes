namespace Compentio.Ferragosto.Tests.Notes
{
    using System;
    using System.Threading.Tasks;
    using Xunit;
    using FluentAssertions;
    using Compentio.Ferragosto.Notes;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Compentio.Ferragosto.Tests.Notes.Mocks;

    public class NotesServiceTests
    {
        private readonly NotesRepositoryMock _notesRepositoryMock;
        private readonly Mock<ILogger<NotesService>> _loggerMock;

        private readonly NotesService _notesService;

        public NotesServiceTests()
        {
            _notesRepositoryMock = new();
            _loggerMock = new();
            _notesService = new NotesService(_notesRepositoryMock.Object, _loggerMock.Object);
        }


        [Fact]
        public async Task ShouldReturnListOfNotes()
        {
            // Arrange
            var mockedNotes = NotesMocks.Notes;
            _notesRepositoryMock.MockGetNotes(mockedNotes);

            // Act            
            var notes = await _notesService.GetNotes().ConfigureAwait(false);

            // Assert
            notes.Should().BeEquivalentTo(mockedNotes);
        }
    }
}
