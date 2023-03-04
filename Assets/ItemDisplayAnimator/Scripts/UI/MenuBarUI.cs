using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    /**
    * MenuBarUIのイベントを管理する．
    */
    public class MenuBarUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject fileMenuUI;

        // Fileメニュー開閉
        public void ShowFileMenu()
        {
            fileMenuUI.SetActive(!fileMenuUI.activeSelf);
        }

        // Jsonファイルを読み込み，ノードを作成する
        public void ImportJson()
        {
            SceneManager.Instance.ShowJsonFilePanelUI();
            fileMenuUI.SetActive(false);
        }
    }
}
