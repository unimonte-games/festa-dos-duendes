using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gerenciadores
{
    public enum JogadorID {J1, J2, J3, J4};

    // TODO: explicitar os números das cenas abaixo
    public enum CenaID
    {
        Tabuleiro,
        QuebraBotao,
        PescaEscorrega,
        TelaCarregamento,
        // TODO: Colocar outros aqui
    };

    public class GerenciadorGeral : MonoBehaviour
    {
        public static int qtdJogadores = 4; // sobrescrito pelo Start por 0;
        public static int pontosPorVencedorMJ = 10;
        public static int[] pontuacao = new int[4];

        static GerenciadorGeral instancia;

        public static void ConfirmarNovoJogador()
        {
            qtdJogadores += 1;
        }

        public static void PontuarCampeaoMJ(JogadorID jogadorID)
        {
            pontuacao[(int)jogadorID] += pontosPorVencedorMJ;
            instancia._TransitarPara();
        }

        void _TransitarPara()
        {
            StartCoroutine(TransitarPara(CenaID.Tabuleiro));
        }

        void Start()
        {
            // eu acho que poderia escrever = this; mas não tenho certeza
            qtdJogadores = 0;
            instancia = GetComponent<GerenciadorGeral>();
        }

        // ALERTA: Por hora esse código é "teórico", ainda preciso testar
        IEnumerator TransitarPara(CenaID cenaID)
        {
            // abre cena de transição, isso já fechará o tabuleiro, talvez
            // dê uma travadinha, se ficar irritante mudaremos para
            // Async também
            SceneManager.LoadScene((int)CenaID.TelaCarregamento);

            // pega o número da cenaID
            int cena_num = (int)cenaID;

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
    }
}
