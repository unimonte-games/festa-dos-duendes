using System.Reflection;
using UnityEngine;
using Componentes.Jogador;
using Gerenciadores;
using Identificadores;

namespace Componentes.Tabuleiro
{
    public class EventosCasa : MonoBehaviour
    {
        public CenaID minijogo;

        public void ativarCasa()
        {
            TiposCasa evento = GetComponent<CasaBase>().tipoCasa;
            MethodInfo metodo = GetType().GetMethod(evento.ToString());

            metodo.Invoke(this, null);

            GerenciadorPartida gp = FindObjectOfType<GerenciadorPartida>();
            gp.StartCoroutine(gp.WaitNovaRodada(2.5f));
        }

        public void Garrafa()
        {
            Inventario inv = GerenciadorPartida.InvAtual;
            inv.rodadasPreso += 2;
            inv.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void BemMal()
        {
            System.Type tipo;
            float rand = Random.value;

            if (rand < 0.5f)
                tipo = typeof(Bencaos);
            else
                tipo = typeof(Maldicoes);

            ExecMetodoRand(tipo);
        }

        public void PowerUp()
        {
            int qtd = System.Enum.GetNames(typeof(PowerUp)).Length;
            int rand = Random.Range(0, qtd);

            Inventario inv = GerenciadorPartida.InvAtual;

            if (inv.powerUps.Count < 3)
            {
                inv.AlteraPowerUp(1, true);
                inv.powerUps.Add((PowerUp)rand);
            }
        }

        public void Acontecimento()
        {
            ExecMetodoRand(typeof(Acontecimentos));
        }

        public void MiniJogo()
        {
            if (GerenciadorPartida.InvAtual.moedas >= 25)
                GerenciadorGeral.TransitarParaMJ(minijogo);
        }

        public void Moeda()
        {
            GerenciadorPartida.InvAtual.AlteraMoeda(+5);
        }

        private void ExecMetodoRand(System.Type tipo)
        {
            MethodInfo[] metodos;

            metodos = tipo.GetMethods(BindingFlags.DeclaredOnly |
                                      BindingFlags.Static |
                                      BindingFlags.Public);

            int rd = Random.Range(0, metodos.Length);

            MethodInfo metodoRand = metodos[rd];
            metodoRand.Invoke(this, null);
        }
    }
}