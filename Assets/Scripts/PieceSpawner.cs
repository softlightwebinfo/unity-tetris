using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public GameObject[] levelPieces;
    public GameObject currentPiece, nextPiece;
    private void Start()
    {
        this.nextPiece = Instantiate(this.levelPieces[0], this.transform.position, Quaternion.identity);
        this.SpawnNextPiece();
    }

    public void SpawnNextPiece()
    {
        this.currentPiece = this.nextPiece;
        this.currentPiece.GetComponent<Piece>().enabled = true;

        foreach (SpriteRenderer child in this.currentPiece.GetComponentsInChildren<SpriteRenderer>())
        {
            Color currentColor = child.color;
            currentColor.a = 1.0f;
            child.color = currentColor;
        }

        StartCoroutine("PrepareNextPiece");
    }

    IEnumerator PrepareNextPiece()
    {
        yield return new WaitForSecondsRealtime(0.0f);

        int i = Random.Range(0, this.levelPieces.Length);
        nextPiece = Instantiate(this.levelPieces[i], this.transform.position, Quaternion.identity);
        nextPiece.GetComponent<Piece>().enabled = false;

        foreach (SpriteRenderer child in nextPiece.GetComponentsInChildren<SpriteRenderer>())
        {
            Color currentColor = child.color;
            currentColor.a = 0.3f;
            child.color = currentColor;
        }
    }
}
