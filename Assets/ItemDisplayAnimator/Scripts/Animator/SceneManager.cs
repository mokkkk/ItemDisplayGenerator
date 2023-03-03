using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    /**
    * シーン全体の管理，機能呼び出しを行う．
    */
    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        // Manager
        [SerializeField]
        private NodeManager nodeManager;

        // UI
        [SerializeField]
        private JsonFilePanelUI jsonFilePanelUI;


        // ノード用Json読み込みUI表示
        public void ShowJsonFilePanelUI()
        {
            jsonFilePanelUI.gameObject.SetActive(true);
        }

        // ノード作成
        public void GenerateNode(NodeGenerationData data)
        {
            nodeManager.GenerateNode(data);
        }
    }
}
