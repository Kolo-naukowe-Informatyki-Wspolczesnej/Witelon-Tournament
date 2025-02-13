using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandExecutor : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerController player;
    private Queue<string> commandsQueue = new Queue<string>();
    public int steps = 0;
    public TextMeshProUGUI stepsText;
    public TextMeshProUGUI commandWindow;
    public GameObject winPanel;
    // Usuń: public GameObject losePanel;
    private Vector3 playerStartPos;

    void Start()
    {
        player = playerObject.GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerController script is not attached to the playerObject!");
        }

        playerStartPos = playerObject.transform.position;
    }

    public void AddCommand(string command)
    {
        commandsQueue.Enqueue(command);
    }

    public void ExecuteCommands()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        while (commandsQueue.Count > 0)
        {
            string command = commandsQueue.Dequeue();
            switch (command)
            {
                case "MoveRight()":
                    player.MoveRight();
                    break;
                case "MoveLeft()":
                    player.MoveLeft();
                    break;
                case "JumpLeft()":
                    player.JumpLeft();
                    break;
                case "JumpRight()":
                    player.JumpRight();
                    break;
                case "JumpUp()":
                    player.JumpUp();
                    break;
                case "Wait()":
                    player.Wait();
                    break;
            }
            steps++;
            stepsText.text = "Steps: " + steps;
            yield return new WaitForSeconds(1); // Czas pomiędzy ruchami
        }

        // Sprawdzamy, czy gracz dotarł do obiektu wygranej
        if (!player.HasReachedFinish)
        {
            ResetLevel();
        }
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    private void ResetLevel()
    {
        playerObject.transform.position = playerStartPos;
        steps = 0;
        stepsText.text = "Kroki: " + steps;
        commandWindow.text = "";
        commandsQueue.Clear();
    }
}
