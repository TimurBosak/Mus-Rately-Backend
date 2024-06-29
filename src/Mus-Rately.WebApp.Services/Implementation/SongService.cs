using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.Repositories.Interfaces;
using Mus_Rately.WebApp.Services.Interfaces;

namespace Mus_Rately.WebApp.Services.Implementation
{
    public class SongService : ISongService
    {
        private readonly IMusRatelyUnitOfWork _uow;


        public SongService(IMusRatelyUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddSongAsync()
        {
            var songRepo = _uow.GetRepository<Song>();

            var name = "Test";
            var song = new Song
            {
                Name = name
            };

            songRepo.Add(song);
            _uow.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Song>> GetAllSongsAsync()
        {
            var songRepo = _uow.GetRepository<Song>();

            var songs = await songRepo.GetAllAsync();

            return songs;
        }
    }
}
