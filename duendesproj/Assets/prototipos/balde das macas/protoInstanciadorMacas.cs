using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipos
{
    public class protoInstanciadorMacas : MonoBehaviour
    {
        public float intervalo = 0f;
        public float limX = 0f;
        public GameObject macaGbj;

        float t1 = 0f;
        Transform tr;

        void Awake()
        {
            tr = GetComponent<Transform>();
        }

        void Start()
        {
            t1 = Time.time;
        }

        void Update()
        {
            float t2 = Time.time;
            float diffT = t2 - t1;

            if (diffT > intervalo)
            {
                t1 = t2;
                GameObject novaMaca = Instantiate<GameObject>(
                    macaGbj,
                    new Vector3(
                        Random.Range(-limX, limX), tr.position.y, tr.position.z)
                    ,
                    tr.rotation
                );
                novaMaca.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
