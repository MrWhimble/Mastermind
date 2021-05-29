using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollerScript : MonoBehaviour
{
    [SerializeField] private Transform tileParent;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileHeight;

    private int tileOffset;

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
        tileOffset = Mathf.FloorToInt(maxRollerIndex / 2);
        tileTransforms = new RectTransform[maxRollerIndex];
        tileYPositions = new float[maxRollerIndex];
        for (int i = 0; i < maxRollerIndex; i++) 
        {
            GameObject go = Instantiate(tilePrefab, tileParent);
            go.name = "Tile_" + string.Format("{0:00}", i);
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
            tileYPositions[i] = Mathf.Lerp(tileYPositions[i], GetTilePositionMultiplier(i) * tileHeight, rollerSpeed * Time.deltaTime);
            tileTransforms[i].localPosition = new Vector2(tileTransforms[i].localPosition.x, tileYPositions[i]);
        }
    }

    public void ChangeRoller(int amount)
    {
        
        if (amount > 0)        // Bottom Button
        {
            //int temp = GetTileIndexWithOffset(tileOffset - maxRollerIndex + 1);
            //tileYPositions[temp] = (GetTilePositionMultiplier(temp) + 1) * tileHeight;
            int tileToTeleport = GetTileIndexWithOffset(tileOffset);
            Debug.Log(tileToTeleport);
            tileYPositions[tileToTeleport] = -tileOffset * tileHeight;
        } else if (amount < 0) // Top Button
        {
            //int temp = GetTileIndexWithOffset(tileOffset - maxRollerIndex);
            //Debug.Log(rollerIndex + " | " + temp);
            //tileYPositions[temp] = maxRollerIndex * tileHeight;

            
        }
        rollerIndex = GetTileIndexWithOffset(amount);
        GameManager.instance.CheckRollers();
    }

    public int GetTilePositionMultiplier(int index)
    {
        return GetTileIndexWithOffset(index + tileOffset) - tileOffset;
    }

    private int GetTileIndexWithOffset(int offset)
    {
        return (rollerIndex + maxRollerIndex + offset) % maxRollerIndex;
    }
}
