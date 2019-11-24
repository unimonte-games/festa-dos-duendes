using UnityEngine;
using System.IO;
using System.Linq;

public class LeitorDescr : MonoBehaviour
{
    private static string path = @"./Assets/Info/PowerUpsDescr.txt";

    public static string LeLinha(int posi)
    {
        return File.ReadLines(path).Skip(posi).Take(1).First();
    }
}
