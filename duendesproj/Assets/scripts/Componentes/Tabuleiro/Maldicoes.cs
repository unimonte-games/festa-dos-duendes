using UnityEngine;
using Gerenciadores;
using Identificadores;
using Componentes.Jogador;

namespace Componentes.Tabuleiro
{
    public class Maldicoes : MonoBehaviour
    {
        public static void Preso2Turnos()
        {
            //TODO: prender por 2 turnos
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
            Debug.Log("Perdeu 10 moedas");
        }

        public static void PerdeMoedasParaJogador()
        {
            //TODO: isso
            Debug.Log("");
        }

        public static void SemPegarObj()
        {
            GerenciadorPartida.InvAtual.itens.Add(Itens.NaoPegaObj);
            Debug.Log("Não pode pegar objetos por 1 turno");
        }
    }
}