using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI commandWindow;
    public Button moveRightButton;
    public Button moveLeftButton;
    public Button jumpLeftButton;
    public Button jumpRightButton;
    public Button jumpUpButton;
    public Button waitButton;
    public Button startButton; // Dodaj referencję do przycisku "Start"
    public CommandExecutor commandExecutor;
    public GameObject playerPrefab; // Dodaj prefab gracza

    void Start()
    {
        moveRightButton.onClick.AddListener(() => AddCommand("MoveRight()"));
        moveLeftButton.onClick.AddListener(() => AddCommand("MoveLeft()"));
        jumpLeftButton.onClick.AddListener(() => AddCommand("JumpLeft()"));
        jumpRightButton.onClick.AddListener(() => AddCommand("JumpRight()"));
        jumpUpButton.onClick.AddListener(() => AddCommand("JumpUp()"));
        waitButton.onClick.AddListener(() => AddCommand("Wait()"));
        startButton.onClick.AddListener(() => commandExecutor.ExecuteCommands()); // Podłącz przycisk "Start" do metody ExecuteCommands()
        
        // Utwórz gracza
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    void AddCommand(string command)
    {
        commandWindow.text += command + "\n";
        commandExecutor.AddCommand(command);
    }
}
