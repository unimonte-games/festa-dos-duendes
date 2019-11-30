using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Gerenciadores;
using Componentes.Tabuleiro;

namespace Componentes.Jogador
{
    public class PosseDeJogador : MonoBehaviour
    {
        void Start()
        {
            var tronco_gbj = FindObjectOfType<TabuleiroRaiz>().tronco_gbj;
            transform.SetParent(tronco_gbj.transform);

            if (!GerenciadorGeral.modoOnline)
            {
                Destroy(GetComponent<PhotonTransformView>());
                Destroy(GetComponent<PhotonView>());
                Destroy(GetComponent<PosseDeJogador>());
            }
            else if (!GetComponent<IdentificadorJogador>().eMeu)
            {
                var mov1 = GetComponent<Movimentacao>();
                if (mov1)
                    mov1.enabled = false;

                var mov2 = GetComponent<Movimentador>();
                if (mov2)
                    mov2.enabled = false;

                GetComponent<ParamAnimSync_Jogador>().enabled = false;
            }
        }
    }
}
