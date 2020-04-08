using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    float lastFall = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!this.IsValidPiecePosition())
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.MovePieceHorizontally(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.MovePieceHorizontally(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.Rotate(0, 0, -90);
            if (this.IsValidPiecePosition())
            {
                this.UpdateGrid();
            }
            else
            {
                this.transform.Rotate(0, 0, 90);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || (Time.time - this.lastFall) > 1)
        {
            this.transform.position += new Vector3(0, -1, 0);
            if (IsValidPiecePosition())
            {
                this.UpdateGrid();
            }
            else
            {
                this.transform.position += new Vector3(0, 1, 0);
                // Como la pieza no puede bajar mas, a lo mejor ha llegado el momento de eliminar las filas
                GridHelper.DeleteAllFullRows();
                FindObjectOfType<PieceSpawner>().SpawnNextPiece();

                //Desabilitamos el script, para que esta pieza no se pueda mover
                this.enabled = false;
            }
            this.lastFall = Time.time;
        }
    }

    void MovePieceHorizontally(int direction)
    {
        //Muevo la pieza a la izquierda
        this.transform.position += new Vector3(direction, 0, 0);
        // Comprobamos si la nueva posición es válida
        if (this.IsValidPiecePosition())
        {
            // Persisto la informacion del movimiento en la parrilla del helper
            this.UpdateGrid();
        }
        else
        {
            // Si la posicion no es valida, revierto el movimiento
            this.transform.position += new Vector3(-direction, 0, 0);
        }
    }

    bool IsValidPiecePosition()
    {
        foreach (Transform block in this.transform)
        {
            // Posicion de cada uno de los hijos de la pieza
            Vector2 pos = GridHelper.RoundVector(block.position);
            // Si la posicion esta fuera de los limites, la posicion no es valida
            if (!GridHelper.IsInsideBorders(pos))
            {
                return false;
            }
            Transform possibleObject = GridHelper.grid[(int)pos.x, (int)pos.y];
            // Si ya hay otro bloque en esa misma posicion, tampoco es valida
            if (possibleObject && possibleObject.parent != this.transform)
            {
                return false;
            }
        }
        return true;
    }

    void UpdateGrid()
    {
        for (int y = 0; y < GridHelper.height; y++)
        {
            for (int x = 0; x < GridHelper.width; x++)
            {
                if (GridHelper.grid[x, y])
                {
                    if (GridHelper.grid[x, y].parent == this.transform)
                    {
                        GridHelper.grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform block in this.transform)
        {
            Vector2 pos = GridHelper.RoundVector(block.position);
            GridHelper.grid[(int)pos.x, (int)pos.y] = block;
        }
    }
}
