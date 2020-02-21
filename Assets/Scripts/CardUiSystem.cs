/* 
    create by baihan 2020.02.21 
    管理ui以及事件 
*/

using System.Collections;
using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Submodule;
using UnityEngine;
using UnityEngine.UI;

namespace TPOne.Ui
{
    public class CardUiSystem : MonoBehaviour
    {
        public CardsShowingSystem m_cardsShowingSystem;

        #region Life Circle

        private void Start()
        {
            m_cardsShowingSystem.InitCard();
        }

        

        #endregion
    }
}
