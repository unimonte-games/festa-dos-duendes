using UnityEngine;
using Gerenciadores;
using Componentes.Jogador;

namespace Componentes.Tabuleiro
{
    public class Maldicoes : MonoBehaviour
    {
        public static void Preso2Turnos()
        {
            GerenciadorPartida.InvAtual.rodadasPreso = 3;
            Debug.Log("Ficou preso por 2 turnos");
        }

        public static void PerdePowerUps()
        {
            GerenciadorPartida.InvAtual.powerUps.Clear();
            Debug.Log("Perdeu todos os PowerUps");
        }

        public static void PerdeObjeto()
        {
            Inventario inv = GerenciadorPartida.InvAtual;
            int rand = Random.Range(0, inv.objetos.Count);

            inv.objetos.RemoveAt(rand);
            Debug.Log("Perdeu 1 objeto aleatório");
        }

        public static void PerdeMoedas()
        {
            GerenciadorPartida.InvAtual.moedas -= 10;
            GerenciadorPartida.InvAtual.moedas %= 1;
            Debug.Log("Perdeu 10 moedas");
        }

        //public static void PerdeMoedasParaJogador()
        //{
        //    Inventario jogador1, jogador2;
        //    jogador1 = GerenciadorPartida.InvAtual;

        //    do
        //    {
        //        int rand = Random.Range(0, GerenciadorGeral.qtdJogadores);
        //    } while (jogador1 != jogador2);
        //    Debug.Log("");
        //}

        public static void SemPegarObj()
        {
            GerenciadorPartida.InvAtual.rodadasSemObj = 2;
            Debug.Log("Não pode pegar objetos por 1 rodada");
        }
    }
}