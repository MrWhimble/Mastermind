using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Color[] tileColors;

    [Header("Roller Info")]
    [SerializeField] private Transform rollerParent;
    [SerializeField] private GameObject rollerPrefab;
    [SerializeField] private int numberOfRollers;
    

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
            go.name = "RollerParent_" + string.Format("{0:00}", i);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2((float)i / numberOfRollers, 0);
            rt.anchorMax = new Vector2((float)(i + 1) / numberOfRollers, 1);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rollers[i] = go.GetComponent<RollerScript>();
            rollers[i].Initialize(tileColors);
        }

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

    public void UpdateUI()
    {
        
    }
}
