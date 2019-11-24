using UnityEngine;
using UnityEngine.UI;
using Gerenciadores;

namespace Componentes.Tabuleiro
{
    public class TabuleiroHUD : MonoBehaviour
    {
        public Sprite[] spritesCabecas;
        public Transform painelPrincipal;
        public GameObject painelPrefab;
        public static Transform PnlDescricoes;
        public static Transform[] Paineis = new Transform[GerenciadorGeral.qtdJogadores];

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

            PnlDescricoes = transform.Find("Painel Descricoes");
        }

        public static void AlteraFundo(Color cor)
        {
            int i = GerenciadorPartida.Turno;
            Transform fundo = Paineis[i].transform.Find("Fundo Jogador");
            fundo.GetComponent<Image>().color = cor;
        }
    }
}
