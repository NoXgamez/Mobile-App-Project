using UnityEngine;

public class MapScript : MonoBehaviour
{
    [Range(2, 4)]
    public int NodesForLayer;
    [Range(3, 4)]
    public int LayerCount;
    public int LayerMax = 4;
    public int LayerMin = 3;
    [Range(1, 4)]
    public int NodeValue;
    public int NodeMax = 4;
    public int NodeMin = 1;

    public void CreateLayers()
    {
        LayerCount = Random.Range(LayerMin, LayerMax);
    }
    public void Nodes()
    {
        NodeValue = Random.Range(NodeMin, NodeMax);
    }

}


