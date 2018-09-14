using System.Web.Script.Serialization;

namespace IntegracaoMeusPedidos.Entities
{
    public class ProdutoMP : IntegracaoMPType<ProdutoMP>
    {
        public int ID;
        public string Codigo;
        public string Nome;

        internal override string Path() { return "produtos"; }

        internal override ProdutoMP ConvertJson(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.ConvertToType<ProdutoMP>(obj);
        }
    }
}