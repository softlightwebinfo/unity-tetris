using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public GameObject[] levelPieces;

    private void Start()
    {
        this.SpawnNextPiece();
    }

    public void SpawnNextPiece()
    {
        int i = Random.Range(0, this.levelPieces.Length);
        Instantiate(this.levelPieces[i], this.transform.position, Quaternion.identity);
    }
}
