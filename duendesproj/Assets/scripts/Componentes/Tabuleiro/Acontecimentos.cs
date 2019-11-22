using UnityEngine;
using Gerenciadores;
using Componentes.Jogador;

namespace Componentes.Tabuleiro
{
    public class Acontecimentos : MonoBehaviour
    {
        public static string descricao;

        public static void Redirecionamento()
        {
            GerenciadorPartida.descricaoCarta =
                "Durante as gincanas vocês ficaram bem tontos. Todos caminharão na direção oposta às que estavam caminhando anteriormente.";

            foreach (Transform jogador in GerenciadorPartida.OrdemJogadores)
            {
                Movimentacao mov = jogador.GetComponent<Movimentacao>();
                mov.paraFrente = !mov.paraFrente;
            }
        }

        public static void PresenteInesperado()
        {
            GerenciadorPartida.descricaoCarta =
                "Pelos esforços de vocês, todos ganharão 1 melhoramento aleatório.";

            foreach (Transform jogador in GerenciadorPartida.OrdemJogadores)
            {
                Inventario inv = jogador.GetComponent<Inventario>();

                if (inv.powerUps.Count < 3)
                {
                    int qtd = System.Enum.GetValues(typeof(Identificadores.PowerUp)).Length; ;
                    int rand = Random.Range(0, qtd);

                    inv.powerUps.Add((Identificadores.PowerUp)rand);
                }
            }
        }

        public static void Recomecar()
        {
            GerenciadorPartida.descricaoCarta =
                "“E hoje sei sei sei” que todos voltarão para o início.";

            GerenciadorPartida gp = FindObjectOfType<GerenciadorPartida>();
            gp.StartCoroutine(gp.VoltaParaInicio());
        }

        public static void TeleportAleatorio()
        {
            GerenciadorPartida.descricaoCarta =
                "Parece que todos vão dar uma caminhadinha. Os duendes serão movidos para espaços aleatórios.";

            GerenciadorPartida gp = FindObjectOfType<GerenciadorPartida>();
            gp.StartCoroutine(gp.TeleportAleatorio());
        }
    }
}
