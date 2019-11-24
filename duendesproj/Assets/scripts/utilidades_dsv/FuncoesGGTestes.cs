using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Identificadores;
using Gerenciadores;

namespace DsvUtils
{
    public class FuncoesGGTestes : MonoBehaviour
    {
        public UnityEngine.UI.Text txtEstadoDeJogo;
        public UnityEngine.UI.Button btCadastrarJogador;
        public UnityEngine.UI.Button btDecadastrarJogador;

        string estadoJogo_fmt = "";

        void Start()
        {
            AtualizaVisualEstadoDeJogo();
        }

        void AtualizaVisualEstadoDeJogo()
        {
            if (estadoJogo_fmt == "")
                estadoJogo_fmt = txtEstadoDeJogo.text;

            string estadoDeJogo_str  = string.Format(
                estadoJogo_fmt,
                GerenciadorGeral.qtdJogadores,
                GerenciadorGeral.pontuacao[0],
                GerenciadorGeral.pontuacao[1],
                GerenciadorGeral.pontuacao[2],
                GerenciadorGeral.pontuacao[3]
            );

            txtEstadoDeJogo.text = estadoDeJogo_str;

            btCadastrarJogador.interactable = GerenciadorGeral.PodeCadastrar();
            btDecadastrarJogador.interactable = GerenciadorGeral.PodeDecadastrar();
        }

#region abrir minijogos

        public void AbrirMJ_QuebraBotao()    { AbrirMJ(CenaID.QuebraBotao); }
        public void AbrirMJ_BaldeDasMacas()  { AbrirMJ(CenaID.BaldeDasMacas); }
        public void AbrirMJ_PescaEscorrega() { AbrirMJ(CenaID.PescaEscorrega); }
        public void AbrirMJ_CogumeloQuente() { AbrirMJ(CenaID.CogumeloQuente); }
        public void AbrirMJ_FlautaHero() { AbrirMJ(CenaID.FlautaHero); }

        void AbrirMJ(CenaID cenaId)
        {
            GerenciadorGeral.TransitarParaMJ(cenaId);
        }

#endregion abrir minijogos

        public void CadastrarJogador()
        {
            GerenciadorGeral.CadastrarJogador();
            AtualizaVisualEstadoDeJogo();
        }

        public void DecadastrarJogador()
        {
            GerenciadorGeral.DecadastrarJogador();
            AtualizaVisualEstadoDeJogo();
        }

    }
}
