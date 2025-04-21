using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public List<GameObject> CharactersStored = new List<GameObject>();

    // Save/load everything to/from a json if multiple scenes are used, otherwise it will just stay in the scene
}
