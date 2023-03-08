using UnityEngine;

namespace Animator
{
    public class SelectParentUIButton : MonoBehaviour
    {
        public SelectParentUI selectParentUI;

        public int nodeId;

        public void OnClickButton()
        {
            selectParentUI.OnClickSelectParentButton(this.nodeId);
        }
    }
}
