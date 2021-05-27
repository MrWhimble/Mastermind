using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RollerScript[] rollers;

    // Start is called before the first frame update
    void Start()
    {
        rollers = new RollerScript[transform.childCount];
        for (int i = 0; i < rollers.Length; i++)
        {
            rollers[i] = transform.GetChild(i).GetComponent<RollerScript>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
