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
        if (!gerenFH.gerenMJ.partidaIniciada)
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
            Vector3 pos = tr.position;
            pos.z = gerenFH.gerenMJ.tempoPartida - gerenFH.tempos[tempoIndice];
            tr.position = pos;
        }
    }
}
