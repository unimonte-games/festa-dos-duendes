using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorGeral : MonoBehaviour
    {
        public static int qtdJogadores;
        public const  int PONTOS_POR_VENCEDOR_MJ = 10;
        public static int[] pontuacao = new int[4];

#region UNITY_EDITOR
        public bool usandoGGTest;
#endregion

        static GerenciadorGeral instancia;

        void Start()
        {
            // eu acho que poderia escrever = this; mas não tenho certeza
            instancia = GetComponent<GerenciadorGeral>();
            DontDestroyOnLoad(gameObject);
        }

#region (de)cadastramento de jogadores

        public static bool PodeCadastrar()   { return qtdJogadores < 4;  }
        public static bool PodeDecadastrar() { return qtdJogadores > 0; }

        public static void CadastrarJogador()
        {
            if (PodeCadastrar())
                qtdJogadores += 1;
        }

        public static void DecadastrarJogador()
        {
            if (PodeDecadastrar())
                qtdJogadores -= 1;
        }

#endregion (de)cadastramento de jogadores

#region gerenciamento de cenas

        public static void PontuarCampeaoMJ(JogadorID jogadorID)
        {
            pontuacao[(int)jogadorID] += PONTOS_POR_VENCEDOR_MJ;
            instancia._TransitarPara(CenaID.Tabuleiro);
        }

        public static void TransitarParaMJ(CenaID cenaId)
        {
            instancia._TransitarPara(cenaId);
        }

        void _TransitarPara(CenaID cenaId)
        {
            // Se estiver rodando no Editor, se rodamos o jogo no GGTest
            // e a cena pra carregar é o tabuleiro, ao invés de carregar
            // o tabuleiro, carregamos o GGTest.
            // caso contrário, carregue o cenaId normalmente (esse vai
            // ser o caso pro jogador final sempre)
#if UNITY_EDITOR
            if (usandoGGTest && cenaId == CenaID.Tabuleiro)
            {
                SceneManager.LoadScene("GGTest");
                return;
            }
#endif
            SceneManager.LoadScene((int)cenaId);
            //StartCoroutine(TransitarPara(cenaId));
        }

        // ALERTA: Por hora esse código é "teórico", ainda preciso testar
        IEnumerator TransitarPara(CenaID cenaId)
        {
            // abre cena de transição, isso já fechará o tabuleiro, talvez
            // dê uma travadinha, se ficar irritante mudaremos para
            // Async também
            SceneManager.LoadScene((int)CenaID.TelaCarregamento);

            // pega o número da cenaId
            int cena_num = (int)cenaId;

            // Carrega a cena assincronamente
            AsyncOperation cenaAsync = SceneManager.LoadSceneAsync(cena_num);

            // Enquanto o sujeito não carrega
            while (!cenaAsync.isDone)
            {
                // "Descansa" a co-rotina por 1 segundo independete do
                // Time.timescale
                yield return new WaitForSecondsRealtime(1);
            }
        }

#endregion gerenciamento de cenas
    }
}
