using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject LayerPrefab;
    public MapLayer[] mapLayers;
    public int LayersForMap = 5;
    public float LayerSpacing = 20;
    private void Start()
    {
        
    }
    public void MapLayerInMap()
    {
       LayersForMap= Random.Range(0, 5);
        for (int i = 0; i < LayersForMap; i++)
        {
        }
    }
}


