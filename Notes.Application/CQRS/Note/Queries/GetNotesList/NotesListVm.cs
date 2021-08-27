using Notes.Application.Common.Mappings;
using System.Collections.Generic;

namespace Notes.Application.CQRS.Note.Queries.GetNotesList
{
    public class NotesListVm : IMapWith<Domain.Entities.Note>
    {
        public IList<NotesLookUpDto> Notes { get; set; }
    }
}
