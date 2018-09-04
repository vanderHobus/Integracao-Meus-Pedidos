using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegracaoMeusPedidos.Entities
{
    public abstract class IntegracaoMPType<T>
    {
        abstract internal string Path();
        abstract internal T ConvertJson(dynamic obj);
    }
}
