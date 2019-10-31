using UnityEngine;
using UnityEngine.UI;

namespace Componentes.Tabuleiro
{
    public class GeraCarta : MonoBehaviour
    {
        public Image botao;
        public Gerenciadores.GerenciadorPartida _gerenPartida;
        public GeradorTabuleiro _geraTabuleiro;
        private int qtdCasas;

        private void Awake()
        {
            qtdCasas = _geraTabuleiro.ordemCores.Length;
        }

        public void GerarCarta()
        {
            int rand = Random.Range(0, qtdCasas);

            botao.color = _geraTabuleiro.ordemCores[rand].color;

            _gerenPartida.MoverJogador(rand + 1);
        }
    }
}