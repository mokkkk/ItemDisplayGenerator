using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animator
{
    /**
    * ノードの値を変更するUI．
    */
    public class ModelUI : MonoBehaviour
    {
        // 対応するノードID
        private int nodeId;
        public int NodeId
        {
            get
            {
                return this.nodeId;
            }
        }

        [SerializeField]
        private Text nodeName;

        // 対応するInputField
        [SerializeField]
        private InputField customModelDataInputField;
        [SerializeField]
        private InputField positionX, positionY, positionZ;
        [SerializeField]
        private InputField rotationX, rotationY, rotationZ;
        [SerializeField]
        private InputField scale;

        // UI作成
        public void Initialize(Node node)
        {
            this.nodeId = node.nodeId;
            nodeName.text = node.name;
            this.customModelDataInputField.text = node.customModelData.ToString();
            this.positionX.text = $"{node.pos.x:F2}";
            this.positionY.text = $"{node.pos.y:F2}";
            this.positionZ.text = $"{node.pos.z:F2}";
            this.rotationX.text = $"{node.rotate.x:F2}";
            this.rotationY.text = $"{node.rotate.y:F2}";
            this.rotationZ.text = $"{node.rotate.z:F2}";
            this.scale.text = $"{node.scale:F2}";
        }

        // 各パラメータ更新時に呼び出すイベント
        public void OnPositionChanged()
        {
            // フォーマット
            positionX.text = $"{float.Parse(positionX.text):F2}";
            positionY.text = $"{float.Parse(positionY.text):F2}";
            positionZ.text = $"{float.Parse(positionZ.text):F2}";
            Debug.Log(nodeName + " : Position Changed");
        }

        public void OnRotationChanged()
        {
            // フォーマット
            rotationX.text = $"{float.Parse(rotationX.text):F2}";
            rotationY.text = $"{float.Parse(rotationY.text):F2}";
            rotationZ.text = $"{float.Parse(rotationZ.text):F2}";
            Debug.Log(nodeName + " : Rotation Changed");
        }

        public void OnScaleChanged()
        {
            // フォーマット
            scale.text = $"{float.Parse(scale.text):F2}";
            Debug.Log(nodeName + " : Scale Changed");
        }
    }
}
