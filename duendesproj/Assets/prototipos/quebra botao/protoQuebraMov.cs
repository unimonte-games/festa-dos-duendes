using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipos
{
    public class protoQuebraMov : MonoBehaviour
    {
        public string teclaMov;
        public float velocidade;

        Transform tr;

        void Awake()
        {
            tr = GetComponent<Transform>();
        }

        void Update()
        {
            if (Input.GetKeyDown(teclaMov))
            {
                Vector3 pos = tr.position;
                pos.z = pos.z + velocidade;
                tr.position = pos;
            }
        }
    }
}
