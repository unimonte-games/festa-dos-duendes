using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador {
    public class IdentificadorJogador : MonoBehaviour
    {
        public Gerenciadores.JogadorID jogadorID;

        void Start()
        {
            switch(Gerenciadores.GerenciadorGeral.qtdJogadores)
            {
                case 1: jogadorID = Gerenciadores.JogadorID.J1; break;
                case 2: jogadorID = Gerenciadores.JogadorID.J2; break;
                case 3: jogadorID = Gerenciadores.JogadorID.J3; break;
                case 4: jogadorID = Gerenciadores.JogadorID.J4; break;
            }
        }
    }
}
