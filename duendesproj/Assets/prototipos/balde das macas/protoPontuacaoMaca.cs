using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototipos
{
    public class protoPontuacaoMaca : MonoBehaviour
    {
        Text txt;
        public protoBaldeColetaMacas[] baldes = new protoBaldeColetaMacas[4];

        void Awake()
        {
            txt = GetComponent<Text>();
        }

        void Update()
        {
            txt.text = string.Concat(
                "J1: ", baldes[0].GetMacasColetadas().ToString(), "\n",
                "J2: ", baldes[1].GetMacasColetadas().ToString(), "\n",
                "J3: ", baldes[2].GetMacasColetadas().ToString(), "\n",
                "J4: ", baldes[3].GetMacasColetadas().ToString(), "\n"
            );
        }
    }
}
