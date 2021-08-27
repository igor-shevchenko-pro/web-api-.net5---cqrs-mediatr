using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Interfaces
{
    public interface INotesDbContext
    {
        DbSet<Note> Notes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
