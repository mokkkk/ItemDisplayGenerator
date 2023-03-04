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
            var nodeName = data.partName;

            // ノード名被り対策
            foreach (Node n in nodeList)
            {
                if (nodeName.Equals(n.nodeName))
                {
                    int i = 1;
                    foreach (Node n_ in nodeList)
                    {
                        var newNodeName = nodeName + "_" + i.ToString();
                        if (newNodeName.Equals(n_.nodeName))
                            i++;
                    }

                    nodeName = nodeName + "_" + i.ToString();
                    data.partName = nodeName;
                }
            }

            // ID決定
            var id = nodeList.Count;

            // ノード生成
            Node newNode = nodeGenerator.GenerateNode(data, id);
            AddNode(newNode);
        }

        // ノードの追加
        public void AddNode(Node node)
        {
            this.nodeList.Add(node);
        }
    }
}
