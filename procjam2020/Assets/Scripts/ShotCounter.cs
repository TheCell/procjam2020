using UnityEngine;

public class ShotCounter : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro uiScoreText;
    private uint hitCount;

    public void Start()
    {
        UpdateDisplay();
    }

    public void AddHit()
    {
        hitCount++;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        uiScoreText.text = $"Shots: {hitCount}";
    }
}
