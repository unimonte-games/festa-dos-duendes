using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores {
    public class GerenciadorFlautaHero : MonoBehaviour
    {
        public float velocidadeMov;

        [HideInInspector]
        public GerenciadorMJLib gerenMJ;

        public float[] tempos = {
            05, 10, 15, 20, 25, 30, // cada linha tem 6 tempos
            35, 40, 45, 50, 55, 60
        };
        public bool[] temposUtilizados;

        public int temposAtual = 0;

        float tempoInicio;

        void Awake ()
        {
            gerenMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start ()
        {
            temposUtilizados = new bool[tempos.Length];
            gerenMJ.evtAoIniciar.AddListener(AoIniciar);
            gerenMJ.evtAoTerminar.AddListener(AoTerminar);
        }

        void Update()
        {
            // intervalo "[0, (tLen-1)[" ou "0 <= tA < (tLen-1)"
            if ( 0 <= temposAtual && temposAtual < tempos.Length - 1)
            {
                // Observação:
                // suponha que o tempos[temposAtual] = 5
                // assim que tempoPartida for >= 5, ele passa pro
                // próximo (por exemplo 10)
                if (gerenMJ.tempoPartida >= tempos[temposAtual])
                    temposAtual++;
            }
        }

        public float CalcPonto()
        {
            // (leia a observação no Update())
            // o que vale é o tempo mais próximo na faixa de 1s
            // sendo assim, pode ser que o tempos mais próximo seja
            // do índice anterior, atual (chamado aqui de corrente)
            // ou o próximo.
            // por isso pegamos os 3 e vemos qual o mais próximo.

            // declarando tempos com número enorme por padrão
            float temposAnterior = 10000f;
            float temposCorrente = 10000f;
            float temposProximo  = 10000f;
            int indiceUtilizado = temposAtual;

            bool podeAnterior =
                temposAtual > 0 && !temposUtilizados[temposAtual-1];

            bool podeCorrente =
                !temposUtilizados[temposAtual];

            bool podeProximo =
                temposAtual < tempos.Length-1 && !temposUtilizados[temposAtual+1];

            // pega os valores de tempos se possível e se não utilizado
            if (podeAnterior)
                temposAnterior = tempos[temposAtual-1];

            if (podeCorrente)
                temposCorrente = tempos[temposAtual];

            if (podeProximo)
                temposProximo = tempos[temposAtual+1];

            // se não foi possível obter nenhum valor, retorna com 0 pontos
            if (!podeAnterior && !podeCorrente && !podeProximo) {
                return 0f;
            }

            // calcula diferenças, se não foi possível obter o valor
            // então a diferença já será enorme graças ao 10000 acima;
            // temposAnterior, temposCorrente e temposProximo
            // foram encurtados para TA, TC e TP respectivamente
            float difTA = gerenMJ.tempoPartida - temposAnterior;
            float difTC = gerenMJ.tempoPartida - temposCorrente;
            float difTP = gerenMJ.tempoPartida - temposProximo;

            // Só devemos considerar tempos dentro de uma latência de 1s
            // resolvi criar uma cópia com prefixo _ pra conservar o original
            bool _podeAnterior= podeAnterior ? (Mathf.Abs(difTA) <= 1f) : false;
            bool _podeCorrente= podeCorrente ? (Mathf.Abs(difTC) <= 1f) : false;
            bool _podeProximo = podeProximo  ? (Mathf.Abs(difTP) <= 1f) : false;

            // se não há tempo próximo, retorna com 0
            if (!_podeAnterior && !_podeCorrente && !_podeProximo) {
                return 0f;
            }

            // obter diferença do tempos mais próximo, informando
            // também o índice utilizado, por padrão se usa o corrente
            float difTempos = difTA;

            if (difTA > difTC && difTA > difTP && _podeAnterior) {
                difTempos = difTA;
                indiceUtilizado = temposAtual-1;
            } else if (difTC > difTA && difTC > difTP && _podeCorrente) {
                difTempos = difTC;
                indiceUtilizado = temposAtual;
            } else if (difTP > difTA && difTP > difTC && _podeProximo) {
                difTempos = difTP;
                indiceUtilizado = temposAtual+1;
            }

            // marca o índice usado como "utilizado"
            temposUtilizados[indiceUtilizado] = true;

            // finalmente podemos calcular a pontuação
            // é definida como 1 - clamp01(abs(difTempos))
            float pontuacao = 1f - Mathf.Clamp01(Mathf.Abs(difTempos));

            Debug.Log("pontuacao: " + pontuacao.ToString());

            return pontuacao;
        }

        void AoIniciar()
        {
            AplicarControladorFlautaHero();
        }

        void AoTerminar()
        {
            JogadorID jogadorCampeao = ObterCampeao();
            GerenciadorGeral.PontuarCampeaoMJ(jogadorCampeao);
        }

        void AplicarControladorFlautaHero()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                gbj_jogador.AddComponent<ControladorFlautaHero>();
            }
        }

        JogadorID ObterCampeao()
        {
            float pontos = 0;

            JogadorID jid_ganhador = JogadorID.J1;

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform tr_j = gerenMJ.tr_jogadores[i];
                var ctrl = tr_j.GetComponent<ControladorFlautaHero>();

                if (ctrl.pontos >= pontos)
                {
                    var id_comp = tr_j.GetComponent<IdentificadorJogador>();
                    jid_ganhador = id_comp.jogadorID;
                }
            }

            return jid_ganhador;
        }
    }
}
