using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animator
{
    // Json読み込み用クラス
    [Serializable]
    public class JsonModel
    {
        public JsonElement[] elements;
        public JsonDisplay display;
    }
    [Serializable]
    public class JsonElement
    {
        public float[] from;
        public float[] to;
        public JsonRotation rotation;
    }
    [Serializable]
    public class JsonRotation
    {
        public float angle;
        public string axis;
        public float[] origin;
    }
    [Serializable]
    public class JsonDisplay
    {
        public JsonHeadDisplay head;
    }
    [Serializable]
    public class JsonHeadDisplay
    {
        public float[] translation;
        public float[] rotation;
        public float[] scale;
    }

    /**
    * ノードのGameObjectを製造する．
    */
    public class NodeGenerator : MonoBehaviour
    {

        // ノード製作用Prefab
        [SerializeField]
        private GameObject dummyNodePrefab;
        [SerializeField]
        private GameObject pivotCubePrefab;

        // 製造中のノード
        private GameObject nodeObject;

        private const float ScaleOffset = 16.0f;
        private const float HeadScaleOffsetSmall = 0.4635f;
        private const float TranslationOffset = 14.0f;
        private const float PivotCenter = 8.0f;
        private const float DefaultLocalPositionY = 0.5f;

        // ノード生成
        public Node GenerateNode(NodeGenerationData data, int id)
        {
            Debug.Log(this + ":ノード生成");

            // オブジェクト生成
            var newNode = Instantiate(dummyNodePrefab, Vector3.zero, Quaternion.identity);
            var node = newNode.GetComponent<Node>();
            var elementHolder = newNode.transform.Find("Pose").Find("Elements");

            // ID設定
            node.nodeId = id;

            // ファイル読み込み
            JsonModel inputJson = LoadJson(data.jsonFilePath);

            // キューブ生成
            foreach (JsonElement element in inputJson.elements)
            {
                GenerateCube(element, elementHolder);
            }

            return null;
        }

        // ファイル読み込み
        private JsonModel LoadJson(string filePath)
        {
            // ファイル読み込み
            string line;
            string inputString = "";
            // ファイルの改行削除，1行に纏める
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                inputString += line.Replace("\r", "").Replace("\n", "");
            }
            file.Close();

            // デシリアライズ
            JsonModel inputJson = new JsonModel();
            inputJson = JsonUtility.FromJson<JsonModel>(inputString);

            return inputJson;
        }

        // キューブ配置
        private void GenerateCube(JsonElement element, Transform elementHolder)
        {
            var cube = Instantiate(pivotCubePrefab, Vector3.zero, Quaternion.identity);
            cube.transform.parent = elementHolder;

            //  RightHand to LeftHand
            var from = new Vector3(element.from[0], element.from[1], element.to[2] + 2.0f * (PivotCenter - element.to[2]));
            var to = new Vector3(element.to[0], element.to[1], element.from[2] + 2.0f * (PivotCenter - element.from[2]));

            // Scale
            var scale = Vector3.zero;
            scale.x = to.x - from.x;
            scale.y = to.y - from.y;
            scale.z = to.z - from.z;
            if (scale.x == 0)
                scale.x = 0.001f;
            if (scale.y == 0)
                scale.y = 0.001f;
            if (scale.z == 0)
                scale.z = 0.001f;
            cube.transform.localScale = scale;

            // transform
            var pos = new Vector3(from.x, from.y, from.z);
            pos.x -= PivotCenter;
            pos.y -= PivotCenter;
            pos.z -= PivotCenter;
            cube.transform.localPosition = pos;

            // rotation
            if (element.rotation.axis != null)
            {
                // create pivot
                var pivot = new GameObject();
                pivot.transform.parent = elementHolder;
                var pivotPos = Vector3.zero;
                pivotPos.x = element.rotation.origin[0] - PivotCenter;
                pivotPos.y = element.rotation.origin[1] - PivotCenter;
                pivotPos.z = element.rotation.origin[2] + 2.0f * (PivotCenter - element.rotation.origin[2]) - PivotCenter;
                pivot.transform.localPosition = pivotPos;

                // rotate
                cube.transform.parent = pivot.transform;
                switch (element.rotation.axis)
                {
                    case "x":
                        pivot.transform.localRotation = Quaternion.Euler(new Vector3(-element.rotation.angle, 0.0f, 0.0f));
                        break;
                    case "y":
                        pivot.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, -element.rotation.angle, 0.0f));
                        break;
                    case "z":
                        pivot.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, element.rotation.angle));
                        break;
                }
                cube.transform.parent = elementHolder;
                // transform childに引っかからないよう，一度親子関係を解除
                pivot.transform.parent = transform.root;
                Destroy(pivot);
            }
        }
    }
}
