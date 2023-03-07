using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    /**
    * 複数ノードを管理する．ノードの値の変更は必ずNodeManager経由で行う．
    */
    public class NodeManager : SingletonMonoBehaviour<NodeManager>
    {
        // ノード生成用
        private NodeGenerator nodeGenerator;
        private NodeUIGenerator nodeUIGenerator;

        // ノードリスト
        [SerializeField]
        private List<Node> nodeList;

        public void Start()
        {
            // Component取得
            nodeGenerator = this.GetComponent<NodeGenerator>();
            nodeUIGenerator = GameObject.Find("Canvas").transform.Find("ModeModel").Find("NodeSettingUI").GetComponent<NodeUIGenerator>();

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
            Node newNode = nodeGenerator.GenerateNode(data);
            newNode.Initialize(data, id);

            // リストに追加
            AddNode(newNode);

            // UI生成
            nodeUIGenerator.GenerateUI(newNode);
        }

        // ノードの追加
        public void AddNode(Node node)
        {
            if (!ReferenceEquals(node.transform.parent, this.transform))
                node.transform.parent = this.transform;
            this.nodeList.Add(node);
        }

        // ノードのPosition設定
        public void SetNodePosition(Vector3 position, int nodeId)
        {
            foreach (Node n in nodeList)
            {
                if (n.nodeId == nodeId)
                    n.pos = position;
            }
            UpdateNodeTransform();
        }

        // ノードのRotation設定
        public void SetNodeRotation(Vector3 rotation, int nodeId)
        {
            foreach (Node n in nodeList)
            {
                if (n.nodeId == nodeId)
                    n.rotate = rotation;
            }
            UpdateNodeTransform();
        }

        // ノードのScale設定
        public void SetNodeScale(float scale, int nodeId)
        {
            foreach (Node n in nodeList)
            {
                if (n.nodeId == nodeId)
                    n.scale = scale;
            }
            UpdateNodeTransform();
        }

        // ノードの位置関係更新
        private void UpdateNodeTransform()
        {
            Debug.Log(this + ":UpdateNodeTransform");
        }
    }
}
