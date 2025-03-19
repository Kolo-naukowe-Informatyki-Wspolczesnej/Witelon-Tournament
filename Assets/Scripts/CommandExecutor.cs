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
    public TextMeshProUGUI commandWindow;
    [SerializeField] TMP_Text finalSteps;
    public GameObject winPanel;
    private Vector3 playerStartPos;
    public int gateTypes;

    private class ObjectData
    {
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
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < objects.Length; i++)
        {
            if (i < dataList.Count)
            {
                objects[i].transform.position = dataList[i].position;
                objects[i].transform.rotation = dataList[i].rotation;
                SpriteRenderer spriteRenderer = objects[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = dataList[i].sprite;
                    spriteRenderer.color = dataList[i].color;
                }
                Collider2D collider = objects[i].GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = dataList[i].colliderEnabled;
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

        List<ObjectData> buttonDataList = new List<ObjectData>();
        SaveObjectData("Button", buttonDataList);
        gateDataMap["Button"] = buttonDataList; // Store button data as well
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
    }

    public void ShowWinPanel()
    {
        finalSteps.text = "Steps: " + steps;
        winPanel.SetActive(true);
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
        playerObject.transform.position = playerStartPos;
        movePointObject.transform.position = playerStartPos;
        steps = 0;
        stepsText.text = "Steps: " + steps;
        commandWindow.text = "";
        commandsQueue.Clear();
        player.HasReachedFinish = false;
    }
}

