using UnityEngine;
using UnityEngine.UI;
using Identificadores;
using Photon.Pun;
using Photon.Realtime;
using Gerenciadores;

namespace Componentes.Tabuleiro
{
    public class GeraCarta : MonoBehaviour
    {
        public Image botao;
        public Gerenciadores.GerenciadorPartida _gerenPartida;
        public GeradorTabuleiro _geraTabuleiro;
        public PainelCartas _painelCartas;

        private string desc;

        public void GerarCarta()
        {
            if (GerenciadorGeral.modoOnline && !PhotonNetwork.IsMasterClient)
            {
                PhotonView pvLocal = GerenciadorPartida.ObterPVLocal();
                pvLocal.RPC("RPC_GerarCarta", RpcTarget.MasterClient);
                return;
            }

            float rand = Random.value;
            TiposCasa carta;

            if (rand <= 0.1f)
            {
                carta = TiposCasa.BemMal;
                _painelCartas.MudaDescricao(carta, "Benção ou Maldição");
            }
            else if (rand <= 0.2f)
            {
                carta = TiposCasa.Garrafa;
            }
            else if (rand <= 0.35f)
            {
                carta = TiposCasa.Acontecimento;
                _painelCartas.MudaDescricao(carta, "Acontecimento Aleatório");
            }
            else if (rand <= 0.55f)
            {
                carta = TiposCasa.PowerUp;
                _painelCartas.MudaDescricao(carta, "Melhoramento Aleatório");
            }
            else if (rand <= 0.9f)
                carta = TiposCasa.Moeda;
            else
                carta = TiposCasa.MiniJogo;

            PainelCartas.MostrarCarta(carta);

            _gerenPartida.MoverJogador((int)carta);
        }
    }
}
