using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class BoardManager : MonoBehaviour
    {
    [SerializeField]
    GameObject[] Nodes;

    public GameObject getNodeWithRank(int i)
    {
        return Nodes[i];

    }

    public GameObject[] getAllNodes(){
        return Nodes;
    }


    }
}