using BasicCRUD.Domain.DTO;
using BasicCRUD.Domain.Interfaces.Services;
using BasicCRUD.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace BasicCRUD.Controllers;

[ApiController]
[Route("notes")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<BaseResult<NoteDto>>> CreateNote([FromBody] CreateNoteDto noteDto)
    {
        var result = await _noteService.CreateAsync(noteDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResult<NoteDto>>> GetNoteById(long id)
    {
        var result = await _noteService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<BaseResult<NoteDto>>> GetAll()
    {
        var result = await _noteService.GetAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<ActionResult<BaseResult<NoteDto>>> UpdateNote([FromBody] UpdateNoteDto noteDto)
    {
        var result = await _noteService.UpdateAsync(noteDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPost("delete")]
    public async Task<ActionResult<BaseResult<NoteDto>>> DeleteNote([FromBody] DeleteNoteDto noteDto)
    {
        var result = await _noteService.DeleteAsync(noteDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}