using UnityEngine;
using UnityEngine.UI;
using Gerenciadores;

namespace Telas
{
    public class TabuleiroHUD : MonoBehaviour
    {
        public RectTransform painelPrincipal;
        public GameObject painelPrefab;
        [HideInInspector]
        public Image[] fundos = new Image[GerenciadorGeral.qtdJogadores];

        private void Start()
        {
            if (fundos[0] == null)
            {
                for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
                {
                    GameObject painel = Instantiate(painelPrefab);
                    painel.transform.SetParent(painelPrincipal);

                    fundos[i] = painel.GetComponentInChildren<Image>();
                    fundos[i].color = Color.gray;
                }
            }
        }
    }
}
