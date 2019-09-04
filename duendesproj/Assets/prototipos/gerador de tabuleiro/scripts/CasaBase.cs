using UnityEngine;
using System.Collections.Generic;

public class CasaBase : MonoBehaviour
{
    public List<Transform> proximaCasa;

    public void AddProximaCasa(Transform obj)
    {
        proximaCasa.Add(obj);
    }
}
