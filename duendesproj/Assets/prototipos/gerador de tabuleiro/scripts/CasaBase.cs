using UnityEngine;
using System.Collections.Generic;

public class CasaBase : MonoBehaviour
{
    public List<Transform> proximaCasa;
    public int indiceCasa;

    public void setProximaCasa(Transform casa)
    {
        proximaCasa.Add(casa);
    }
}
