using FluentValidation;
using System;

namespace Notes.Application.CQRS.Note.Commands.CreateNote
{
    public class CreateNoteCommandValidation : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidation()
        {
            RuleFor(createNoteCommand => createNoteCommand.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(createNoteCommand => createNoteCommand.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
