using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gerenciadores;
using Identificadores;
using Componentes.Jogador;
using Photon.Pun;
using Photon.Realtime;

namespace Telas
{
    public class MenuPrincipal : MonoBehaviour
    {
        public Transform circulos,
                         listaJogadores;

        public Button adicionarJogador, removerJogador;

        public RectTransform[] telas;

        int telaAtual;

        static readonly Vector2 telaMin_Visivel   = Vector2.zero;
        static readonly Vector2 telaMin_Invisivel = Vector2.right;
        static readonly Vector2 telaMax_Visivel   = new Vector2(1f, 1f);
        static readonly Vector2 telaMax_Invisivel = new Vector2(2f, 1f);

        void Start()
        {
            for (int i = 0; i < circulos.childCount; i++)
            {
                Transform circulo = circulos.GetChild(i);
                var mov =
                    circulo.GetComponent<Componentes.Jogador.Movimentador>();
                mov.velocidade = Random.Range(0.5f, 1f);
            }

            AtualizaListaVisualJogadores();
        }

        void Update()
        {
            for (int i = 0; i < circulos.childCount; i++)
            {
                Transform circulo = circulos.GetChild(i);

                Vector3 pos = circulo.position;
                if (circulo.position.x > 22f)
                    pos.x = -22f;
                if (circulo.position.y < -8f)
                    pos.y = 8f;
                circulo.position = pos;

                circulo.Rotate(Vector3.forward * Time.deltaTime * 20);
            }

            for (int i = 0; i < telas.Length; i++)
            {
                float vel = Time.deltaTime * 5;
                RectTransform tela_i = telas[i];
                bool telaEVisivel = telaAtual == i;

                Vector2 telaMin_alvo =
                    telaEVisivel ? telaMin_Visivel : telaMin_Invisivel;
                Vector2 telaMax_alvo =
                    telaEVisivel ? telaMax_Visivel : telaMax_Invisivel;

                tela_i.anchorMin = Vector2.Lerp(
                    tela_i.anchorMin, telaMin_alvo, vel
                );
                tela_i.anchorMax = Vector2.Lerp(
                    tela_i.anchorMax, telaMax_alvo, vel
                );
            }
        }

        public void BtMenuPrincipal()
        {
            telaAtual = 0;
            if (GerenciadorGeral.modoOnline)
                GerenciadorGeral.DesconectarRede();
        }
        public void BtJogar()         { telaAtual = 1; }
        public void BtInstrucoes()    { telaAtual = 2; }
        public void BtCreditos()      { telaAtual = 3; }
        public void BtJogarRede()
        {
            telaAtual = 4;
            GerenciadorGeral.ConectarRede();
        }

        public void BtIniciarPartida()
        {
            if (GerenciadorGeral.modoOnline && PhotonNetwork.IsMasterClient)
            {
                var room = PhotonNetwork.CurrentRoom;
                if (room == null)
                    return;

                GerenciadorGeral.qtdJogadores = room.PlayerCount;
            }


            GerenciadorGeral.TransitarParaMJ(CenaID.Tabuleiro);
        }

        //public void BtIniciarPartidaRede()
        //{
            //GerenciadorGeral.TransitarParaMJ(CenaID.Tabuleiro);
        //}

        public void CadastrarJogador()
        {
            GerenciadorGeral.CadastrarJogador();
            AtualizaListaVisualJogadores();
        }

        public void DecadastrarJogador()
        {
            GerenciadorGeral.DecadastrarJogador();
            AtualizaListaVisualJogadores();
        }

        void AtualizaListaVisualJogadores()
        {
            for (int i = 0; i < 4; i++)
            {
                var qtdJogadores = GerenciadorGeral.qtdJogadores;
                Color corFinal = i < qtdJogadores ? Color.white : Color.grey;

                var duendeSprites = listaJogadores
                                        .GetChild(i)
                                        .GetChild(0)
                                        .GetComponent<MudaSpritePorJogador>();

                duendeSprites.cabeca.color = corFinal;
                duendeSprites.corpo.color = corFinal;
                duendeSprites.manga.color = corFinal;
                duendeSprites.mao.color = corFinal;
            }

            adicionarJogador.interactable = GerenciadorGeral.PodeCadastrar();
            removerJogador.interactable = GerenciadorGeral.PodeDecadastrar();
        }
    }
}

