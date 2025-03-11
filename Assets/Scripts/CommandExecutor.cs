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
    private Queue<string> commandsQueue = new Queue<string>();
    public int steps = 0;
    public TextMeshProUGUI stepsText;
    public TextMeshProUGUI commandWindow;
    [SerializeField] TMP_Text finalSteps;
    public GameObject winPanel;
    private Vector3 playerStartPos;

    private class ObjectData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Sprite sprite;
        public bool colliderEnabled;
        public Color color;
    }

    private List<ObjectData> gateDataList = new List<ObjectData>();
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

        SaveObjectData("Gate", gateDataList);
        SaveObjectData("Gate1", gateDataList);
        SaveObjectData("Button", buttonDataList);
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

    private void ResetLevel()
    {
        ResetObjectData("Gate", gateDataList);
        ResetObjectData("Button", buttonDataList);
        playerObject.transform.position = playerStartPos;
        movePointObject.transform.position = playerStartPos;
        steps = 0;
        stepsText.text = "Steps: " + steps;
        commandWindow.text = "";
        commandsQueue.Clear();
    }
}

