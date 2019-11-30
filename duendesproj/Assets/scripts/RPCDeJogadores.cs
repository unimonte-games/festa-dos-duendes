using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Gerenciadores;
using Componentes.Tabuleiro;
using Componentes.Jogador;
using Identificadores;

public class RPCDeJogadores : MonoBehaviour
{
    IdentificadorJogador idJ;
    GeraCarta geraCarta;
    PowerUps powerUps;
    EscolheRota escolheRota;

    public static bool DeveUsarRPC()
    {
        return GerenciadorGeral.modoOnline && !PhotonNetwork.IsMasterClient;
    }

    public static void UsarRPC(string rpcStr)
    {
        PhotonView pvLocal = GerenciadorPartida.ObterPVLocal();
        pvLocal.RPC(rpcStr, RpcTarget.MasterClient);
    }

    public static void UsarRPCArg<T>(string rpcStr, T t)
    {
        PhotonView pvLocal = GerenciadorPartida.ObterPVLocal();
        pvLocal.RPC(rpcStr, RpcTarget.MasterClient, t);
    }

    void Awake()
    {
        idJ = GetComponent<IdentificadorJogador>();
        geraCarta = FindObjectOfType<GeraCarta>();
        powerUps = FindObjectOfType<PowerUps>();
        escolheRota = FindObjectOfType<EscolheRota>();
    }

    [PunRPC]
    void RPC_GerarCarta()
    {
        geraCarta.GerarCarta();
    }

    //[PunRPC]
    //void RPC_AtivarPowerUp(int powerUp)
    //{
    //    powerUps.AtivarPowerUp(powerUp);
    //}

    [PunRPC]
    void RPC_EscolherRota(bool confirmacao)
    {
        escolheRota.EscolherRota(confirmacao);
    }

    [PunRPC]
    void RPC_DarPosse(int i)
    {
        idJ.DarPosse(i);
    }
}
