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
        private List<Node> nodeList;

        public void Start()
        {
            // Component取得
            nodeGenerator = this.GetComponent<NodeGenerator>();
        }

        // ノード生成
        public Node GenerateNode()
        {
            return nodeGenerator.GenerateNode();
        }

        // ノードの追加
        public void AddNode(Node node)
        {
            this.nodeList.Add(node);
        }
    }
}
