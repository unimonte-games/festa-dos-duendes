using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototipos
{
    public class protoQuebraPontos : MonoBehaviour
    {
        Text txt;
        public Transform[] trs= new Transform[4];

        void Awake()
        {
            txt = GetComponent<Text>();
        }

        void Update()
        {
            txt.text = string.Concat(
                "J1: ", trs[0].position.z.ToString(), "\n",
                "J2: ", trs[1].position.z.ToString(), "\n",
                "J3: ", trs[2].position.z.ToString(), "\n",
                "J4: ", trs[3].position.z.ToString(), "\n"
            );
        }
    }
}
