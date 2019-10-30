using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Identificadores;

namespace Componentes.Jogador {
    public class IdentificadorJogador : MonoBehaviour
    {
        static int id;

        /// <summary>A qual jogador esse personagem pertence.</summary>
        public JogadorID jogadorID;

        void Start()
        {
            switch(id)
            {
                case 0: jogadorID = JogadorID.J1; break;
                case 1: jogadorID = JogadorID.J2; break;
                case 2: jogadorID = JogadorID.J3; break;
                case 3: jogadorID = JogadorID.J4; break;
            }

            id++;
        }
    }
}
