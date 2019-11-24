using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Tabuleiro;
using Gerenciadores;

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
        if (GerenciadorPartida.OrdemJogadores.Count > 0)
            tripe.alvo = gerenP.ObterJogadorAtivo();
    }
}
