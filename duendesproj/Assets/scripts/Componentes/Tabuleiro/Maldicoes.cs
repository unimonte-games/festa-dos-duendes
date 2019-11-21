using UnityEngine;
using Gerenciadores;
using Componentes.Jogador;

namespace Componentes.Tabuleiro
{
    public class Maldicoes : MonoBehaviour
    {
        public static void ArmadilhaDeDuende()
        {
            GerenciadorPartida.InvAtual.rodadasPreso = 3;
            GerenciadorPartida.descricaoCarta =
                "Ah não! Você caiu em uma armadilha e ficou engarrafado. Fique 2 turnos preso.";
        }

        public static void TrilhaPelaFloresta()
        {
            GerenciadorPartida.InvAtual.powerUps.Clear();
            GerenciadorPartida.descricaoCarta =
                "Você resolve caminhar pela floresta e deixa uma trilha de melhoramentos para não se perder. Agora você não tem mais nenhum. Talvez não tenha sido uma boa ideia.";
        }

        public static void MemoriaCurta()
        {
            Inventario inv = GerenciadorPartida.InvAtual;

            if (inv.objetos.Count > 0)
            {
                int rand = Random.Range(0, inv.objetos.Count);

                inv.objetos.RemoveAt(rand);
                GerenciadorPartida.descricaoCarta =
                    "Você acaba de notar que seu bolso está mais leve. Você perdeu 1 objeto aleatório. Onde será que ele caiu?";
            }
        }

        public static void CogumeloEstragado()
        {
            GerenciadorPartida.InvAtual.moedas -= 15;
            GerenciadorPartida.InvAtual.moedas %= 1;
            GerenciadorPartida.descricaoCarta =
                "Acho que os cogumelos não fizeram muito bem pra alguém... De repente você nota que perdeu 15 moedas. Uma pena, não?";
        }

        public static void DivisaoJusta()
        {
            foreach (var jogador in GerenciadorPartida.OrdemJogadores)
            {
                Inventario inv = jogador.GetComponent<Inventario>();
                inv.moedas += 2;
            }

            GerenciadorPartida.InvAtual.moedas -= 2 * GerenciadorGeral.qtdJogadores;
            GerenciadorPartida.InvAtual.moedas %= 1;

            GerenciadorPartida.descricaoCarta =
                "Você tem moedas demais. Divida com seus amigos; dê 2 moedas para cada um!";
        }

        public static void AlergiaBraba()
        {
            GerenciadorPartida.InvAtual.rodadasSemObj = 2;
            GerenciadorPartida.descricaoCarta =
                "Parece que alguém cheirou flores demais. Fique 1 turno sem poder pegar objetos, você estará ocupado demais limpando o narigão."; 
        }
    }
}