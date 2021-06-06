using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Color[] tileColors;
    public Color[] TileColors { get { return tileColors; } }

    [Header("Roller Info")]
    [SerializeField] private Transform rollerParent;
    [SerializeField] private GameObject rollerPrefab;
    [SerializeField] private int numberOfRollers;

    public int NumberOfRollers { get { return numberOfRollers; } }

    
    

    private RollerScript[] rollers;

    private int[] code;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rollers = new RollerScript[numberOfRollers];
        for (int i = 0; i < numberOfRollers; i++)
        {
            GameObject go = Instantiate(rollerPrefab, rollerParent);
            go.name = string.Format("RollerParent_{0:00}", i);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2((float)i / numberOfRollers, 0);
            rt.anchorMax = new Vector2((float)(i + 1) / numberOfRollers, 1);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rollers[i] = go.GetComponent<RollerScript>();
            rollers[i].Initialize(tileColors);
        }

        StatsManager.instance.InitiateTurns(12);

        code = new int[numberOfRollers];
        GenerateCode();
    }

    private void GenerateCode()
    {
        // Randomise Code
        for (int i = 0; i < code.Length; i++)
        {
            code[i] = Random.Range(0, tileColors.Length);
        }

        // Make sure it isn't already 
        bool allTrue = true;
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] != rollers[i].RollerIndex)
            {
                allTrue = false;
                break;
            }
        }
        if (allTrue)
            GenerateCode();
    }

    public void Check()
    {
        int color = 0; // Correct color, wrong place
        int place = 0; // Correct color, correct place;

        int[] colorsInCode = new int[tileColors.Length];
        int[] rollerCode = new int[numberOfRollers];
        for (int i = 0; i < numberOfRollers; i++)
        {
            rollerCode[i] = rollers[i].RollerIndex;
            colorsInCode[code[i]]++;
            colorsInCode[rollers[i].RollerIndex]--;
            if (code[i] == rollers[i].RollerIndex)
            {
                place++;
            }
        }

        for (int i = 0; i < colorsInCode.Length; i++)
        {
            color += Mathf.Max(colorsInCode[i], 0);
        }
        color = Mathf.Abs(color - numberOfRollers + place);

        TurnData data = new TurnData(rollerCode, color, place);
        StatsManager.instance.AddTurn(data);

        
    }


    public void ResetGame()
    {
        GenerateCode();
        for (int i = 0; i < numberOfRollers; i++)
        {
            rollers[i].ResetRoller();
        }
        StatsManager.instance.ResetTurns();
    }
}

public struct TurnData
{
    public int[] code;
    public int correctColor;
    public int correctPlace;
    

    public TurnData(int[] code, int correctColor, int correctPlace)
    {
        this.code = code;

        this.correctColor = correctColor;
        this.correctPlace = correctPlace;
        
    }
}
