using AutoFixture;
using LojaVirtual.ProdutoAPI.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.ProdutoAPI.Test.Constructor
{
    public class BaseConstructor<T> where T : class
    {
        protected readonly Fixture _fixture;
        protected readonly Mock<T> _mock;

        protected BaseConstructor()
        {
            _mock = new Mock<T>();
            _fixture = new Fixture();
        }

        public T Construir()
        {
            return _mock.Object;
        }

        public Mock<T> ObterMock()
        {
            return _mock;
        }
    }
}
