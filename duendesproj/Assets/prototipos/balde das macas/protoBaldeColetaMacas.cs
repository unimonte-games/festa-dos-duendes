using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoBaldeColetaMacas : MonoBehaviour
{
    int macasColetadas;

    public int GetMacasColetadas() { return macasColetadas; }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Maca"))
        {
            macasColetadas = macasColetadas + 1;
            Destroy(col.gameObject);
        }
    }
}
