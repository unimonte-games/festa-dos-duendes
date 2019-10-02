using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;

public class BaldeDasMacas_Maca : MonoBehaviour
{
    public Vector3 posInicial;
    bool pego;

    void Start()
    {
        var gerenBM =
            FindObjectOfType<Gerenciadores.GerenciadorBaldeDasMacas>();

        posInicial.x = Random.Range(
            posInicial.x - gerenBM.limX,
            posInicial.x + gerenBM.limX
        );

        transform.position = posInicial;
    }

    void OnTriggerEnter(Collider col)
    {
        if (!pego && col.tag == "Player")
        {
            pego = true;
            var ctrlMacas = col.GetComponent<ControladorBaldeDasMacas>();
            ctrlMacas.PontuarMaca();
            Destroy(gameObject);
        }
    }
}
