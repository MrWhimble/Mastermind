using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    private int maxNumberOfTurns;
    private int currentTurn;

    private TurnData[] turnDatas;

    [Header("Prefabs")]
    [SerializeField] private GameObject turnPanelPrefab;
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private GameObject numberPrefab;

    [Header("Sprites")]
    [SerializeField] private Sprite[] numberSprites;

    [Header("Parent")]
    [SerializeField] private RectTransform turnsParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void InitiateTurns(int maxTurns)
    {
        

        maxNumberOfTurns = maxTurns;
        currentTurn = 0;
        turnDatas = new TurnData[maxNumberOfTurns];
        
        for (int i = 0; i < maxNumberOfTurns; i++)
        {
            GameObject turnGO = Instantiate(turnPanelPrefab, turnsParent);
            turnGO.name = string.Format("TurnPanel_{0:00}", i);
            RectTransform turnRT = turnGO.GetComponent<RectTransform>();
            turnRT.anchorMin = new Vector2(0, 1f - ((float)(i + 1) / 12f));
            turnRT.anchorMax = new Vector2(1, 1f - ((float)i / 12f));
            turnRT.offsetMin = Vector2.zero;
            turnRT.offsetMax = Vector2.zero;
            RectTransform lightsParentRT = turnRT.GetChild(0).GetComponent<RectTransform>();
            for (int j = 0; j < GameManager.instance.NumberOfRollers; j++)
            {
                RectTransform lightRT = Instantiate(lightPrefab, lightsParentRT).GetComponent<RectTransform>();
                lightRT.anchorMin = new Vector2((float)j / GameManager.instance.NumberOfRollers + (0.5f / GameManager.instance.NumberOfRollers), 0.5f);
                lightRT.anchorMax = new Vector2((float)j / GameManager.instance.NumberOfRollers + (0.5f / GameManager.instance.NumberOfRollers), 0.5f);
            }
        }
    }

    public void AddTurn(TurnData data)
    {
        turnDatas[currentTurn] = data;

        Transform turnParent = turnsParent.GetChild(currentTurn);
        for (int i = 0; i < data.code.Length; i++)
        {
            turnParent.GetChild(0).GetChild(i).GetComponent<Image>().color = GameManager.instance.TileColors[data.code[i]];
        }

        currentTurn++;
    }

    public void ResetTurns()
    {
        currentTurn = 0;
        turnDatas = new TurnData[maxNumberOfTurns];
    }
}
