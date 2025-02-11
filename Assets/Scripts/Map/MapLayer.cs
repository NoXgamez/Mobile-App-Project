using UnityEngine;

public class MapLayer : MonoBehaviour
{
    [Range(2, 4)]
    public int NodesForLayer;
    [Range(3, 4)]
    public int LayerCount;
    public float nodeSpacing = 2.0f;
    public GameObject nodePrefab; // Assign a prefab of MapNode in Inspector
    public MapNode[] mapNodes;
    private void Start()
    {
        MapNodesInLayer();
    }
    public void MapNodesInLayer()
    {
        NodesForLayer = Random.Range(2, 5); // 2, 3, or 4

        mapNodes = new MapNode[NodesForLayer]; // Initialize array

        for (int i = 0; i < NodesForLayer; i++)
        {
            Vector3 nodePosition = transform.position + new Vector3(i * nodeSpacing, 0, 0); // Space nodes horizontally

            GameObject nodeObj = Instantiate(nodePrefab, nodePosition, Quaternion.identity, transform); // Instantiate node
            MapNode node = nodeObj.GetComponent<MapNode>();

            if (i == 0 || i == NodesForLayer - 1) // First and last nodes
            {
                node.SetNode(0);
            }
            else
            {
                node.RandomNode();
            }

            mapNodes[i] = node; // Store in array
        }
    }
}
