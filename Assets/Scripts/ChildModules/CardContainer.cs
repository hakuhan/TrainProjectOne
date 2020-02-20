/* 
    create by baihan 2020.02.20 
    CardContainer: 单例容器类，提供数据操作接口 
*/

using System.Collections;
using System.Collections.Generic;
using TPOne.Singleton;
using UnityEngine;

namespace TPOne.Submodule
{
    public class CardContainer : MonoSingleton<CardContainer>
    {
        public List<CardData> m_lsCardDatas;
    }
}
