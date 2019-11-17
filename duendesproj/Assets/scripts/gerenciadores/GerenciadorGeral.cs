using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorGeral : MonoBehaviour
    {
        /// <summary>
        /// Quantidade de jogadores em jogo,
        /// apesar de possível, não o modifique externamente.
        /// </summary>
        public static int qtdJogadores = 4;
        /// <summary>
        /// Pontos para serem dados ao jogador que ganhar um minijogo.
        ///</summary>
        public const  int PONTOS_POR_VENCEDOR_MJ = 10;

        /// <summary>
        /// inicializado com tamanho 4, armazena pontuação de cada jogador;
        /// não o modifique externamente.
        /// </summary>
        public static int[] pontuacao = new int[4];

#region UNITY_EDITOR
        public bool usandoGGTest;
#endregion

        static GerenciadorGeral instancia;

        void Awake()
        {
            if (instancia != null)
                Destroy(gameObject);
        }

        void Start()
        {
            // eu acho que poderia escrever = this; mas não tenho certeza
            instancia = GetComponent<GerenciadorGeral>();
            DontDestroyOnLoad(gameObject);
        }

#region (de)cadastramento de jogadores

        /// <summary>
        /// Retorna com uma booliana que diz se há espaço para mais um jogador.
        /// </summary>
        public static bool PodeCadastrar()   { return qtdJogadores < 4;  }
        /// <summary>
        /// Retorna com uma booliana que diz se há um jogador que pode ser
        /// descadastrado.
        /// </summary>
        public static bool PodeDecadastrar() { return qtdJogadores > 0; }

        /// <summary>Se possível, cadastra um jogador.</summary>
        public static void CadastrarJogador()
        {
            if (PodeCadastrar())
                qtdJogadores += 1;
        }

        /// <summary>Se possível, descadastra um jogador.</summary>
        public static void DecadastrarJogador()
        {
            if (PodeDecadastrar())
                qtdJogadores -= 1;
        }

#endregion (de)cadastramento de jogadores

#region gerenciamento de cenas

        /// <summary>
        /// Pontua o jogador correspondente ao parâmetro
        /// com PONTOS_POR_VENCEDOR_MJ pontos.
        /// </summary>
        public static void PontuarCampeaoMJ(JogadorID jogadorID)
        {
            pontuacao[(int)jogadorID] += PONTOS_POR_VENCEDOR_MJ;
            instancia._TransitarPara(CenaID.Tabuleiro);
        }

        /// <summary>
        /// Transitará para a cena correspondente ao parâmetro
        /// </summary>
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
