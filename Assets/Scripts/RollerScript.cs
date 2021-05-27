using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollerScript : MonoBehaviour
{
    [SerializeField] private Transform tileParent;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileHeight;

    private int rollerIndex;
    public int RollerIndex { 
        get { 
            return rollerIndex; 
        }
    }

    private int maxRollerIndex = 6;
    [SerializeField] private float rollerSpeed;

    private GameObject colorPrefab;
    private RectTransform[] tileTransforms;
    private float[] tileYPositions;

    // Start is called before the first frame update
    public void Initialize(Color[] colors)
    {
        maxRollerIndex = colors.Length;
        tileTransforms = new RectTransform[maxRollerIndex];
        tileYPositions = new float[maxRollerIndex];
        for (int i = 0; i < maxRollerIndex; i++) 
        {
            GameObject go = Instantiate(tilePrefab, tileParent);
            tileTransforms[i] = go.GetComponent<RectTransform>();
            tileTransforms[i].GetChild(0).GetComponent<Image>().color = colors[i];
            tileYPositions[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tileTransforms.Length; i++)
        {
            tileYPositions[i] = Mathf.Lerp(tileYPositions[i], (GetRollerIndexWithOffset(i - 3) + 2) * tileHeight, rollerSpeed * Time.deltaTime);
            tileTransforms[i].localPosition = new Vector2(tileTransforms[i].localPosition.x, tileYPositions[i]);
        }
    }

    public void ChangeRoller(int amount)
    {
        rollerIndex = GetRollerIndexWithOffset(amount);
        GameManager.instance.CheckRollers();
    }

    private int GetRollerIndexWithOffset(int offset)
    {
        return (rollerIndex + offset) % maxRollerIndex;
    }
}
