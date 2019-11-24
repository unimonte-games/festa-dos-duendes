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

        public void AddPowerUp(TipoPowerUps novoPowerUp, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;

            Transform pnlDescricao = TabuleiroHUD.PnlDescricoes.GetChild(GerenciadorPartida.Turno);

            string txt = LeitorDescr.LeLinha((int)novoPowerUp);

            PowerUp pw = new PowerUp();
            pw.tipo = novoPowerUp;
            pw.titulo = txt.Split(';')[0];
            pw.descricao = txt.Split(';')[1];

            powerUps.Add(pw);
            pnlDescricao = pnlDescricao.GetChild(powerUps.Count-1);

            pnlDescricao.Find("titulo").GetComponentInChildren<Text>().text = pw.titulo;
            pnlDescricao.Find("conteudo").GetComponentInChildren<Text>().text = pw.descricao;
        }

        public void RemovePowerUp(int qtd, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;

            Transform pnlPowerUp = TabuleiroHUD.Paineis[i].transform.Find("Painel PowerUps");
            Text pnlDescricao = TabuleiroHUD.PnlDescricoes.GetComponentInChildren<Text>();

            if (qtd <= powerUps.Count)
            {
                for (int j = qtd; j >= 0; j--)
                {
                    powerUps.RemoveAt(j);
                    Image fundo = pnlPowerUp.transform.GetChild(j).GetComponent<Image>();
                    fundo.color = Color.black;
                }
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
