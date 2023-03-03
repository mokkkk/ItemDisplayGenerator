using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    /**
    * ノードのGameObjectを製造する．
    */
    public class NodeGenerator : MonoBehaviour
    {

        // ノード製作用Prefab
        [SerializeField]
        private GameObject dummyNodePrefab;
        [SerializeField]
        private GameObject pivotCubePrefab;

        // 製造中のノード
        private GameObject nodeObject;

        // ノード生成
        public Node GenerateNode(NodeGenerationData data)
        {
            Debug.Log(this + ":ノード生成");
            return null;
        }
    }
}
