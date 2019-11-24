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

    [PunRPC]
    void RPC_AtivarPowerUp(int powerUp)
    {
        powerUps.AtivarPowerUp(powerUp);
    }

    [PunRPC]
    void RPC_EscolherRota(bool confirmacao)
    {
        escolheRota.EscolherRota(confirmacao);
    }
}
