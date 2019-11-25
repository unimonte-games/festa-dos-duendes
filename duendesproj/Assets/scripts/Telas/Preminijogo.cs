using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Identificadores;
using Gerenciadores;

namespace Telas
{
    public class Preminijogo : MonoBehaviour
    {
        public VideoClip vidQuebraBotao,
                         vidBaldeDasMacas,
                         vidPescaEscorrega,
                         vidCogumeloQuente,
                         vidFlautaHero;

        public Text descricao, controles, titulo;
        public VideoPlayer vPlayer;
        public static CenaID cenaMJ;

        void Start()
        {
            descricao.text = ObterTextoDescricao();
            controles.text = ObterTextoControles();
            titulo.text = ObterTextoTitulo();
            vPlayer.clip = ObterClipeVideo();
        }

        public void IniciarMJ()
        {
            GerenciadorGeral.TransitarParaMJ(cenaMJ);
        }

        string ObterTextoDescricao()
        {
            switch (cenaMJ)
            {
                case CenaID.QuebraBotao:
                    return string.Concat(
                        "Quem será o mais rápido?", "\n",
                        "Quem estiver mais próximo da garrafa de vidro, ",
                        "ganhará este minijogo, para isso, dê o máximo ",
                        "de passos possíveis!"
                    );
                case CenaID.BaldeDasMacas:
                    return string.Concat(
                        "Quem será o mais charmoso?", "\n",
                        "Quem tiver mais chapéus vestidos ganhará este ",
                        "minijogo, para isso, corra e pule atrás dos chapéus ",
                        "que estão caindo como chuva!"

                    );
                case CenaID.PescaEscorrega:
                    return string.Concat(
                        "Quem será o mais perfumado?", "\n",
                        "Quem tiver a maior colheita de flores ganhará ",
                        "este minijogo!"
                    );
                case CenaID.CogumeloQuente:
                    return string.Concat(
                        "Quem sobreviverá ao cogumelo quente?", "\n",
                        "Esse cogumelo tá pegando fogo bicho! Passe ",
                        "para o próximo jogador para sobreviver a ele!"
                    );
                case CenaID.FlautaHero:
                    return string.Concat(
                        "Quem terá o melhor ritmo?", "\n",
                        "Cate as flautas no ritmo mais preciso possível ",
                        "para ganhar este minijogo!"
                    );
            }

            return "";
        }

        string ObterTextoControles()
        {
            switch (cenaMJ)
            {
                case CenaID.QuebraBotao:
                    return string.Concat(
                        "Controles:", "\n", "\n",
                        "Use o <b>botão de ação</b> para dar um <b>passo</b>"
                    );
                case CenaID.BaldeDasMacas:
                    return string.Concat(
                        "Controles:", "\n", "\n",
                        "Movimentação é <b>horizontal</b>", "\n",
                        "Use o <b>botão de ação</b> para pular, entretanto ",
                        "não poderá mudar a direção do seu movimento ",
                        "enquanto estiver no ar."
                    );
                case CenaID.PescaEscorrega:
                    return string.Concat(
                        "Controles:", "\n", "\n",
                        "Movimentação é <b>horizontal</b> e <b>vertical</b>",
                        "\n",
                        "Use o <b>botão de ação</b> para colher uma flor."
                    );
                case CenaID.CogumeloQuente:
                    return string.Concat(
                        "Controles:", "\n", "\n",
                        "Use o <b>botão de ação</b> para passar o cogumelo ",
                        "quente para o próximo jogador."
                    );
                case CenaID.FlautaHero:
                    return string.Concat(
                        "Controles:", "\n", "\n",
                        "Use o <b>botão de ação</b> para catar a flauta, ",
                        "tente ao máximo catar a flauta no ritmo da música ",
                        "(ocorre quando a flauta está no personagem)."
                    );
            }

            return "";
        }

        string ObterTextoTitulo()
        {
            switch (cenaMJ)
            {
                case CenaID.QuebraBotao:    return "Corrida dos Passinhos";
                case CenaID.BaldeDasMacas:  return "Chuva de Chapéus";
                case CenaID.PescaEscorrega: return "Colheita das Flores";
                case CenaID.CogumeloQuente: return "Cogumelo Quente";
                case CenaID.FlautaHero:     return "Cata-Flauta";
            }

            return "";
        }

        VideoClip ObterClipeVideo()
        {
            switch (cenaMJ)
            {
                case CenaID.QuebraBotao:
                    return vidQuebraBotao;
                case CenaID.BaldeDasMacas:
                    return vidBaldeDasMacas;
                case CenaID.PescaEscorrega:
                    return vidPescaEscorrega;
                case CenaID.CogumeloQuente:
                    return vidCogumeloQuente;
                case CenaID.FlautaHero:
                    return vidFlautaHero;
            }

            return vPlayer.clip;
        }
    }
}
