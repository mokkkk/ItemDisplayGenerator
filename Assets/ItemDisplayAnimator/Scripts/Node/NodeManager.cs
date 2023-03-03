using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    /**
    * 複数ノードを管理する．ノードの値の変更は必ずNodeManager経由で行う．
    */
    public class NodeManager : MonoBehaviour
    {
        // ノード生成用
        private NodeGenerator nodeGenerator;

        // ノード
        [SerializeField]
        private List<Node> nodeList;

        public void Start()
        {
            // Component取得
            nodeGenerator = this.GetComponent<NodeGenerator>();

            // List初期化
            nodeList = new List<Node>();
        }

        // ノード生成
        public void GenerateNode(NodeGenerationData data)
        {
            Node newNode = nodeGenerator.GenerateNode(data);
            AddNode(newNode);
        }

        // ノードの追加
        public void AddNode(Node node)
        {
            this.nodeList.Add(node);
        }
    }
}
