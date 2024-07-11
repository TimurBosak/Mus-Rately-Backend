namespace Mus_Rately.WebApp.Services.Interfaces
{
    public interface ILoginService
    {
        public Task LoginAsync();

        public Task LogoutAsync();
    }
}
