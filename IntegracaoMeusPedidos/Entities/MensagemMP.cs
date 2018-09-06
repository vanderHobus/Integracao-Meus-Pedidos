using System.Collections.Generic;

namespace IntegracaoMeusPedidos.Entities
{
    public class MensagemMP
    {
        public string mensagem { get; set; }
        public IList<ErroMP> erros { get; set; }
    }

    public class ErroMP
    {
        public string mensagem { get; set; }
        public string campo { get; set; }

    }
}
