using IntegracaoMeusPedidos;
using IntegracaoMeusPedidos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace AppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegracaoMP mp = new IntegracaoMP("COMPANY_TOKEN", "APPLICATION_TOKEN", true);
            DateTime ultimaSincronizacao = DateTime.Now.AddDays(-10);
            List<SegmentoMP> segmentos = new List<SegmentoMP>();
            bool excluir = false;
            bool inserir = false;
            bool buscar = true;

            //var regPorId = mp.Get<SegmentoMP>(1330);

            if (excluir)
            {
                segmentos = mp.GetAll<SegmentoMP>(ultimaSincronizacao);

                foreach (var s in segmentos)
                {
                    s.excluido = true;
                    Console.WriteLine("Removendo registro [" + s.nome + "]");
                    mp.Update(s, s.id);
                }
            }

            if (inserir)
            {
                segmentos.Clear();

                for (int i = 0; i < 800; i++)
                {
                    segmentos.Add(new SegmentoMP(0, "Dimensão " + i, false));
                }

                foreach (var segmento in segmentos)
                {
                    Console.WriteLine("Inserindo registro [" + segmento.nome + "]");
                    SegmentoMP segmentos2 = mp.Insert(segmento);
                }
            }

            if (buscar)
            {
                segmentos.Clear();
                segmentos = mp.GetAll<SegmentoMP>(ultimaSincronizacao);

                foreach (var s in segmentos)
                {
                    SegmentoMP segmento = mp.Get<SegmentoMP>(s.id);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Console.WriteLine(DateTime.Now.ToLongTimeString() + " - " + segmento.nome.ToString());
                }
            }

            Console.ReadKey();
        }
    }
}