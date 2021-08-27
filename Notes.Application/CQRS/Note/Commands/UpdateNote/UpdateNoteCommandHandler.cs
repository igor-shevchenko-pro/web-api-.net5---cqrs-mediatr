﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.CQRS.Note.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDbContext _dbContext;

        public UpdateNoteCommandHandler(INotesDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes
                                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if(entity == null || entity.UserId != request.UserId)
                throw new NotFoundException(nameof(Note), request.Id);

            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
