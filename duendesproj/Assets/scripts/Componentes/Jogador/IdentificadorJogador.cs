using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Identificadores;
using Photon.Pun;
using Photon.Realtime;

namespace Componentes.Jogador {
    public class IdentificadorJogador : MonoBehaviour
    {

        /// <summary>A qual jogador esse personagem pertence.</summary>
        public JogadorID jogadorID;
        public bool eMeu;

        void Start()
        {
            //var jogadores = FindObjectsOfType<IdentificadorJogador>();
//
            //switch(jogadores.Length)
            //{
                //case 0: jogadorID = JogadorID.J1; break;
                //case 1: jogadorID = JogadorID.J2; break;
                //case 2: jogadorID = JogadorID.J3; break;
                //case 3: jogadorID = JogadorID.J4; break;
            //}
        }

        public void DarPosse(int i)
        {
            var pl = PhotonNetwork.CurrentRoom.Players[i+1];
            var localPl = PhotonNetwork.LocalPlayer;

            // se o jogador passado pelo RPC é o local
            if (pl.ActorNumber == localPl.ActorNumber)
                eMeu = true;
        }
    }
}
