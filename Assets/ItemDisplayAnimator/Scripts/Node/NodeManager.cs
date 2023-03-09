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
        private NodeUIManager nodeUIManager;

        // ノードリスト
        [SerializeField]
        private List<Node> nodeList;

        // Parent設定用
        [SerializeField]
        private SelectParentUI selectParentUI;
        private Node parentSelectingNode;

        public void Start()
        {
            // Component取得
            nodeGenerator = this.GetComponent<NodeGenerator>();
            nodeUIManager = GameObject.Find("Canvas").transform.Find("ModeModel").Find("NodeSettingUI").GetComponent<NodeUIManager>();

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
            nodeUIManager.GenerateUI(newNode);
        }

        // ノードの追加
        public void AddNode(Node node)
        {
            if (!ReferenceEquals(node.transform.parent, this.transform))
                node.transform.parent = this.transform;
            this.nodeList.Add(node);
        }

        // ノードのParent設定開始
        public void SelectNodeParentStart(int nodeId)
        {
            // ノード保持
            foreach (Node n in nodeList)
            {
                if (n.nodeId == nodeId)
                    parentSelectingNode = n;
            }

            // UI表示
            selectParentUI.gameObject.SetActive(true);
            selectParentUI.Initialize(nodeList, nodeId);
        }

        // ノードのParent設定完了
        public void SelectNodeParentComplete(int parentNodeId)
        {
            // 対象ノードの親を設定
            Node tmpParentNode = null;
            if (parentNodeId > -1)
            {
                foreach (Node n in nodeList)
                {
                    if (n.nodeId == parentNodeId)
                    {
                        tmpParentNode = n;
                        parentSelectingNode.parentNode = tmpParentNode;
                        parentSelectingNode.parentNodeId = tmpParentNode.nodeId;
                    }
                }
            }
            else
            {
                parentSelectingNode.parentNode = null;
                parentSelectingNode.parentNodeId = -1;
                parentSelectingNode.nodeType = NodeType.Root;
            }

            // UI更新
            nodeUIManager.UpdateParentNode(parentSelectingNode.nodeId, tmpParentNode);

            // ノード解放
            parentSelectingNode = null;

            // ノードTransform更新
            UpdateNodeRelation();
            UpdateNodeTransform(Vector3.zero, Vector3.zero);
        }

        // ノードのParent設定キャンセル
        public void SelectNodeParentCancel()
        {
            // ノード解放
            parentSelectingNode = null;
        }

        // ノードの親子関係更新
        private void UpdateNodeRelation()
        {
            // childNodeListリセット
            foreach (Node n in nodeList)
                n.childNodeList = new List<Node>();

            // parentNodeのchildNodeListに自分を追加
            foreach (Node n in nodeList)
            {
                if (!ReferenceEquals(n.parentNode, null))
                    n.parentNode.childNodeList.Add(n);
            }

            // NodeType更新
            foreach (Node n in nodeList)
            {
                if (!ReferenceEquals(n.parentNode, null))
                {
                    if (0 < n.childNodeList.Count)
                        n.nodeType = NodeType.Node;
                    else
                        n.nodeType = NodeType.Leaf;
                }
                else
                {
                    n.nodeType = NodeType.Root;
                }
            }
        }

        // ノードのPosition設定
        public void SetNodePosition(Vector3 position, int nodeId)
        {
            foreach (Node n in nodeList)
            {
                if (n.nodeId == nodeId)
                    n.pos = position;
            }
            UpdateNodeTransform(Vector3.zero, Vector3.zero);
        }

        // ノードのRotation設定
        public void SetNodeRotation(Vector3 rotation, int nodeId)
        {
            foreach (Node n in nodeList)
            {
                if (n.nodeId == nodeId)
                    n.rotate = rotation;
            }
            UpdateNodeTransform(Vector3.zero, Vector3.zero);
        }

        // ノードのScale設定
        public void SetNodeScale(float scale, int nodeId)
        {
            foreach (Node n in nodeList)
            {
                if (n.nodeId == nodeId)
                    n.scale = scale;
            }
            UpdateNodeTransform(Vector3.zero, Vector3.zero);
        }

        // ノードの位置関係更新
        private void UpdateNodeTransform(Vector3 rootPos, Vector3 rootRotate)
        {
            foreach (Node n in nodeList)
            {
                // RootNodeで実行開始
                if (n.nodeType == NodeType.Root)
                    UpdateNodeTransformRoot(n, rootPos, rootRotate);
            }

            Debug.Log(this + ":UpdateNodeTransform");
        }

        // ノード位置関係更新（Root用）
        private void UpdateNodeTransformRoot(Node node, Vector3 rootPos, Vector3 rootRotate)
        {
            // パラメータを元にTransformを更新
            node.transform.localScale = Vector3.one * node.scale;

            Vector3 newPos = node.pos + rootPos;
            node.transform.localPosition = newPos;

            Quaternion rootQuaternion = Quaternion.Euler(rootRotate);
            Quaternion nodeQuaternion = Quaternion.Euler(node.rotate);
            Vector3 newRotate = (rootQuaternion * nodeQuaternion).eulerAngles;
            node.pose.localEulerAngles = newRotate;

            // 子ノードで実行
            if (node.nodeType != NodeType.Leaf)
            {
                foreach (Node childNode in node.childNodeList)
                {
                    UpdateNodeTransformNode(childNode, newPos, newRotate);
                }
            }
        }

        // ノード位置関係更新（Node用）
        private void UpdateNodeTransformNode(Node node, Vector3 parentPos, Vector3 parentRotate)
        {
            // パラメータを元にTransformを更新
            // 子ノードposは親ノードrotateで回転した後に加算する
            node.transform.localScale = Vector3.one * node.scale;

            Quaternion parentQuaternion = Quaternion.Euler(parentRotate);

            Vector3 newPos = parentPos + (parentQuaternion * node.pos);
            node.transform.localPosition = newPos;

            Quaternion nodeQuaternion = Quaternion.Euler(node.rotate);
            Vector3 newRotate = (parentQuaternion * nodeQuaternion).eulerAngles;
            node.pose.localEulerAngles = newRotate;

            // 子ノードで実行
            if (node.nodeType != NodeType.Leaf)
            {
                foreach (Node childNode in node.childNodeList)
                {
                    UpdateNodeTransformRoot(childNode, newPos, newRotate);
                }
            }
        }
    }
}
