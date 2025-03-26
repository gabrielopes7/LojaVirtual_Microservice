using AutoFixture;
using LojaVirtual.ProdutoAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.ProdutoAPI.Test.Constructor
{
    public class ProdutoDTOConstructor : BaseConstructor<ProdutoDTO>
    {
        public static ProdutoDTOConstructor Um() 
        {
            return new ProdutoDTOConstructor();
        }

        public ProdutoDTOConstructor ProdutoDTOConstructorPadrao()
        {
            _mock.SetupGet(item => item.Id).Returns(_fixture.Create<int>());
            _mock.SetupGet(item => item.Nome).Returns(_fixture.Create<string>());
            _mock.SetupGet(item => item.Preco).Returns(_fixture.Create<decimal>());
            _mock.SetupGet(item => item.Descricao).Returns(_fixture.Create<string>());
            _mock.SetupGet(item => item.ImagemURL).Returns(_fixture.Create<string>());
            _mock.SetupGet(item => item.CategoriaId).Returns(_fixture.Create<int>());
            
            return this;        
        }

        public ProdutoDTOConstructor ComId(int id)
        {
            _mock.SetupGet(item => item.Id).Returns(id);
            return this;
        }
        public ProdutoDTOConstructor ComNome(string nome)
        {
            _mock.SetupGet(item => item.Nome).Returns(nome);
            return this;
        }

        public ProdutoDTOConstructor ComPreco(decimal preco)
        {
            _mock.SetupGet(item => item.Preco).Returns(preco);
            return this;
        }
        public ProdutoDTOConstructor ComDescricao(string descricao)
        {
            _mock.SetupGet(item => item.Descricao).Returns(descricao);
            return this;
        }
        public ProdutoDTOConstructor ComImagemURL(string imagemUrl)
        {
            _mock.SetupGet(item => item.ImagemURL).Returns(imagemUrl);
            return this;
        }
        public ProdutoDTOConstructor ComCategoriaId(int categoriaId)
        {
            _mock.SetupGet(item => item.CategoriaId).Returns(categoriaId);
            return this;
        }

    }
}
