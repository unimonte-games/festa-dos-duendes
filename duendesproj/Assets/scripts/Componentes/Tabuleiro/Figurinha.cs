using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Identificadores;

namespace Componentes.Tabuleiro
{
    public class Figurinha : MonoBehaviour
    {
        public Sprite figMinijogo;
        public Sprite figMoeda;
        public Sprite figBemMal;
        public Sprite figPowerUp;
        public Sprite figGarrafa;
        public Sprite figAcontecimentoAleatorio;

        void Start()
        {
            Transform casas = transform.Find("Casas");
            Transform figurinhas = transform.Find("Figurinhas");

            for (int i = 0; i < casas.childCount; i++)
            {
                Transform casaBase_tr = casas.GetChild(i);
                CasaBase casaBase = casaBase_tr.GetComponent<CasaBase>();

                if (casaBase.tipoCasa == TiposCasa.Moeda)
                    Instanciar(figMoeda, casaBase_tr, figurinhas);
                else if (casaBase.tipoCasa == TiposCasa.BemMal)
                    Instanciar(figBemMal, casaBase_tr, figurinhas);
                else if (casaBase.tipoCasa == TiposCasa.PowerUp)
                    Instanciar(figPowerUp, casaBase_tr, figurinhas);
                else if (casaBase.tipoCasa == TiposCasa.Garrafa)
                    Instanciar(figGarrafa, casaBase_tr, figurinhas);
                else if (casaBase.tipoCasa == TiposCasa.Acontecimento)
                    Instanciar(figAcontecimentoAleatorio, casaBase_tr, figurinhas);
                else if (casaBase.tipoCasa == TiposCasa.MiniJogo)
                    Instanciar(figMinijogo, casaBase_tr, figurinhas);
            }
        }

        void Instanciar(Sprite spr, Transform casaBase_tr, Transform figurinhas)
        {
            GameObject novaFigurinha = new GameObject("figurinha");
            Transform novaFigurinha_tr = novaFigurinha.transform;

            novaFigurinha_tr.position =
                casaBase_tr.position + (casaBase_tr.up * 0.05f);
            novaFigurinha_tr.rotation = Quaternion.Euler(90f, 0f, 0f);
            novaFigurinha_tr.SetParent(figurinhas);

            var sprRend = novaFigurinha.AddComponent<SpriteRenderer>();
            sprRend.sprite = spr;
        }
    }
}
