using System;

namespace TPOne.Datas
{
    [Serializable]
    public class CardsSystemData
    {
        public bool m_bTouchValid;
        // 当前输入的选中状态，true为此次输入为选牌，否则为取消选牌
        public bool m_bCurrentSeletState;
    }
}