using IntegracaoMeusPedidos;
using IntegracaoMeusPedidos.Entities;
using System;
using System.Collections.Generic;

namespace AppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegracaoMP mp = new IntegracaoMP("COMPANY_TOKEN", "APPLICATION_TOKEN", true);
            DateTime ultimaSincronizacao = DateTime.Now.AddDays(-10);

            var regPorId = mp.Get<SegmentoMP>(1330);

            /*List<SegmentoMP> segmentos = mp.GetAll<SegmentoMP>(ultimaSincronizacao);

            foreach (var s in segmentos)
            {
                s.excluido = true;
                Console.WriteLine("Removendo registro [" + s.nome + "]");
                var xxxxxxxxzx = mp.Update<SegmentoMP>(s, s.id);
            }

            //List<ProdutoMP> produtos = mp.GetAll<ProdutoMP>(ultimaSincronizacao);

            //foreach (var p in produtos)
            //{
            //    // TODO: produtos
            //}

            segmentos.Clear();
            
            for (int i = 0; i < 10; i++)

            {
                segmentos.Add(new SegmentoMP(0, "Teste " + i, false));
            }

            foreach (var segmento in segmentos)
            {
                Console.WriteLine("Inserindo registro [" + segmento.nome + "]");
                var segmentos2 = mp.Insert<SegmentoMP>(segmento);
            }
            Console.ReadKey();*/
        }
    }
}