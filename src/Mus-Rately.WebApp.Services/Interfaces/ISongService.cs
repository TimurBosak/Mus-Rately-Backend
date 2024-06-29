using Mus_Rately.WebApp.Domain.Models;

namespace Mus_Rately.WebApp.Services.Interfaces
{
    public interface ISongService
    {
        public void AddSongAsync();

        public Task<IReadOnlyCollection<Song>> GetAllSongsAsync();
    }
}
