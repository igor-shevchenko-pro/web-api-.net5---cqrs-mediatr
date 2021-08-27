using FluentValidation;
using System;

namespace Notes.Application.CQRS.Note.Commands.DeleteNote
{
    public class DeleteNoteCommandValidation : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidation()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Id)
                .NotEqual(Guid.Empty);

            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
