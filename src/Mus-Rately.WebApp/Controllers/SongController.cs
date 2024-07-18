using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mus_Rately.WebApp.Services.Interfaces;

namespace Mus_Rately.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;


        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpPost(Name = "CreateSong")]
        public IActionResult CreateSong()
        {
            _songService.AddSongAsync();
            
            return Ok();
        }

        [HttpGet(Name = "GetSongs")]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songService.GetAllSongsAsync();

            return Ok(songs);
        }
    }
}