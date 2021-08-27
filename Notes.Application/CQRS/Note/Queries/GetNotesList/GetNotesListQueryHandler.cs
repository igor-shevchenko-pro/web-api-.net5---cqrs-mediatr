using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.CQRS.Note.Queries.GetNotesList
{
    public class GetNotesListQueryHandler : IRequestHandler<GetNotesListQuery, NotesListVm>
    {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNotesListQueryHandler(INotesDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);


        public async Task<NotesListVm> Handle(GetNotesListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _dbContext.Notes
                                             .Where(x => x.UserId == request.UserId)
                                             .ProjectTo<NotesLookUpDto>(_mapper.ConfigurationProvider)
                                             .ToListAsync(cancellationToken);

            return new NotesListVm { Notes = notesQuery };
        }
    }
}
