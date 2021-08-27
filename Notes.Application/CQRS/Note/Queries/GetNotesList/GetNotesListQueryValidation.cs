using FluentValidation;
using System;

namespace Notes.Application.CQRS.Note.Queries.GetNotesList
{
    public class GetNotesListQueryValidation : AbstractValidator<GetNotesListQuery>
    {
        public GetNotesListQueryValidation()
        {
            RuleFor(getNotesListQuery => getNotesListQuery.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
