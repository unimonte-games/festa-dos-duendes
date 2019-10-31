using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Tabuleiro;

public class TripeTabuleiro : MonoBehaviour
{
    Tripe tripe;
    GerenciadorPartida gerenP;

    void Awake ()
    {
        tripe = GetComponent<Tripe>();
        gerenP = FindObjectOfType<GerenciadorPartida>();
    }

    void Update ()
    {
        tripe.alvo = gerenP.ObterJogadorAtivo();
    }
}
