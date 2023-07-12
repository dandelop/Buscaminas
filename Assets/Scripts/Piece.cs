using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    public int x;
    public int y;
    public bool bomb;
    public TMP_Text Text;
    public bool tested = false;
    
    public void OnMouseDown()
    {   // Si es una bomba se pinta de rojo
        if (bomb)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else
        {   // Si no es una bomba se pinta de gris
            GetComponent<SpriteRenderer>().material.color = Color.grey;
            if (Generator.Gen.CheckBombsAround(x, y).ToString() != "0")
            {   // Y si es distinto de 0 se pone el número de bombas alrededor
                Text.text = Generator.Gen.CheckBombsAround(x, y).ToString();
            }
        }
        // Para que la pieza no se vuelva a comprobar
        tested = true;
    }
}
