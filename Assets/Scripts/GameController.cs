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
    public Button startButton; // Dodaj referencję do przycisku "Start"
    public CommandExecutor commandExecutor;
    public GameObject playerPrefab; // Dodaj prefab gracza

    void Start()
    {
        moveRightButton.onClick.AddListener(() => AddCommand("MoveRight()"));
        moveLeftButton.onClick.AddListener(() => AddCommand("MoveLeft()"));
        moveUpButton.onClick.AddListener(() => AddCommand("MoveUp()"));
        moveDownButton.onClick.AddListener(() => AddCommand("MoveDown()"));
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
