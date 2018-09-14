using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegracaoMeusPedidos.Entities
{
    public class ThrottlingMP
    {
        public int tempo_ate_permitir_novamente { get; set; }
        public int limite_de_requisicoes { get; set; }
    }
}