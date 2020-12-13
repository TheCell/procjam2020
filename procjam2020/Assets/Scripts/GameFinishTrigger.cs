using UnityEngine;

public class GameFinishTrigger : MonoBehaviour
{
    private GameEndAndRestart gameEndAndRestart;

    public void Start()
    {
        gameEndAndRestart = FindObjectOfType<GameEndAndRestart>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameEndAndRestart.FinishGame();
    }
}
