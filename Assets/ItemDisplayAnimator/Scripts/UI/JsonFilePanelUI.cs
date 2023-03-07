using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
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

        private const string DefaultCustomModelData = "0";

        // ファイル選択ボタンクリック時
        public void OnClickSelectFileButton()
        {
            // ファイルダイアログを開く
            var extensions = new[]
            {
                new ExtensionFilter( "Model Files", "json"),
            };

            string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);

            if (paths.Length <= 0)
                return;

            jsonFileInput.text = paths[0];

            // デフォルトノード名を入力
            var strList = paths[0].Split('\\').Last().Split('.')[0].Split('_');
            var defaultNodeName = "";
            foreach (string s in strList)
            {
                var name = char.ToUpper(s[0]) + s.Substring(1);
                defaultNodeName += name;
            }
            partNameInput.text = defaultNodeName;
        }

        // キャンセルボタンクリック時
        public void OnClickCancelButton()
        {
            jsonFileInput.text = null;
            partNameInput.text = null;
            customModelDataInput.text = DefaultCustomModelData;
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
            customModelDataInput.text = DefaultCustomModelData;
            this.gameObject.SetActive(false);
        }
    }
}