using UnityEngine;
using UnityEngine.UI;

public class GeraCarta : MonoBehaviour
{
    public Image botao;
    public void GerarCarta()
    {
        int rand = Random.Range(1, 6);

        if (rand == 1)
            botao.color = Color.red;
        else if (rand == 2)
            botao.color = Color.magenta;
        else if (rand == 3)
            botao.color = Color.yellow;
        else if (rand == 4)
            botao.color = Color.blue;
        else if (rand == 5)
            botao.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
        else if (rand == 6)
            botao.color = Color.green;

    }
}