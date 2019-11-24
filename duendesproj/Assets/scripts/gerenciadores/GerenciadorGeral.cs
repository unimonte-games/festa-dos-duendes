using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Identificadores;
using Componentes.Tabuleiro;
using Componentes.Jogador;
using Photon.Pun;
using Photon.Realtime;

namespace Gerenciadores
{
    public class GerenciadorGeral : MonoBehaviourPunCallbacks
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

        public static bool modoOnline;
        static bool conectando;
        public static string _versaoRede = "0.1";

        static GerenciadorGeral instancia;
        public static GameObject tabuleiroRaiz;
        public static JogadorID vencedorID;

        public static GerenciadorGeral ObterInstancia() { return instancia; }

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

            PhotonNetwork.AutomaticallySyncScene = true;
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
        public static void PontuarCampeaoMJ(JogadorID jogadorID, Objetos objMJ)
        {
            pontuacao[(int)jogadorID] += PONTOS_POR_VENCEDOR_MJ;

            Inventario inventarioJogador = GerenciadorPartida
                                            .OrdemJogadores[(int)jogadorID]
                                            .GetComponent<Inventario>();

            inventarioJogador.AlteraObjeto(objMJ, true);

            if (inventarioJogador.VerificaSeGanhou()) {
                vencedorID = jogadorID;
                instancia._TransitarPara(CenaID.Vencedor);
            }
            else
                instancia._TransitarPara(CenaID.Tabuleiro);
        }

        /// <summary>
        /// Transitará para a cena correspondente ao parâmetro
        /// </summary>
        public static void TransitarParaMJ(CenaID cenaId)
        {
            if (cenaId != CenaID.Tabuleiro &&
                cenaId != CenaID.Vencedor &&
                Telas.Preminijogo.cenaMJ != cenaId)
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

        public static void ConectarRede()
        {
            modoOnline = true;
            Debug.Log("ConectarRede(): " + PhotonNetwork.IsConnected);
            PhotonNetwork.LogLevel = PunLogLevel.Informational;

            conectando = true;
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public static void DesconectarRede()
        {
            modoOnline = false;
            PhotonNetwork.Disconnect();
        }

        public override void OnJoinRandomFailed(short returnCode, string msg)
        {
            Debug.Log(string.Concat(
                "callback OnJoinRandomFailed(",
                returnCode,
                ", \"",
                msg,
                "\") foi chamada"
            ));
            PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = 4});
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("callback OnJoinedRoom() foi chamada");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("callback OnConnectedToMaster() foi chamada");

            if (conectando)
                PhotonNetwork.JoinRandomRoom();
        }

#endregion gerenciamento de cenas
    }
}
