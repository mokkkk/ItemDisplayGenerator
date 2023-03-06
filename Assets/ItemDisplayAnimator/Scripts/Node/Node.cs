using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    /**
    * ノード親子関係表現用のEnum．
    */
    public enum NodeType
    {
        Root, Node, Leaf
    }

    /**
    * ノードのデータを保持する．
    */
    public class Node : MonoBehaviour
    {

        // ID
        public int nodeId;
        // 種類
        public NodeType nodeType;
        // 名前
        public string nodeName;
        // CustomModelData
        public int customModelData;

        // TransformationNbt
        public Vector3 pos, rotate;
        public float scale;

        // モデル表示用
        public Transform pose;
        public Transform element;
        public List<Transform> elementCubes;

        // 親子関係
        public Node parentNode;
        public int parentNodeId;
        public List<Node> childNodeList;

        // 初期化
        public void Initialize(NodeGenerationData data, int id)
        {
            // Component取得
            pose = this.transform.Find("Pose");
            element = pose.transform.Find("Element");
            foreach (Transform child in element)
                elementCubes.Add(child);

            // 値初期化
            this.nodeId = id;
            this.nodeName = data.partName;
            this.customModelData = data.customModelData;
            this.nodeType = NodeType.Root;
            this.parentNode = null;
            this.childNodeList = new List<Node>();
            this.pos = this.rotate = Vector3.zero;
            this.scale = 1.0f;

            // ノード名設定
            this.gameObject.name = nodeName;
        }
    }
}
