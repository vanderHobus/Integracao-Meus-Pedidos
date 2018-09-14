using System;
using System.Web.Script.Serialization;

namespace IntegracaoMeusPedidos.Entities
{
    public class SegmentoMP : IntegracaoMPType<SegmentoMP>
    {
        public int id;
        public string nome;
        public DateTime ultima_alteracao;
        public bool excluido;

        internal override string Path() => "segmentos";

        public SegmentoMP()
        {

        }

        public SegmentoMP(int id, string nome, bool excluido)
        {
            this.excluido = excluido;
            this.nome = nome;
            this.id = id;
        }

        public SegmentoMP(int id, string nome, DateTime ultima_alteracao, bool excluido)
        {
            this.id = id;
            this.nome = nome;
            this.ultima_alteracao = ultima_alteracao;
            this.excluido = excluido;
        }

        internal override SegmentoMP ConvertJson(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.ConvertToType<SegmentoMP>(obj);
        }
    }
}