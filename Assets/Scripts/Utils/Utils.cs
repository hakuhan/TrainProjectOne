/* 
    create by baihan 2020.02.20 
    Utils:提供全局常用方法 
*/

using System.Collections.Generic;
using TPOne.Submodule;

public class Utils
{
    public static void Order(List<CardData> lsData, CardInfoSO cardInfos, bool bReverse)
    {
        for (int i = 0; i < lsData.Count - 1; ++i)
        {
            for (int j = 0; j < lsData.Count - i - 1; ++j)
            {
                int orderFirst = cardInfos.m_infos.Find(c => c.m_iId == lsData[j].m_iId).m_iOrder;
                int orderSecond = cardInfos.m_infos.Find(c => c.m_iId == lsData[j + 1].m_iId).m_iOrder;
                if (orderFirst > orderSecond || (bReverse && orderSecond > orderFirst))
                {
                    var temp = lsData[j + 1];
                    lsData[j + 1] = lsData[j];
                    lsData[j] = temp;
                }
            }
        }
    }
}