using UnityEngine;

public class MapLayer : MonoBehaviour
{
    [Range(2, 4)]
    public int NodesForLayer;
    [Range(3, 4)]
    public int LayerCount;
    //public int LayerMax = 4;
    //public int LayerMin = 3;

    public MapNode[] mapNodes;
    //public void CreateLayers()
    //{
    //    LayerCount = Random.Range(LayerMin, LayerMax);
    //}
    public void MapNodesInLayer()
    {
        NodesForLayer = Random.Range(2, 4);
        for (int i = 0; i < NodesForLayer; i++) 
        {
            if (i == 0 || i==NodesForLayer)
            {
                mapNodes[i].SetNode(0);
            }
        mapNodes[i] = new MapNode();
        mapNodes[i].RandomNode();
        }
    }
}
