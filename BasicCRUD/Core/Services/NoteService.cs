using BasicCRUD.Domain.DTO;
using BasicCRUD.Domain.Enums;
using BasicCRUD.Domain.Interfaces;
using BasicCRUD.Domain.Interfaces.Services;
using BasicCRUD.Domain.Models;
using BasicCRUD.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace BasicCRUD.Core.Services;

public class NoteService : INoteService
{
    private readonly IBaseRepository<Note> _repository;

    public NoteService(IBaseRepository<Note> repository)
    {
        _repository = repository;
    }

    public async Task<CollectionResult<NoteDto>> GetAsync()
    {
        NoteDto[] result;
        try
        {
            result = await _repository
                .GetAll()
                .Select(n => new NoteDto(n.Id, n.Name, n.Description, n.CreatedAt.ToString()))
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            return new CollectionResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message
            };
        }

        if (result.Length == 0)
        {
            return new CollectionResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.NoteNotFound,
                ErrorMessage = "Notes are not found"
            };
        }

        return new CollectionResult<NoteDto>
        {
            Data = result,
            Count = result.Length
        };
    }

    public Task<BaseResult<NoteDto>> GetByIdAsync(long id)
    {
        NoteDto? result;
        try
        {
            result = _repository
                .GetAll()
                .AsEnumerable()
                .Select(n => new NoteDto(n.Id, n.Name, n.Description, n.CreatedAt.ToString()))
                .FirstOrDefault(n => n.Id == id);
        }
        catch (Exception ex)
        {
            return Task.FromResult(new BaseResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message
            });
        }

        if (result == null)
        {
            return Task.FromResult(new BaseResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.NoteNotFound,
                ErrorMessage = "Note is not found"
            });
        }

        return Task.FromResult(new BaseResult<NoteDto>
        {
            Data = result
        });
    }

    public async Task<BaseResult<NoteDto>> CreateAsync(CreateNoteDto noteDto)
    {
        try
        {
            if (noteDto == null)
            {
                return new BaseResult<NoteDto>
                {
                    ErrorCode = (int)ErrorCodes.NullNoteEntity,
                    ErrorMessage = "Note entity is null"
                };
            }

            var newNote = new Note()
            {
                Name = noteDto.Name,
                Description = noteDto.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(newNote);

            return new BaseResult<NoteDto>
            {
                Data = new NoteDto(newNote.Id, newNote.Name, newNote.Description, newNote.CreatedAt.ToString())
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<BaseResult<NoteDto>> UpdateAsync(UpdateNoteDto noteDto)
    {
        if (noteDto == null)
        {
            return new BaseResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.NullNoteEntity,
                ErrorMessage = "Note entity is null"
            };
        }
        
        try
        {
            var note = await _repository
                .GetAll()
                .FirstOrDefaultAsync(n => n.Id == noteDto.Id);

            if (note == null)
            {
                return new BaseResult<NoteDto>
                {
                    ErrorCode = (int)ErrorCodes.NoteNotFound,
                    ErrorMessage = "Note is not found"
                };
            }

            note.Name = noteDto.Name;
            note.Description = noteDto.Description;

            await _repository.UpdateAsync(note);

            return new BaseResult<NoteDto>
            {
                Data = new NoteDto(note.Id, note.Name, note.Description, note.CreatedAt.ToString())
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<BaseResult<NoteDto>> DeleteAsync(DeleteNoteDto noteDto)
    {
        if (noteDto == null)
        {
            return new BaseResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.NullNoteEntity,
                ErrorMessage = "Note entity is null"
            };
        }
        
        try
        {
            var note = await _repository
                .GetAll()
                .FirstOrDefaultAsync(n => n.Id == noteDto.Id);

            if (note == null)
            {
                return new BaseResult<NoteDto>
                {
                    ErrorCode = (int)ErrorCodes.NoteNotFound,
                    ErrorMessage = "Note is not found"
                };
            }
            
            await _repository.DeleteAsync(note);

            return new BaseResult<NoteDto>
            {
                Data = new NoteDto(note.Id, note.Name, note.Description, note.CreatedAt.ToString())
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<NoteDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }
}