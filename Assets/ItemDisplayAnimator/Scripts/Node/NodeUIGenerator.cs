using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    /**
    * ノードと対応するUIを生成する．
    */
    public class NodeUIGenerator : MonoBehaviour
    {
        // UI製作用Prefab
        [SerializeField]
        private GameObject modelUIPrefab;

        // Transform
        [SerializeField]
        private Transform modelUIHolder;

        // UIリスト
        private List<ModelUI> modelUIList;

        // UIのサイズ
        private const float ModelUIHeight = 300.0f;
        private const float ModelUIHeightOffset = -150.0f;

        // UIを生成する
        public void GenerateUI(Node node)
        {
            // UI生成
            ModelUI modelUI = Instantiate(modelUIPrefab).GetComponent<ModelUI>();
            modelUI.Initialize(node);
            modelUI.transform.parent = modelUIHolder;

            // Holderのサイズ調整
            modelUIHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, ModelUIHeight * modelUIHolder.transform.childCount);

            // UI移動
            var modelUITransform = modelUI.GetComponent<RectTransform>();
            modelUITransform.offsetMin = new Vector2(0.0f, modelUITransform.offsetMin[1]);
            modelUITransform.offsetMax = new Vector2(0.0f, modelUITransform.offsetMax[1]);
            modelUITransform.anchoredPosition = new Vector2(0.0f, -(modelUIHolder.transform.childCount - 1) * ModelUIHeight + ModelUIHeightOffset);
        }
    }
}
