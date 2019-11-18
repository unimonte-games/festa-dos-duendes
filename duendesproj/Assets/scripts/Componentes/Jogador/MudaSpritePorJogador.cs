using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Identificadores;

namespace Componentes.Jogador
{
    public class MudaSpritePorJogador : MonoBehaviour
    {
        public SpriteRenderer cabeca,
                              corpo,
                              manga,
                              mao;

        public Sprite J1_cabeca,
                      J1_corpo,
                      J1_manga,
                      J1_mao,

                      J2_cabeca,
                      J2_corpo,
                      J2_manga,
                      J2_mao,

                      J3_cabeca,
                      J3_corpo,
                      J3_manga,
                      J3_mao,

                      J4_cabeca,
                      J4_corpo,
                      J4_manga,
                      J4_mao;

        JogadorID jogadorId;


        // Start is called before the first frame update
        void Awake()
        {
            jogadorId = GetComponent<IdentificadorJogador>().jogadorID;
        }

        void Start()
        {
            switch (jogadorId)
            {
                case JogadorID.J1:
                    cabeca.sprite= J1_cabeca;
                    corpo.sprite = J1_corpo;
                    manga.sprite = J1_manga;
                    mao.sprite   = J1_mao;
                    break;

                case JogadorID.J2:
                    cabeca.sprite= J2_cabeca;
                    corpo.sprite = J2_corpo;
                    manga.sprite = J2_manga;
                    mao.sprite   = J2_mao;
                    break;

                case JogadorID.J3:
                    cabeca.sprite= J3_cabeca;
                    corpo.sprite = J3_corpo;
                    manga.sprite = J3_manga;
                    mao.sprite   = J3_mao;
                    break;

                case JogadorID.J4:
                    cabeca.sprite= J4_cabeca;
                    corpo.sprite = J4_corpo;
                    manga.sprite = J4_manga;
                    mao.sprite   = J4_mao;
                    break;
            }
        }
    }
}
