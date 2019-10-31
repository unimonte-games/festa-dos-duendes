using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gerenciadores;

public class TripeQuebraBotao : MonoBehaviour
{
    Tripe tripe;
    GerenciadorQuebraBotao gerenQB;

    void Awake ()
    {
        tripe = GetComponent<Tripe>();
        gerenQB = FindObjectOfType<GerenciadorQuebraBotao>();
    }

    void Update()
    {
        if (!gerenQB.gerenMJ.partidaIniciada)
            return;

        int jogadorMaisLonge = gerenQB.ObterMaiorZ();
        tripe.alvo = gerenQB.gerenMJ.tr_jogadores[jogadorMaisLonge];
    }
}
