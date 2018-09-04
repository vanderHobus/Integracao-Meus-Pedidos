using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegracaoMeusPedidos.Entities
{
    public class ProdutoMP : IntegracaoMPType<ProdutoMP>
    {
        public int ID;
        public string Codigo;
        public string Nome;

        internal override string Path() { return "produtos"; }

        internal override ProdutoMP ConvertJson(dynamic obj)
        {
            ProdutoMP p = new ProdutoMP();
            p.ID = obj["id"];
            p.Codigo = obj["codigo"];
            p.Nome = obj["nome"];
            return p;
        }
    }
}
