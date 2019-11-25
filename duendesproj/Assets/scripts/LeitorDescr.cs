using UnityEngine;
using System.IO;
using System.Linq;

namespace Componentes.Tabuleiro
{
    public class LeitorDescr : MonoBehaviour
    {
        private static string path = @"./Assets/Info/PowerUpsDescr.txt";

        public static string LeLinha(int posi)
        {
            return File.ReadLines(path, System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(posi)
                .Take(1)
                .First();
        }
    }
}
