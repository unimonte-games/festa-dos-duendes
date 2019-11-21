using UnityEngine;
using Gerenciadores;
using Identificadores;
using Componentes.Jogador;

namespace Componentes.Tabuleiro
{
    public class Bencaos : MonoBehaviour
    {
        public static void CarteiraNova()
        {
            Inventario inv = GerenciadorPartida.InvAtual;
            int rand = Random.Range(0, inv.powerUps.Count);
            
            inv.moedas += 10;
            GerenciadorPartida.descricaoCarta =
                "Que chique, hein? Uma carteira novinha em folha e, veja só, com dez moedas dentro";

            if (inv.powerUps.Count < 3)
            {
                inv.powerUps.Add((PowerUps)rand);
                GerenciadorPartida.descricaoCarta +=
                    "e um melhoramento!";
            }
        }

        public static void MoedaNoChapeu()
        {
            GerenciadorPartida.InvAtual.moedas += 10;

            GerenciadorPartida.descricaoCarta =
                    "Você tira seu chapéu para refrescar a cabeça e encontra dez moedas perdidas lá dentro. Não é ótimo?";
        }

        public static void Melhoramento()
        {
            Inventario inv = GerenciadorPartida.InvAtual;

            if (inv.powerUps.Count < 3)
            {
                int rand = Random.Range(0, inv.powerUps.Count);
                inv.powerUps.Add((PowerUps)rand);

                GerenciadorPartida.descricaoCarta =
                    "Você anda bem sortudo, hein? Aqui está: um melhoramento para dar um empurrãozinho na sua jornada.";
            }
            else
                GerenciadorPartida.descricaoCarta =
                    "Aqui está: um melhoramento só para você! O que? Está com os bolsos cheios? Mas que mal agradecido...";
        }

        public static void SorteGrande()
        {
            Inventario inv = GerenciadorPartida.InvAtual;

            if (inv.rodadasSemObj == 0)
            {
                int rand = Random.Range(0, inv.objetos.Count);
                inv.objetos.Add((Objetos)rand);
                GerenciadorPartida.descricaoCarta =
                    "Você vai caminhando no tabuleiro quando, de repente, tropeça em algo. Você acaba de ganhar um objeto! Parabéns!";
            }
            else
                GerenciadorPartida.descricaoCarta =
                    "Você caminha pelo tabuleiro e tropeça em um objeto! Mas você não enxerga e sai andando direto...";
        }

        public static void GincanaGratis()
        {
            Inventario inv = GerenciadorPartida.InvAtual;
            if (inv.powerUps.Count < 3)
            {
                inv.powerUps.Add(PowerUps.MiniJogoGratis);
                GerenciadorPartida.descricaoCarta =
                    "O Duende foi abençoado pela Fadinha Festeira com uma gincana grátis! Agradeça à Fadinha!";
            }
            else
                GerenciadorPartida.descricaoCarta =
                    "A Fadinha Festeira lhe entrega uma gincana grátis... mas opa, você não tem mais espaço.";
        }
    }
}