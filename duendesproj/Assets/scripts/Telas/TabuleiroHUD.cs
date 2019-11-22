using UnityEngine;
using UnityEngine.UI;
using Gerenciadores;
using Componentes.Jogador;
using UnityEngine.U2D;

namespace Componentes.Tabuleiro
{
    public class TabuleiroHUD : MonoBehaviour
    {
        public Sprite[] spritesCabecas;
        public RectTransform painelPrincipal;
        public GameObject painelPrefab;
        public static RectTransform[] Paineis = new RectTransform[GerenciadorGeral.qtdJogadores];

        private void Start()
        {
            if (Paineis[0] == null)
            {
                for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
                {
                    GameObject painel = Instantiate(painelPrefab);
                    painel.transform.SetParent(painelPrincipal);
                    Paineis[i] = painel.GetComponent<RectTransform>();

                    AlteraFundo(Color.gray);

                    Transform rosto = Paineis[i].transform.Find("Mascara").Find("Img Rosto");
                    rosto.GetComponent<Image>().sprite = spritesCabecas[i];
                }
            }
        }

        public static void AlteraMoeda(int qtd, bool substitui = false, Inventario inv = null)
        {
            if (inv == null) inv = GerenciadorPartida.InvAtual;

            inv.moedas = substitui ? qtd : qtd + inv.moedas;
            if (inv.moedas < 0) inv.moedas = 0;

            int i = GerenciadorPartida.Turno;
            Transform moedas = Paineis[i].transform.Find("Painel Moedas");
            moedas.GetComponentInChildren<Text>().text = inv.moedas + " moedas";
        }

        public static void AlteraFundo(Color cor)
        {
            int i = GerenciadorPartida.Turno;
            Transform fundo = Paineis[i].transform.Find("Fundo Jogador");
            fundo.GetComponent<Image>().color = cor;
        }

        public static void AlteraPowerUp(int qtd, bool estado, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;

            Transform painel = Paineis[i].transform.Find("Painel PowerUps");

            for (int j = 0; j < qtd; j++)
            {
                Image fundo = painel.transform.GetChild(j).GetComponent<Image>();
                fundo.color = estado ? Color.green : Color.black;
            }
        }
    }
}
