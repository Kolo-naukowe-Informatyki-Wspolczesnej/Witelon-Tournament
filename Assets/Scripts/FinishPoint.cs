using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HasReachedFinish = true;
                CommandExecutor commandExecutor = FindObjectOfType<CommandExecutor>();
                Timer timer = FindObjectOfType<Timer>();
                timer.StopTimer();
                commandExecutor.ShowWinPanel();
            }
        }
    }
}
