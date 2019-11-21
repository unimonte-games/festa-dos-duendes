using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Gerenciadores;

namespace Componentes.Jogador
{
    public class PosseDeJogador : MonoBehaviour
    {
        PhotonView pv;

        void Awake()
        {
            pv = GetComponent<PhotonView>();
        }

        void Start()
        {
            if (!GerenciadorGeral.modoOnline)
            {
                Destroy(GetComponent<PhotonTransformView>());
                Destroy(GetComponent<PhotonView>());
                Destroy(GetComponent<PosseDeJogador>());
            }
            else if (!pv.IsMine)
            {
                GetComponent<Movimentador>().enabled = false;
                GetComponent<ParamAnimSync_Jogador>().enabled = false;
            }
        }
    }
}
