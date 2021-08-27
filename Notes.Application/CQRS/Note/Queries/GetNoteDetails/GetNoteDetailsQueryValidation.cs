using FluentValidation;
using System;

namespace Notes.Application.CQRS.Note.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryValidation : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsQueryValidation()
        {
            RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.Id)
                .NotEqual(Guid.Empty);

            RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
