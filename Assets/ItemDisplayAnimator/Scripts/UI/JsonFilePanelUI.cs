using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;

namespace Animator
{
    /**
    * ノード生成用のJson読み込みUI．
    */
    public class JsonFilePanelUI : MonoBehaviour
    {
        [SerializeField]
        private InputField jsonFileInput;
        [SerializeField]
        private InputField partNameInput;
        [SerializeField]
        private InputField customModelDataInput;

        // ファイル選択ボタンクリック時
        public void OnClickSelectFileButton()
        {
            // ファイルダイアログを開く
            var extensions = new[]
            {
                new ExtensionFilter( "Model Files", "json"),
            };

            string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);

            jsonFileInput.text = paths[0];
        }

        // キャンセルボタンクリック時
        public void OnClickCancelButton()
        {
            jsonFileInput.text = null;
            partNameInput.text = null;
            customModelDataInput.text = null;
            this.gameObject.SetActive(false);
        }

        // インポートボタンクリック時
        public void OnClickImportButton()
        {
            // データ取得
            NodeGenerationData data;
            data.jsonFilePath = jsonFileInput.text;
            data.partName = partNameInput.text;
            data.customModelData = int.Parse(customModelDataInput.text);

            // NodeGeneratorにデータを渡す
            SceneManager.Instance.GenerateNode(data);

            jsonFileInput.text = null;
            partNameInput.text = null;
            customModelDataInput.text = null;
            this.gameObject.SetActive(false);
        }
    }
}