using System;

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

        internal override SegmentoMP ConvertJson(dynamic obj)
        {
            // TODO: Automatizar o cast do Obj com a Entity da classe. (Foreach nas properties)
            SegmentoMP s = new SegmentoMP();
            s.id = obj["id"];
            s.nome = obj["nome"];
            s.excluido = obj["excluido"];
            s.ultima_alteracao = DateTime.Parse(obj["ultima_alteracao"]);
            return s;
        }
    }
}