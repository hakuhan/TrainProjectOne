/* 
    create by baihan 2020.02.24 
    通用的提示弹窗 
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TPOne.Ui
{
    public class PopUpNotice : MonoBehaviour
    {
        public Text tTitle;
        public Text tInfo;
        UnityAction callback;
        public void Init(string title, string info, UnityAction callback = null)
        {
            tTitle.text = title;
            tInfo.text = info;
            this.callback = callback;
        }

        public void OnClickConfirm()
        {
            callback?.Invoke();
            Close();
        }

        void Close()
        {
            Destroy(gameObject);
        }
    }
}