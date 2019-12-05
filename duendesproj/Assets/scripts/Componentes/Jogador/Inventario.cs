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
        public GameObject garrafaGbj;

        public void AlteraMoeda(int qtd, bool substitui = false, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;
            Inventario inv = GerenciadorPartida.OrdemJogadores[i].GetComponent<Inventario>();

            inv.moedas = substitui ? qtd : qtd + inv.moedas;
            if (inv.moedas < 0) inv.moedas = 0;

            Transform txtMoedas = TabuleiroHUD.Paineis[i].transform.Find("Painel Moedas");
            txtMoedas.GetComponentInChildren<Text>().text = moedas + " moedas";
        }

        public void AddPowerUp(TipoPowerUps novoPowerUp, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;
            Inventario inv = GerenciadorPartida.OrdemJogadores[i].GetComponent<Inventario>();

            //Transform pnlDescricao = TabuleiroHUD.PnlDescricoes.GetChild(i);
            if (inv.powerUps.Count >= 3)
                return;

            Transform pnlDescricao = TabuleiroHUD.PnlsDescricoes[i];

            bool pnlActive = pnlDescricao.gameObject.activeSelf;
            pnlDescricao.gameObject.SetActive(true);

            string txt = LeitorDescr.LeLinha((int)novoPowerUp);

            PowerUp pw = new PowerUp();
            pw.tipo = novoPowerUp;
            pw.titulo = txt.Split(';')[0];
            pw.descricao = txt.Split(';')[1];

            inv.powerUps.Add(pw);
            int teste = powerUps.Count - 1;
            pnlDescricao = pnlDescricao.GetChild(teste);

            pnlDescricao.Find("titulo").GetComponentInChildren<Text>().text = pw.titulo;
            pnlDescricao.Find("conteudo").GetComponentInChildren<Text>().text = pw.descricao;

            pnlDescricao.gameObject.SetActive(pnlActive);

            TabuleiroHUD.FundoPowerUps(TabuleiroHUD.corOn, powerUps.Count - 1, i);
        }

        public void RemovePowerUp(int qtd, int i = -1)
        {
            if (powerUps.Count == 0)
                return;

            if (i < 0) i = GerenciadorPartida.Turno;
            Inventario inv = GerenciadorPartida.OrdemJogadores[i].GetComponent<Inventario>();

            Transform pnlDescricao = TabuleiroHUD.PnlsDescricoes[i];

            bool pnlActive = pnlDescricao.gameObject.activeSelf;
            pnlDescricao.gameObject.SetActive(true);

            pnlDescricao = pnlDescricao.GetChild(powerUps.Count - 1);

            if (qtd < inv.powerUps.Count)
            {
                for (int j = qtd; j >= 0; j--)
                {
                    inv.powerUps.RemoveAt(j);
                    pnlDescricao.Find("titulo").GetComponentInChildren<Text>().text = "";
                    pnlDescricao.Find("conteudo").GetComponentInChildren<Text>().text = "nenhum melhoramento";
                }

                TabuleiroHUD.FundoPowerUps(TabuleiroHUD.corOff, powerUps.Count - 1, i);
            }

            pnlDescricao.gameObject.SetActive(pnlActive);
        }

        public void AddObjeto(Objetos novoObj, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;
            Inventario inv = GerenciadorPartida.OrdemJogadores[i].GetComponent<Inventario>();

            Transform pnlObj = TabuleiroHUD.Paineis[i].Find("Painel Objetos");

            if (!inv.objetos.Contains(novoObj))
                inv.objetos.Add(novoObj);

            pnlObj = pnlObj.GetChild((int)novoObj);
            pnlObj.GetComponent<Image>().color = TabuleiroHUD.corOn;
        }

        public void RemoveObjeto(int qtd, int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;
            Inventario inv = GerenciadorPartida.OrdemJogadores[i].GetComponent<Inventario>();

            Transform pnlObj = TabuleiroHUD.Paineis[i].Find("Painel Objetos");

            if (qtd <= inv.objetos.Count)
            {
                for (int j = qtd; j >= 0; j--)
                {
                    Transform x = pnlObj.GetChild((int)inv.objetos[j]);
                    x.GetComponent<Image>().color = TabuleiroHUD.corOff;
                    inv.objetos.RemoveAt(j);
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
