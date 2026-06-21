using Moq;
using Parcial2.Repositories;
using Parcial2.Services;
using Parcial2.ViewModels;
using Xunit;

namespace Parcial2.Tests
{
    public class AddGameViewModelTests
    {
        [Fact]
        public async Task AddGame_SinNombre_MuestraError()
        {
            var apiMock =
                new Mock<IApiService>();

            var repoMock =
                new Mock<IGameRepository>();

            var vm =
                new AddGameViewModel(
                    apiMock.Object,
                    repoMock.Object);

            vm.Name = "";

            await vm.AddGame();

            Assert.Equal(
                "Debe ingresar un nombre",
                vm.Status);
        }
    }
}