using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

public class BaldeDasMacas_Maca : MonoBehaviour
{
    public Vector3 posInicial;
    bool pego;
    Transform tr, macaAnterior;
    public int maca;
    public Material[] mats;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Start()
    {
        var gerenBM =
            FindObjectOfType<Gerenciadores.GerenciadorBaldeDasMacas>();

        posInicial.x = Random.Range(
            posInicial.x - gerenBM.limX,
            posInicial.x + gerenBM.limX
        );

        tr.position = posInicial;
    }

    void Update()
    {
        if (!pego)
            return;

        var pos = tr.position;
        pos.x = macaAnterior.position.x;
        pos.y = macaAnterior.position.y + 0.2f;
        tr.position = pos;
    }

    void OnTriggerEnter(Collider col)
    {
        if (!pego && col.tag == "Player")
        {
            pego = true;
            var ctrlMacas = col.GetComponent<ControladorBaldeDasMacas>();
            macaAnterior = ctrlMacas.PontuarMaca(tr);

            if (macaAnterior == null)
                macaAnterior = ctrlMacas.GetComponent<Transform>();

            Destroy(GetComponent<Rigidbody>());

            MeshRenderer chapeuMR = tr.GetChild(0).GetComponent<MeshRenderer>();
            JogadorID jid = col.GetComponent<IdentificadorJogador>().jogadorID;
            chapeuMR.material = mats[(int)jid];
        }
    }
}
