using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform rollerParent;
    [SerializeField] private GameObject rollerPrefab;
    [SerializeField] private int numberOfRollers;
    [SerializeField] private Color[] tileColors;

    private RollerScript[] rollers;

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
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2((float)i / numberOfRollers, 0);
            rt.anchorMax = new Vector2((float)(i + 1) / numberOfRollers, 1);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rollers[i] = go.GetComponent<RollerScript>();
            rollers[i].Initialize(tileColors);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckRollers()
    {

    }
}
