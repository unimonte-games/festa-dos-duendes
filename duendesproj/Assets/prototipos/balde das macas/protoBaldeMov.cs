using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipos
{
    public class protoBaldeMov : MonoBehaviour
    {
        public float velocidade, limX;
        public string teclaEsq, teclaDir;
        Transform tr;

        void Awake()
        {
            tr = GetComponent<Transform>();
        }

        void Update()
        {
            Vector3 pos = tr.position;
            float dirX = ObtemMov();
            pos.x = Mathf.Clamp(
                pos.x + velocidade * dirX * Time.deltaTime, -limX, limX
            );
            tr.position = pos;
        }

        float ObtemMov()
        {
            return (Input.GetKey(teclaEsq)) ? (
                -1
            ):(
                    Input.GetKey(teclaDir) ? 1 : 0
            );
        }
    }
}
