using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerScript : MonoBehaviour
{
    public int rollerIndex { get; private set; }
    private int maxRollerIndex = 6;
    [SerializeField] private float rollerSpeed;

    private GameObject colorPrefab;
    private Transform[] colorTransforms;
    private float[] colorYPositions;

    // Start is called before the first frame update
    void Start()
    {
        colorTransforms = new Transform[transform.childCount];
        colorYPositions = new float[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) 
        {
            colorTransforms[i] = transform.GetChild(i);
            colorYPositions[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < colorTransforms.Length; i++)
        {
            colorYPositions[i] = Mathf.Lerp(colorYPositions[i], GetRollerIndexWithOffset(i), rollerSpeed * Time.deltaTime);
            colorTransforms[i].localPosition = new Vector2(colorTransforms[i].localPosition.x, colorYPositions[i]);
        }
    }

    public void ChangeRoller(int amount)
    {
        rollerIndex = GetRollerIndexWithOffset(amount);
    }

    private int GetRollerIndexWithOffset(int offset)
    {
        return (rollerIndex + offset) % maxRollerIndex;
    }
}
