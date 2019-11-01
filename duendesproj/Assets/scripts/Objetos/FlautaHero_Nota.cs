using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Gerenciadores;
using Identificadores;

public class FlautaHero_Nota : MonoBehaviour
{
    GerenciadorFlautaHero gerenFH;
    public int tempoIndice;
    public JogadorID jid;

    ControladorFlautaHero ctrlFH;

    Transform tr;

    void Awake()
    {
        gerenFH = FindObjectOfType<GerenciadorFlautaHero>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        if (!gerenFH.gerenMJ.partidaIniciada || gerenFH.gerenMJ.partidaEncerrada)
            return;

        if (ctrlFH == null)
        {
            ctrlFH = gerenFH
                        .gerenMJ
                        .tr_jogadores[(int)jid]
                        .GetComponent<ControladorFlautaHero>();
        }

        if (ctrlFH.temposUtilizados[tempoIndice])
            Destroy(gameObject);
        else
        {
            float velocidadeMov = gerenFH.velocidadeMov;
            float tempoPartida = gerenFH.gerenMJ.tempoPartida;
            float tempo = gerenFH.tempos[tempoIndice];

            Vector3 pos = tr.position;
            pos.z = (tempoPartida * velocidadeMov) - (tempo * velocidadeMov);
            tr.position = pos;
        }
    }
}
