/* 
    create by baihan 2020.02.20 
    CardData: 牌运行过程中的数据 
*/
using System;
using UnityEngine;

namespace TPOne.Datas
{
    [Serializable]
    public class CardCurrentData
    {
        public bool m_bVisble = false;
        // 最终的选择状态
        public bool m_bSelected = false;
        public bool m_bSelectedTemp = false;
    }

}