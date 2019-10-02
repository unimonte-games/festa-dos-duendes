using UnityEngine;
using UnityEngine.UI;

public class GeraCarta : MonoBehaviour
{
    public Image botao;
    public Movimentacao jogador;

    public void GerarCarta()
    {
        int rand = Random.Range(1, 6);

        switch (rand)
        {
            case 1:
                botao.color = Color.red;
                Debug.Log("Cor vermelha");
                break;

            case 2:
                botao.color = Color.magenta;
                Debug.Log("Cor roxo");
                break;

            case 3:
                botao.color = Color.yellow;
                Debug.Log("Cor amarelo");
                break;

            case 4:
                botao.color = Color.blue;
                Debug.Log("Cor azul");
                break;

            case 5:
                botao.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
                Debug.Log("Cor laranja");
                break;

            case 6:
                botao.color = Color.green;
                Debug.Log("Cor verde");
                break;
        }

        jogador.ProcuraCasa(rand);
    }
}