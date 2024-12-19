using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int rewardPerTile = 1;
    public bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    public void CashOut()
    {
        if (isGameOver) return;

        isGameOver = true;
        UIManager.Instance.ShowResult($"Cash Out! Twoj wynik: {score}");
    }

    public void GameOver()
    {
        isGameOver = true;
        UIManager.Instance.ShowResult("Przegrales! Trafiles na mine.");
    }

    public void AddScore(int tilesRevealed)
    {
        if (isGameOver) return;

        score += tilesRevealed;
        Debug.Log("Wynik: " + score);
        UIManager.Instance.UpdateScoreText(score);
    }



}
