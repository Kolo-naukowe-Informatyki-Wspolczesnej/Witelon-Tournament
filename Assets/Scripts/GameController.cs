using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI commandWindow;
    public Button moveRightButton;
    public Button moveLeftButton;
    public Button moveUpButton;
    public Button moveDownButton;
    public Button startButton;
    public CommandExecutor commandExecutor;

    void Start()
    {
        moveRightButton.onClick.AddListener(() => AddCommand("MoveRight()"));
        moveLeftButton.onClick.AddListener(() => AddCommand("MoveLeft()"));
        moveUpButton.onClick.AddListener(() => AddCommand("MoveUp()"));
        moveDownButton.onClick.AddListener(() => AddCommand("MoveDown()"));
        startButton.onClick.AddListener(() => commandExecutor.ExecuteCommands());
        Timer timer = FindObjectOfType<Timer>();
        timer.StartTimer();
    }

    void AddCommand(string command)
    {
        commandWindow.text += command + "\n";
        commandExecutor.AddCommand(command);
    }
}
