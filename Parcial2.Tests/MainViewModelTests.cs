using Moq;
using Parcial2.Model;
using Parcial2.Repositories;
using Parcial2.Services;
using Parcial2.ViewModels;
using Xunit;

namespace Parcial2.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public async Task FilterGames_EncuentraJuego()
        {
            var games =
                new List<Game>
                {
                    new Game
                    {
                        Name = "Mirrors Edge"
                    }
                };

            var repoMock =
                new Mock<IGameRepository>();

            repoMock
                .Setup(x =>
                    x.SearchAsync("Mirror"))
                .ReturnsAsync(games);

            var apiMock =
                new Mock<IApiService>();

            var vm =
                new MainViewModel(
                    apiMock.Object,
                    repoMock.Object);

            vm.SearchText = "Mirror";

            await vm.FilterGames();

            Assert.Single(vm.Games);

            Assert.Equal(
                "Mirrors Edge",
                vm.Games[0].Name);
        }
    }
}