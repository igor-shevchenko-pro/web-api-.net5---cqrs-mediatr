using AutoMapper;
using Notes.Application.Common.Mappings;
using System;

namespace Notes.Application.CQRS.Note.Queries.GetNotesList
{
    public class NotesLookUpDto : IMapWith<Domain.Entities.Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Note, NotesLookUpDto>()
                   .ForMember(noteVm => noteVm.Title, opt => opt.MapFrom(note => note.Title))
                   .ForMember(noteVm => noteVm.Id, opt => opt.MapFrom(note => note.Id));
        }
    }
}
