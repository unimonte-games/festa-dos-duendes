using UnityEngine;
using UnityEngine.UI;
using Identificadores;
using Photon.Pun;
using Gerenciadores;

namespace Componentes.Tabuleiro
{
    public class GeraCarta : MonoBehaviour
    {
        public Image botao;
        public GerenciadorPartida _gerenPartida;
        public GeradorTabuleiro _geraTabuleiro;
        public PainelCartas _painelCartas;
        private EscolheRota _escolheRota;

        private void Start()
        {
            _escolheRota = GetComponent<EscolheRota>();
        }

        public void GerarCarta()
        {
            if (RPCDeJogadores.DeveUsarRPC())
            {
                RPCDeJogadores.UsarRPC("RPC_GerarCarta");
                return;
            }

            Debug.Log("GerarCarta()");

            float rand = Random.value;
            TiposCasa carta;

            if (rand <= 0.1f) // 10%
            {
                carta = TiposCasa.BemMal;
                _painelCartas.MudaDescricao(carta, "Benção ou Maldição");
            }
            else if (rand <= 0.2f) // 10%
            {
                carta = TiposCasa.Garrafa;
            }
            else if (rand <= 0.35f) // 15%
            {
                carta = TiposCasa.Acontecimento;
                _painelCartas.MudaDescricao(carta, "Acontecimento Aleatório");
            }
            else if (rand <= 0.50f) // 15%
            {
                carta = TiposCasa.PowerUp;
                _painelCartas.MudaDescricao(carta, "Melhoramento Aleatório");
            }
            else if (rand <= 0.85f) // 35%
                carta = TiposCasa.Moeda;
            else // 15%
                carta = TiposCasa.MiniJogo;

            _escolheRota.estadoPowerUp = true;
            _escolheRota.AlteraEstadoPowerUps();

            PainelCartas.MostrarCarta(carta);

            _gerenPartida.MoverJogador((int)carta);
        }
    }
}
