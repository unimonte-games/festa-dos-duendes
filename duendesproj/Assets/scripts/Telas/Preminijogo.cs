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
                        "Descrição para QuebraBotao"
                    );
                case CenaID.BaldeDasMacas:
                    return string.Concat(
                        "Descrição para BaldeDasMacas"
                    );
                case CenaID.PescaEscorrega:
                    return string.Concat(
                        "Descrição para PescaEscorrega"
                    );
                case CenaID.CogumeloQuente:
                    return string.Concat(
                        "Descrição para CogumeloQuente"
                    );
                case CenaID.FlautaHero:
                    return string.Concat(
                        "Descrição para FlautaHero"
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
                        "Controles para QuebraBotao"
                    );
                case CenaID.BaldeDasMacas:
                    return string.Concat(
                        "Controles para BaldeDasMacas"
                    );
                case CenaID.PescaEscorrega:
                    return string.Concat(
                        "Controles para PescaEscorrega"
                    );
                case CenaID.CogumeloQuente:
                    return string.Concat(
                        "Controles para CogumeloQuente"
                    );
                case CenaID.FlautaHero:
                    return string.Concat(
                        "Controles para FlautaHero"
                    );
            }

            return "";
        }

        string ObterTextoTitulo()
        {
            switch (cenaMJ)
            {
                case CenaID.QuebraBotao:    return "Corrida dos Passinhos";
                case CenaID.BaldeDasMacas:  return "Veste Chapéu";
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
