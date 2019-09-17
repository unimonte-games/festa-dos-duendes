using UnityEngine;
using System.Collections.Generic;

public class CasaBase : MonoBehaviour
{
    public List<Transform> proximaCasa;
    public List<Transform>  casaAnterior;

    public virtual void SetProximaCasa(Transform casa)
    {
        proximaCasa.Add(casa);
    }

    public virtual void SetCasaAnterior(Transform casa)
    {
        casaAnterior.Add(casa);
    }
}
