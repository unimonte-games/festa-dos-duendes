using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorQuebraBotao : MonoBehaviour
    {
        public GerenciadorMJLib gerenMJ;
        public float tamanhoPasso;

        void Awake()
        {
            gerenMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start()
        {
            gerenMJ.evtAoIniciar.AddListener(AoIniciar);
            gerenMJ.evtAoTerminar.AddListener(AoTerminar);
        }

        void AoIniciar()
        {
            AplicarControladorQuebraBotao();
        }

        void AoTerminar()
        {
            JogadorID jogadorCampeao = ObterCampeao();
            GerenciadorGeral.PontuarCampeaoMJ(jogadorCampeao);
        }

        void AplicarControladorQuebraBotao ()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogaodor = tr_jogadores[i].gameObject;
                gbj_jogaodor.AddComponent<ControladorQuebraBotao>();
            }
        }

        JogadorID ObterCampeao()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            int maisLonge_i = ObterMaiorZ();

            // Obtém o jogadorID do campeão
            IdentificadorJogador idJogador =
                tr_jogadores[maisLonge_i].GetComponent<IdentificadorJogador>();

            // retorna jogadorID obtido
            return idJogador.jogadorID;
        }

        // itera sobre todos os jogadores,
        // vê quem está mais longe no eixo Z
        public int ObterMaiorZ()
        {
            float maisLonge = -100000;
            int maisLonge_i = -1;

            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                float jogador_i_posz = tr_jogadores[i].position.z;

                if (jogador_i_posz > maisLonge)
                {
                    maisLonge = jogador_i_posz;
                    maisLonge_i = i;
                }
            }

            return maisLonge_i;
        }
    }
}
