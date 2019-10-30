using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;
using Gerenciadores;

public class PescaEscorrega_Pescado : MonoBehaviour
{
    bool[] emContato = new bool[4];

    Controlador[] controladores = new Controlador[4];
    ControladorPescaEscorrega[] controladoresPE =
        new ControladorPescaEscorrega[4];

    void Update()
    {
        for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
        {
            if (emContato[i])
            {
                Controlador.EntradaJogador entradaJ =
                    controladores[i].ObterEntradaJogador();

                if (entradaJ.acao1) {
                    controladoresPE[i].PontuarPescado();
                    GerenciadorPescaEscorrega.pescadosAoMar--;
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            JogadorID jid = col.GetComponent<IdentificadorJogador>().jogadorID;

            emContato[(int)jid] = true;

            if (controladores[(int)jid] == null)
                controladores[(int)jid] = col.GetComponent<Controlador>();

            if (controladoresPE[(int)jid] == null)
                controladoresPE[(int)jid] = col.GetComponent<ControladorPescaEscorrega>();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            JogadorID jid = col.GetComponent<IdentificadorJogador>().jogadorID;
            emContato[(int)jid] = false;
        }
    }
}
