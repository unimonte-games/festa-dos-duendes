using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace DsvUtils
{
    /// <summary>
    /// Serve pra visulizar informações gerais do Photon
    /// </summary>
    public class InspetorPhoton : MonoBehaviour
    {
        public enum TipoSaida { DebugLog, GUIText };

        public bool informar;

        public TipoSaida tipoSaidaIsMasterClient,
                         tipoSaidaIsMine,
                         tipoSaidaRoomPlayerCount,
                         tipoSaidaIsConnected,
                         tipoSaidaIsConnectedReady;

        public Text textIsMasterClient,
                    textIsMine,
                    textRoomPlayerCount,
                    textIsConnected,
                    textIsConnectedReady;


        public PhotonView photonView;

        void Update()
        {
            if (!informar)
                return;

            string isMasterClient = PhotonNetwork.IsMasterClient.ToString();
            ImprimirInfo(
                tipoSaidaIsMasterClient, textIsMasterClient, isMasterClient
            );

            string isMine = (
                photonView != null
                    ? photonView.IsMine.ToString()
                    : "photonView é null"
            );
            ImprimirInfo(
                tipoSaidaIsMine, textIsMine, isMine
            );

            string roomPlayerCount = (
                PhotonNetwork.CurrentRoom != null
                    ? PhotonNetwork.CurrentRoom.PlayerCount.ToString()
                    : "PhotonNetwork.CurrentRoom é null"
            );
            ImprimirInfo(
                tipoSaidaRoomPlayerCount, textRoomPlayerCount, roomPlayerCount
            );

            string isConnected = PhotonNetwork.IsConnected.ToString();
            ImprimirInfo(
                tipoSaidaIsConnected, textIsConnected, isConnected
            );

            string isConnectedReady = PhotonNetwork.IsConnectedAndReady.ToString();
            ImprimirInfo(
                tipoSaidaIsConnectedReady, textIsConnectedReady, isConnectedReady
            );
        }

        void ImprimirInfo(TipoSaida tipoSaida, Text text, string info)
        {
            if (text == null)
                return;

            switch (tipoSaida)
            {
                case TipoSaida.DebugLog: Debug.Log(info, gameObject); break;
                case TipoSaida.GUIText: text.text = info; break;
            }
        }

        public static void ImprimirDicio<T, T2>(Dictionary<T, T2> dict)
        {
            string str = "";

            foreach (KeyValuePair<T, T2> kvp in dict)
            {
                str = string.Concat(
                    str,
                    string.Format("K={0}, V={1}\n", kvp.Key, kvp.Value)
                );
            }

            Debug.Log(str);
        }
    }
}
