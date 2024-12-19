using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row;
    public int column;
    public bool isMine = false;
    public bool isRevealed = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.gray;
    }

    void OnMouseDown()
    {
        Debug.Log($"Kliknieto pole [{row}, {column}]");

        if (isRevealed) return;

        if (isMine)
        {
            Debug.Log("Trafiono mine!");
            RevealMine();
            GridManager.Instance.GameOver();
        }
        else
        {
            Debug.Log("Odslonieto pole.");
            Reveal();
            GridManager.Instance.RevealTile(row, column);
        }
    }



    public void Reveal()
    {
        isRevealed = true;
        spriteRenderer.color = Color.white;
    }

    public void RevealMine()
    {
        isRevealed = true;
        spriteRenderer.color = Color.red;
    }
}
