using BasicCRUD.Domain.DTO;
using BasicCRUD.Domain.Models;
using BasicCRUD.Domain.Result;

namespace BasicCRUD.Domain.Interfaces.Services;

public interface INoteService
{
    Task<CollectionResult<NoteDto>> GetAsync();
    Task<BaseResult<NoteDto>> GetByIdAsync(long id);
    Task<BaseResult<NoteDto>> CreateAsync(CreateNoteDto noteDto);
    Task<BaseResult<NoteDto>> UpdateAsync(UpdateNoteDto note);
    Task<BaseResult<NoteDto>> DeleteAsync(DeleteNoteDto note);
}