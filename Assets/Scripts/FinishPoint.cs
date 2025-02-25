using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("Fein");
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HasReachedFinish = true;
                CommandExecutor commandExecutor = FindObjectOfType<CommandExecutor>();
                commandExecutor.ShowWinPanel();
            }
        }
    }
}
