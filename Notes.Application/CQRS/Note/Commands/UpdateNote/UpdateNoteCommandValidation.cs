using FluentValidation;
using System;

namespace Notes.Application.CQRS.Note.Commands.UpdateNote
{
    public class UpdateNoteCommandValidation : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidation()
        {
            RuleFor(updateNoteCommand => updateNoteCommand.Id)
                .NotEqual(Guid.Empty);

            RuleFor(updateNoteCommand => updateNoteCommand.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(updateNoteCommand => updateNoteCommand.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
