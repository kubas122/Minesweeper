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
        if (isRevealed) return;

        if (isMine)
        {
            RevealMine();
            GridManager.Instance.GameOver();
        }
        else
        {
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
