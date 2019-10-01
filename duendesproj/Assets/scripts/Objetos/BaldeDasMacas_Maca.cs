using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldeDasMacas_Maca : MonoBehaviour
{
    public Vector3 posInicial;
    public float intervaloX;


    void Start()
    {
        posInicial.x = Random.Range(
            posInicial.x - intervaloX,
            posInicial.x + intervaloX
        );

        transform.position = posInicial;
    }
}
