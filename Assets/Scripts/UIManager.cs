using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text scoreText;
    public TMP_Text resultText;
    public GameObject resultPanel;

    void Awake()
    {
        Instance = this;
    }

    public void ShowResult(string message)
    {
        resultPanel.SetActive(true);
        resultText.text = message;
    }

    public void UpdateScoreText(int score)
    {
        Debug.Log("Aktualizacja UI " + score);
        scoreText.text = $"Wynik: {score}";
    }

}
