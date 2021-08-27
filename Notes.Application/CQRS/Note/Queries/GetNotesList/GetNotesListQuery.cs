using MediatR;
using System;

namespace Notes.Application.CQRS.Note.Queries.GetNotesList
{
    public class GetNotesListQuery : IRequest<NotesListVm>
    {
        public Guid UserId { get; set; }
    }
}
