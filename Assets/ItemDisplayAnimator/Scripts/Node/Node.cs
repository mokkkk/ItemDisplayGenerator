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

        // TransformationNbt
        public Vector3 pos, rotate;

        // モデル表示用
        public Transform pose;
        public Transform element;
        public List<Transform> elementCubes;

        // 親子関係
        public Node parentNode;
        public int parentNodeId;
        public List<Node> childNodeList;

    }
}