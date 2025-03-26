using AutoFixture;
using LojaVirtual.ProdutoAPI.DTOs;

namespace LojaVirtual.ProdutoAPI.Test.Constructor
{
    class CategoriaDTOConstructor : BaseConstructor<CategoriaDTO>
    {
        public static CategoriaDTOConstructor Um() 
        {
            return new CategoriaDTOConstructor();
        }

        public CategoriaDTOConstructor CategoriaDTOConstructorPadrao()
        {
            _mock.SetupGet(item => item.Id).Returns(_fixture.Create<int>());
            _mock.SetupGet(item => item.Nome).Returns(_fixture.Create<string>());

            return this;
        }

        public CategoriaDTOConstructor ComId(int id)
        {
            _mock.SetupGet(item => item.Id).Returns(id);
            return this;
        }

        public CategoriaDTOConstructor ComNome(string Nome)
        {
            _mock.SetupGet(item => item.Nome).Returns(Nome);
            return this;
        }
    }
}
