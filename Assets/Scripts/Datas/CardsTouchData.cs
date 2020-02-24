/* 
    create by baihan 2020.02.24 
    触摸操作的数据 
*/

using System;

namespace TPOne.Datas
{
    [Serializable]
    public class CardsTouchData
    {
        public bool m_bTouchValid;
        // 当前输入的选中状态，true为此次输入为选牌，否则为取消选牌
        public bool m_bCurrentSeletState;
    }
}