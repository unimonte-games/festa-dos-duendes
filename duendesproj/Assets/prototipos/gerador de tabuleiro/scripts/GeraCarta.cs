using UnityEngine;
using UnityEngine.UI;

public class GeraCarta : MonoBehaviour
{
    public Image botao;
    public Movimentacao _mov;

    public void GerarCarta()
    {
        int rand = Random.Range(1, 6);

        switch (rand)
        {
            case 1:
                botao.color = Color.red;
                break;

            case 2:
                botao.color = Color.magenta;
                break;

            case 3:
                botao.color = Color.yellow;
                break;

            case 4:
                botao.color = Color.blue;
                break;

            case 5:
                botao.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
                break;

            case 6:
                botao.color = Color.green;
                break;
        }

        _mov.ProcuraCasa(rand);
    }
}