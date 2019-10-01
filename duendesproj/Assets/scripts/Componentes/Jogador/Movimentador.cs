using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador {
    public class Movimentador : MonoBehaviour
    {
        Transform tr;

        public float velocidade;
        public Vector3 direcao;
        public bool usarDeltaTime = true;

        void Awake ()
        {
            tr = GetComponent<Transform>();
        }

        void Update ()
        {
            float dt = usarDeltaTime ? Time.deltaTime : 1f;
            tr.Translate(direcao * velocidade * dt);
        }
    }
}
