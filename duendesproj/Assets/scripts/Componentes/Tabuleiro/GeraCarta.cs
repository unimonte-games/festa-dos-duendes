using UnityEngine;
using UnityEngine.UI;
using Identificadores;

namespace Componentes.Tabuleiro
{
    public class GeraCarta : MonoBehaviour
    {
        public Image botao;
        public Gerenciadores.GerenciadorPartida _gerenPartida;
        public GeradorTabuleiro _geraTabuleiro;

        public void GerarCarta()
        {
            float rand = Random.value;
            TiposCasa carta;

            if (rand <= 0.1f)
                carta = TiposCasa.BemMal;
            else if (rand <= 0.2f)
                carta = TiposCasa.Garrafa;
            else if (rand <= 0.35f)
                carta = TiposCasa.Acontecimento;
            else if (rand <= 0.55f)
                carta = TiposCasa.PowerUp;
            else if (rand <= 0.9f)
                carta = TiposCasa.Moeda;
            else
                carta = TiposCasa.MiniJogo;

            PainelCartas.MostrarCarta(carta);

            _gerenPartida.MoverJogador((int)carta);
        }
    }
}
