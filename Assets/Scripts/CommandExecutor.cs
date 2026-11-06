using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class CommandExecutor : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject movePointObject;
    private PlayerController player;
    public Queue<string> commandsQueue = new Queue<string>();
    public int steps = 0;
    public TextMeshProUGUI stepsText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI commandWindow;
    [SerializeField] TMP_Text finalSteps;
    public GameObject winPanel;
    private Vector3 playerStartPos;
    public int gateTypes;
    public bool enableCommands = true;

    private class ObjectData
    {
        public GameObject targetObject;
        public Vector3 position;
        public Quaternion rotation;
        public Sprite sprite;
        public bool colliderEnabled;
        public Color color;
    }

    private Dictionary<string, List<ObjectData>> gateDataMap = new Dictionary<string, List<ObjectData>>();
    private List<ObjectData> buttonDataList = new List<ObjectData>();


    private void SaveObjectData(string tag, List<ObjectData> dataList)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            ObjectData data = new ObjectData();
            data.targetObject = obj;
            data.position = obj.transform.position;
            data.rotation = obj.transform.rotation;
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                data.sprite = spriteRenderer.sprite;
                data.color = spriteRenderer.color;
            }
            Collider2D collider = obj.GetComponent<Collider2D>();
            if (collider != null)
            {
                data.colliderEnabled = collider.enabled;
            }
            dataList.Add(data);
        }
    }

    private void ResetObjectData(string tag, List<ObjectData> dataList)
    {
        foreach (var data in dataList)
        {
            if (data.targetObject != null)
            {
                data.targetObject.transform.position = data.position;
                data.targetObject.transform.rotation = data.rotation;
                SpriteRenderer spriteRenderer = data.targetObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = data.sprite;
                    spriteRenderer.color = data.color;
                }
                Collider2D collider = data.targetObject.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = data.colliderEnabled;
                }
            }
        }
    }


    void Start()
    {
        player = playerObject.GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerController script is not attached to the playerObject!");
        }

        playerStartPos = playerObject.transform.position;

        for (int i = 0; i < gateTypes; i++)
        {
            string gate = "Gate" + i.ToString();
            Debug.Log(gate);

            List<ObjectData> gateDataList = new List<ObjectData>();
            SaveObjectData(gate, gateDataList);

            gateDataMap[gate] = gateDataList; // Store the list in the dictionary
        }

        buttonDataList = new List<ObjectData>();
        SaveObjectData("Button", buttonDataList);
        gateDataMap["Button"] = buttonDataList; // Store button data as well
    }

    public void AddCommand(string command)
    {
        if (enableCommands)
        {
            commandsQueue.Enqueue(command);
        }
    }

    public void ExecuteCommands()
    {
        if (enableCommands)
        {
            StartCoroutine(Execute());
            enableCommands = false;
        }
    }

    private IEnumerator Execute()
    {
        while (commandsQueue.Count > 0)
        {
            string command = commandsQueue.Dequeue();
            switch (command)
            {
                case "MoveRight()":
                    player.Move(PlayerController.Direction.RIGHT);
                    break;
                case "MoveLeft()":
                    player.Move(PlayerController.Direction.LEFT);
                    break;
                case "MoveUp()":
                    player.Move(PlayerController.Direction.UP);
                    break;
                case "MoveDown()":
                    player.Move(PlayerController.Direction.DOWN);
                    break;
            }
            steps++;
            stepsText.text = "Steps: " + steps;
            yield return new WaitUntil(() => player.isLoopFinished);
            player.isLoopFinished = false;
        }

        if (!player.HasReachedFinish)
        {
            ResetLevel();
        }

        enableCommands = true;
    }

    public void ShowWinPanel(double time)
    {
        finalSteps.text = "Steps: " + steps;
        double score = 1000 - (time * 5) - (steps * 20);
        finalScoreText.text = "Score: " + score;
        winPanel.SetActive(true);

        if (PlayerSession.HasName)
        {
            ScoreSaver.Save(PlayerSession.PlayerName, time, steps, score);
            PlayerSession.Clear();
        }
    }

    public void ResetLevel()
    {
        foreach (var gateEntry in gateDataMap)
        {
            string tag = gateEntry.Key;
            List<ObjectData> dataList = gateEntry.Value;

            Debug.Log(tag);
            ResetObjectData(tag, dataList);
        }
        commandsQueue.Clear();
        playerObject.transform.position = playerStartPos;
        movePointObject.transform.position = playerStartPos;
        steps = 0;
        stepsText.text = "Steps: " + steps;
        commandWindow.text = "";
        player.HasReachedFinish = false;
    }
}

