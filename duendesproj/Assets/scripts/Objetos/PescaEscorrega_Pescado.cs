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

    bool vaiDestruirSe;

    Vector3 posInicial;
    Transform flor_tr;

    float t1, t2;
    const float velLerp = 2f;

    void Awake()
    {
        flor_tr = transform.GetChild(0);
    }

    void Start()
    {
        flor_tr.localScale = Vector3.zero;
        posInicial = flor_tr.localPosition;
    }

    void Update()
    {

        if (vaiDestruirSe)
        {
            if (t1 < 0.99f)
            {
                t1 += Time.deltaTime;
                flor_tr.localScale = Vector3.Lerp(
                    flor_tr.localScale, Vector3.zero, t1 * velLerp
                );
            }

            if (t2 < 0.99f)
            {
                t2 += Time.deltaTime;
                flor_tr.localPosition = Vector3.Lerp(
                    flor_tr.localPosition, posInicial + flor_tr.up, t2 * velLerp
                );
            }
            else
                Destroy(gameObject);

        }
        else
        {
            if (t1 < 0.99f)
            {
                t1 += Time.deltaTime;
                flor_tr.localScale = Vector3.Lerp(
                    Vector3.zero, Vector3.one, t1 * velLerp
                );
            }

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                if (emContato[i])
                {
                    Controlador.EntradaJogador entradaJ =
                        controladores[i].ObterEntradaJogador();

                    if (entradaJ.acao1) {
                        controladoresPE[i].PontuarPescado();
                        GerenciadorPescaEscorrega.pescadosAoMar--;
                        vaiDestruirSe = true;
                        GetComponent<BoxCollider>().enabled = false;
                        t1 = 0f;
                    }
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
