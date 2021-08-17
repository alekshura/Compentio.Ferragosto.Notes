namespace Compentio.Ferragosto.Tests.Notes
{
    using Compentio.Ferragosto.Tests.Core;
    using System.Threading.Tasks;
    using Xunit;
    using FluentAssertions;
    using Compentio.Ferragosto.Notes;
    using Moq;
    using System.Net.Http.Json;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Compentio.Ferragosto.Tests.Notes.Mocks;
    using System;

    public class NotesApiIntegrationTests : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly WebApplicationFactory<Api.Startup> _factory;
        private readonly NotesServiceMock _notesServiceMock;

        private const string notesBaseUrl = "api/notes";

        public NotesApiIntegrationTests(WebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
            _notesServiceMock = new();
        }


        [Fact]
        public async Task ShouldReturnListOfNotes()
        {
            // Arrange
            var httpClient = _factory.WithAuthentication()
                      .WithService(_ => _notesServiceMock.Object)
                      .CreateAndConfigureClient();

            var mockedNotes = NotesMocks.Notes;
            _notesServiceMock.MockGetNotes(mockedNotes);

            // Act
            var response = await httpClient.GetAsync(notesBaseUrl).ConfigureAwait(false);
            var notes = await response.Content.ReadFromJsonAsync<IEnumerable<Note>>().ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().ContainAll("application/json; charset=utf-8");
            notes.Should().BeEquivalentTo(mockedNotes);            
        }

        [Fact]
        public async Task ShouldBeUnauthorized()
        {
            // Arrange
            var httpClient = _factory.WithService(_ => _notesServiceMock.Object)
                      .CreateAndConfigureClient();


            // Act
            var response = await httpClient.GetAsync(notesBaseUrl).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            _notesServiceMock.Verify(notes => notes.GetNotes(), Times.Never());
        }

        [Fact]
        public async Task ShouldBeForbidden()
        {
            // Arrange
            var httpClient = _factory.WithAuthenticationWithoutClaims()
                      .WithService(_ => _notesServiceMock.Object)
                      .CreateAndConfigureClient();


            // Act
            Func<Task> getNotesTask = async () => { _ = await httpClient.GetAsync(notesBaseUrl).ConfigureAwait(false); };

            // Assert
            await getNotesTask.Should().ThrowAsync<System.Net.Http.HttpRequestException>();

            _notesServiceMock.Verify(notes => notes.GetNotes(), Times.Never());
        }
    }
}
