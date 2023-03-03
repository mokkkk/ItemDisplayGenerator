using System;
using UnityEngine;

/**
* シングルトン親クラス．
*/
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    //外部からアクセスされるためのプロパティ
    public static T Instance
    {
        get
        {
            //インスタンスがなければ
            if (instance == null)
            {
                //クラスの型を取得
                Type type = typeof(T);

                //自分自身をインスタンス化
                instance = (T)FindObjectOfType(type);

                //アタッチされていない時
                if (instance == null)
                {
                    Debug.LogError(type + " をアタッチしているGameObjectが存在しません");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
    }

    protected void CheckInstance()
    {
        //インスタンスが存在していなければ、自分自身をインスタンス化
        if (instance == null) { instance = this as T; return; }

        //自分自身なら何もしない
        else if (Instance == this) { return; }

        //自分自身を破棄する
        Destroy(this);
        return;
    }
}