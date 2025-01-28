using UnityEngine;
//Lets us set a value and image for a node
public class MapNode : MonoBehaviour
{
    [Range(1, 4)]
    [Tooltip("This corresponds to the role and img given eg 2 = boss.")]
    public int NodeValue;

    [Tooltip("The maximum value allowed for the node.")]
    public int NodeMax = 4;

    [Tooltip("The minimum value allowed for the node.")]
    public int NodeMin = 1;

    [Tooltip("The texture image of the node.")]
    public Texture2D Img;

    [Tooltip("Array of textures corresponding to different node values.")]
    public Texture2D[] NodeImages;

    public void RandomNode()
    {
        NodeValue = Random.Range(NodeMin, NodeMax);
        NodeImg();
    }
    public void SetNode(int node)
    {
        NodeValue = node; 
        NodeImg();
    }
    public void NodeImg()
    {
       Img=NodeImages[NodeValue];
    }
}
