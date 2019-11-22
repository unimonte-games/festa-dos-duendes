using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Identificadores;

namespace Componentes.Tabuleiro
{
    public class PainelCartas : MonoBehaviour
    {
        public float vel;
        public RectTransform pn_cartas;
        public RectTransform[] cartas;

        public static void MostrarCarta(TiposCasa casa)
        {
            PainelCartas pn_cartas = FindObjectOfType<PainelCartas>();
            if (pn_cartas != null)
                pn_cartas._MostrarCarta(casa);
        }

        void _MostrarCarta (TiposCasa casa)
        {
            int i = -1;

            switch (casa)
            {
                case TiposCasa.Moeda:         i = 0; break;
                case TiposCasa.BemMal:        i = 1; break;
                case TiposCasa.PowerUp:       i = 2; break;
                case TiposCasa.Garrafa:       i = 3; break;
                case TiposCasa.Acontecimento: i = 4; break;
                case TiposCasa.MiniJogo:      i = 5; break;
            }

            if (i >= 0)
                StartCoroutine(co_MostrarCarta(i));
        }

        IEnumerator co_MostrarCarta(int idx_carta)
        {
            float t = 0f;
            float dtvel = Time.deltaTime * vel;

            while (true)
            {
                t = Mathf.Clamp01(t + dtvel);

                for (int i = 0; i < cartas.Length; i++)
                {
                    RectTransform carta = cartas[i];
                    bool cartaEmUso = idx_carta == i;

                    Vector3 min = new Vector3(
                        cartaEmUso ? 0f : 0.5f, cartaEmUso ? 0f : -1f
                    );
                    Vector3 max = new Vector3(
                        cartaEmUso ? 1f : 0.5f, cartaEmUso ? 1f :  0f
                    );

                    carta.anchorMin = Vector2.Lerp(carta.anchorMin, min, t);
                    carta.anchorMax = Vector2.Lerp(carta.anchorMax, max, t);
                }

                yield return new WaitForEndOfFrame();

                if (t >= 1)
                    break;
            }
        }

        public void MudaDescricao(TiposCasa casa, string descricao)
        {
            int i = -1;

            switch (casa)
            {
                case TiposCasa.Moeda: i = 0; break;
                case TiposCasa.BemMal: i = 1; break;
                case TiposCasa.PowerUp: i = 2; break;
                case TiposCasa.Garrafa: i = 3; break;
                case TiposCasa.Acontecimento: i = 4; break;
                case TiposCasa.MiniJogo: i = 5; break;
            }

            if (i >= 0)
            {
                Text textoCarta = cartas[i].GetComponentInChildren<Text>();
                textoCarta.text = descricao;
            }
        }
    }
}
