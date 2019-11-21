using UnityEngine;
using Gerenciadores;
using Identificadores;
using Componentes.Jogador;

namespace Componentes.Tabuleiro
{
    public class Bencaos : MonoBehaviour
    {
        public static void GanhaMoedaPowerUp()
        {
            Inventario inv = GerenciadorPartida.InvAtual;
            int rand = Random.Range(0, inv.powerUps.Count);
            
            inv.moedas += 10;
            Debug.Log("Ganhou 10 moedas");

            if (inv.powerUps.Count < 3)
            {
                inv.powerUps.Add((PowerUps)rand);
                Debug.Log("E 1 PowerUp");
            }
        }

        public static void GanhaMoeda()
        {
            GerenciadorPartida.InvAtual.moedas += 10;

            Debug.Log("Ganhou 10 moedas");
        }

        public static void GanhaPowerUp()
        {
            Inventario inv = GerenciadorPartida.InvAtual;

            if (inv.powerUps.Count < 3)
            {
                int rand = Random.Range(0, inv.powerUps.Count);
                inv.powerUps.Add((PowerUps)rand);

                Debug.Log("Ganhou 1 powerup");
            }
            else
                Debug.Log("Sem espaço para mais PowerUps");
        }

        public static void GanhaObjeto()
        {
            Inventario inv = GerenciadorPartida.InvAtual;

            if (inv.rodadasSemObj == 0)
            {
                int rand = Random.Range(0, inv.objetos.Count);
                inv.objetos.Add((Objetos)rand);
                Debug.Log("Ganhou 1 objeto");
            }
            else
                Debug.Log("Não pode pegar outro objeto");
        }

        public static void GanhaMinijogo()
        {
            Inventario inv = GerenciadorPartida.InvAtual;
            if (inv.powerUps.Count < 3)
            {
                inv.powerUps.Add(PowerUps.MiniJogoGratis);
                Debug.Log("Ganhou 1 Minijogo gratis");
            }
            else
                Debug.Log("Não pode pegar outro PowerUp");
        }
    }
}