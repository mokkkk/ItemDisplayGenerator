using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    // ノード生成用データの構造体
    public struct NodeGenerationData
    {
        public string jsonFilePath;
        public string partName;
        public int customModelData;
    }

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
        public Node GenerateNode()
        {
            return null;
        }
    }
}
