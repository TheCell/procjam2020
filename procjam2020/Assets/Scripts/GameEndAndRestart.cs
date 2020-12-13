using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndAndRestart : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro finishText;
    [SerializeField]
    private ShotCounter shotCounter;
    private bool finishedGame = false;

    public void Start()
    {
        finishText.enabled = false;
    }

    public void FinishGame()
    {
        finishText.enabled = true;
        finishText.text = $"You finished the course in {shotCounter.HitCount} shots (restart by pressing R)";
        finishedGame = true;
    }

    public void NewGame()
    {
        if (finishedGame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
