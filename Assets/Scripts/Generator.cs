using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject piece;
    public int width;
    public int heigth;
    public int bombsNumber;
    private GameObject[][] _map;
    public static Generator Gen;


    void Start()
    {
        // Singleton
        Gen = this;

        // Ajustamos la cámara al centro de la malla
        Camera.main.transform.position = new Vector3(((float)width / 2) - 0.5f, ((float)heigth / 2) - 0.5f, -10);

        // Creamos las filas del array2D 
        _map = new GameObject[width][];

        // Recorremos las filas y creamos una columna para cada una
        for (int i = 0; i < _map.Length; i++)
        {
            _map[i] = new GameObject[heigth];
        }

        // Colocamos una pieza en cada posición
        for (int i = 0; i < width; i++) // X
        {
            for (int j = 0; j < heigth; j++) // Y
            {
                _map[i][j] = Instantiate(piece, new Vector2(i, j), Quaternion.identity);
                _map[i][j].GetComponent<Piece>().x = i;
                _map[i][j].GetComponent<Piece>().y = j;
            }
        }

        // Colocamos tantas bombas como hayamos escogido
        for (int i = 0; i < bombsNumber; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, heigth);
            if (!_map[x][y].GetComponent<Piece>().bomb)
            {
                _map[x][y].GetComponent<Piece>().bomb = true;
            }
            else
            {
                i--;
            }
        }
    }

    // Comprobamos si hay bombas alrededor de la pieza y si el numero de bombas es cero comprobamos las piezas de alrededor de forma recursiva
    public int CheckBombsAround(int x, int y)
    {
        int cont = 0;

        for (int i = x - 1; i <= x + 1; i++) // X
        {
            for (int j = y - 1; j <= y + 1; j++) // Y
            {
                try
                {
                    if (_map[i][j].GetComponent<Piece>().bomb && !_map[i][j].GetComponent<Piece>().tested)
                    {
                        cont++;
                    }
                }
                catch (System.Exception) { }
            }
        }
        if (cont == 0) // Si no hay bombas alrededor repetimos el proceso para las piezas adyacentes
        {
            for (int i = x - 1; i <= x + 1; i++) // X
            {
                for (int j = y - 1; j <= y + 1; j++) // Y
                {
                    try
                    {
                        if (!_map[i][j].GetComponent<Piece>().bomb && !_map[i][j].GetComponent<Piece>().tested)
                        {
                            _map[i][j].GetComponent<Piece>().OnMouseDown();
                        }
                    }
                    catch (System.Exception) { }
                }
            }
        }
        return cont;
    }
}
