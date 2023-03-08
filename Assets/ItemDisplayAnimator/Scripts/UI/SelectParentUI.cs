using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animator
{
    /**
    * ノードのParent選択用UIを管理する．
    */
    public class SelectParentUI : MonoBehaviour
    {
        [SerializeField]
        private SelectParentUIButton selectParentButton;
        [SerializeField]
        private Transform contentHolder;

        private List<GameObject> selectParentButtonList;

        private const float ButtonHeight = 30.0f;
        private const float ButtonHeightOffset = -15.0f;

        // UI生成
        public void Initialize(List<Node> nodeList, int targetNodeId)
        {
            // List初期化
            selectParentButtonList = new List<GameObject>();

            // Root用Button表示
            CreateButton(-1, "Root");

            // その他Button表示
            foreach (Node n in nodeList)
            {
                if (n.nodeId != targetNodeId)
                    CreateButton(n.nodeId, n.nodeName);
            }
        }

        // UI非表示
        public void HideUI()
        {
            // Button削除
            foreach (GameObject obj in selectParentButtonList)
            {
                Destroy(obj);
            }

            // List初期化
            selectParentButtonList = new List<GameObject>();

            this.gameObject.SetActive(false);
        }

        // ボタン作成
        private void CreateButton(int nodeId, string nodeName)
        {
            // GameObject作成
            var obj = Instantiate(selectParentButton.gameObject);
            var button = obj.GetComponent<SelectParentUIButton>();
            button.nodeId = nodeId;
            obj.GetComponentInChildren<Text>().text = nodeName;
            button.selectParentUI = this;
            button.transform.parent = contentHolder;
            selectParentButtonList.Add(obj);

            // Holderのサイズ調整
            contentHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, ButtonHeight * contentHolder.transform.childCount);

            // UI移動
            var buttonTransform = obj.GetComponent<RectTransform>();
            buttonTransform.offsetMin = new Vector2(0.0f, buttonTransform.offsetMin[1]);
            buttonTransform.offsetMax = new Vector2(0.0f, buttonTransform.offsetMax[1]);
            buttonTransform.anchoredPosition = new Vector2(0.0f, -(contentHolder.transform.childCount - 1) * ButtonHeight + ButtonHeightOffset);
        }

        // Nodeボタンクリック時
        public void OnClickSelectParentButton(int nodeId)
        {
            NodeManager.Instance.SelectNodeParentComplete(nodeId);
            // UI非表示
            HideUI();
        }

        // キャンセルボタンクリック時
        public void OnClickCancelButton()
        {
            NodeManager.Instance.SelectNodeParentCancel();
            // UI非表示
            HideUI();
        }
    }
}
