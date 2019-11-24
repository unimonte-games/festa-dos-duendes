using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Identificadores;
using Componentes.Tabuleiro;
using Gerenciadores;

namespace Componentes.Jogador
{
    public class Inventario : MonoBehaviour
    {
        public int moedas = 0;
        public List<Objetos> objetos = new List<Objetos>();
        public List<PowerUp> powerUps = new List<PowerUp>();
        public int rodadasPreso = 0;
        public int rodadasSemObj = 0;

        public void AlteraMoeda(int qtd, bool substitui = false)
        {
            moedas = substitui ? qtd : qtd + moedas;
            if (moedas < 0) moedas = 0;

            int i = GerenciadorPartida.Turno;
            Transform txtMoedas = TabuleiroHUD.Paineis[i].transform.Find("Painel Moedas");
            txtMoedas.GetComponentInChildren<Text>().text = moedas + " moedas";
        }

        public void AlteraPowerUp(int qtd, bool estado, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;

            Transform pnlPowerUp = TabuleiroHUD.Paineis[i].transform.Find("Painel PowerUps");

            for (int j = 0; j < qtd; j++)
            {
                Image fundo = pnlPowerUp.transform.GetChild(j).GetComponent<Image>();
                fundo.color = estado ? Color.green : Color.black;

                //pnlDescricao.GetComponentInChildren<Text>().text = GerenciadorPartida.descricaoCarta;
            }
        }

        public void AlteraObjeto(Objetos obj, bool estado)
        {
            if (estado)
            {
                objetos.Add(obj);
                //TODO: mudar cor do icone
            }
            else
            {
                objetos.Remove(obj);
                //TODO: mudar cor do icone
            }
        }

        public bool VerificaSeGanhou()
        {
            int index = 0;
            while (objetos.Contains((Objetos)index))
                index++;

            return index == 5;
        }
    }
}
