using AutoMapper;
using Notes.Application.CQRS.Note.Queries.GetNotesList;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Note.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteListQueryHandlerTests
    {
        private readonly NotesDbContext Context;
        private readonly IMapper Mapper;

        public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetNotesListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetNotesListQuery
                {
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NotesListVm>();
            result.Notes.Count.ShouldBe(2);
        }
    }
}
