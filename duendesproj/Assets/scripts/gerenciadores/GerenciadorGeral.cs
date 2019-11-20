﻿using System.Collections;
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
        public static GameObject tabuleiroRaiz;

        public static GerenciadorGeral ObterInstancia()
        {
            return instancia;
        }

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
            if (Telas.Preminijogo.cenaMJ != cenaId)
            {
                TabuleiroRaiz.Desativar();
                Telas.Preminijogo.cenaMJ = cenaId;
                instancia._TransitarPara(CenaID.PreMiniJogo);
            }
            else
            {
                instancia._TransitarPara(cenaId);
            }
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
        }

        public static CenaID ReconhecerCena(int idx)
        {
            switch (idx)
            {
                case (int)CenaID.Tabuleiro:      return CenaID.Tabuleiro;
                case (int)CenaID.QuebraBotao:    return CenaID.QuebraBotao;
                case (int)CenaID.BaldeDasMacas:  return CenaID.BaldeDasMacas;
                case (int)CenaID.PescaEscorrega: return CenaID.PescaEscorrega;
                case (int)CenaID.CogumeloQuente: return CenaID.CogumeloQuente;
                case (int)CenaID.FlautaHero:     return CenaID.FlautaHero;
                case (int)CenaID.MenuInicial:    return CenaID.MenuInicial;
                case (int)CenaID.PreMiniJogo:    return CenaID.PreMiniJogo;
            }
            return CenaID.Nenhum;
        }

#endregion gerenciamento de cenas
    }
}
