/* 
    create by baihan 2020.02.21 
    管理ui以及事件 
*/

using System.Collections;
using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using TPOne.Submodule;
using UnityEngine;
using UnityEngine.UI;

namespace TPOne.Ui
{
    public class CardUiSystem : MonoBehaviour
    {
        public CardsShowingSystem m_cardsShowingSystem;
        public GameObject m_objPopUpPref;

        #region Life Circle

        private void Start()
        {
            m_cardsShowingSystem.InitCard();
        }

        private void OnEnable()
        {
            UiEvents.NoneSelectionFond += NoneSelectionFond;
        }

        private void OnDisable()
        {
            UiEvents.NoneSelectionFond -= NoneSelectionFond;
        }


        #endregion
        public void NoneSelectionFond()
        {
            var scrPop = Instantiate(m_objPopUpPref, transform.parent).GetComponent<PopUpNotice>();
            scrPop.Init("提示", "没有找到相应的选择");
        }
    }
}
