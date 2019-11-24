using UnityEngine;
using UnityEngine.UI;
using Gerenciadores;

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

        public static void AlteraFundo(Color cor)
        {
            int i = GerenciadorPartida.Turno;
            Transform fundo = Paineis[i].transform.Find("Fundo Jogador");
            fundo.GetComponent<Image>().color = cor;
        }
    }
}
