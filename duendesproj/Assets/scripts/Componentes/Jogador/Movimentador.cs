using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador {
    public class Movimentador : MonoBehaviour
    {
        Transform tr;

        /// <summary>
        /// Velocidade de movimento; se é por sefundo ou por quadro
        /// de jogo depende do usarDeltaTime.
        /// </summary>
        public float velocidade;

        /// <summary>
        /// Direção do movimento; deve se tomar o cuidado
        /// para deixar a magnitude = 1.
        /// </summary>
        public Vector3 direcao;

        /// <summary>
        /// Define se a velocidade de movimento é por quadro ou por segundo,
        /// por padrão é verdadeiro (por segundo)
        /// </summary>
        public bool usarDeltaTime = true;

        /// <summary>
        /// Se a movimentação deve usar os eixos locais ou globais
        /// </summary>
        public bool relativoAoGlobal;

        void Awake ()
        {
            tr = GetComponent<Transform>();
        }

        void Update ()
        {
            float dt = usarDeltaTime ? Time.deltaTime : 1f;
            tr.Translate(
                direcao * velocidade * dt,
                relativoAoGlobal ? Space.World : Space.Self
            );
        }
    }
}
