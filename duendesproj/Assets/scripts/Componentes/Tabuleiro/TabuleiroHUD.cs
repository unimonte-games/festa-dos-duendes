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
        public static Transform[] PnlsDescricoes = new Transform[4];
        public static Transform[] Paineis = new Transform[GerenciadorGeral.qtdJogadores];
        public static Color corOn = new Color(0.2039216f, 0.2f, 0.1843137f);
        public static Color corOff = new Color(0.8392158f, 0.7529413f, 0.5333334f);

        private void Awake()
        {
            if (Paineis[0] == null)
            {
                for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
                {
                    GameObject painel = Instantiate(painelPrefab);
                    painel.transform.SetParent(painelPrincipal);
                    Paineis[i] = painel.GetComponent<RectTransform>();

                    FundoJogador(Color.gray);

                    Transform rosto = Paineis[i].transform.Find("Mascara").Find("Img Rosto");
                    rosto.GetComponent<Image>().sprite = spritesCabecas[i];
                }
            }

            PnlDescricoes = transform.Find("Painel Descricoes");

            for (int i = 0; i < 4; i++)
            {
                PnlsDescricoes[i] = PnlDescricoes.GetChild(0);
                PnlsDescricoes[i].gameObject.SetActive(false);
            }
        }

        public static void FundoJogador(Color cor, int i = -1)
        {
            if (i == -1) i = GerenciadorPartida.Turno;
            Transform fundo = Paineis[i].transform.Find("Fundo Jogador");
            fundo.GetComponent<Image>().color = cor;
        }

        public static void FundoPowerUps(Color cor, int i, int turno = -1)
        {
            if (turno == -1) turno = GerenciadorPartida.Turno;
            Transform fundo = Paineis[turno].transform.Find("Painel PowerUps");
            fundo = fundo.GetChild(i);
            fundo.GetComponent<Image>().color = cor;
        }
    }
}
