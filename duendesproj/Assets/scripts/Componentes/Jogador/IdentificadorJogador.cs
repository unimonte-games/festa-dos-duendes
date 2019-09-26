using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Identificadores;

namespace Componentes.Jogador {
    public class IdentificadorJogador : MonoBehaviour
    {
        public JogadorID jogadorID;

        void Start()
        {
            switch(Gerenciadores.GerenciadorGeral.qtdJogadores)
            {
                case 1: jogadorID = JogadorID.J1; break;
                case 2: jogadorID = JogadorID.J2; break;
                case 3: jogadorID = JogadorID.J3; break;
                case 4: jogadorID = JogadorID.J4; break;
            }
        }
    }
}
