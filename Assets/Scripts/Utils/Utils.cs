/* 
    create by baihan 2020.02.20 
    Utils:提供全局常用方法 
*/

using System.Collections.Generic;
using TPOne.Datas;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utils
{
    public static void Order(List<InfoData> lsData, CardInfoSO cardInfos, bool bReverse)
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

    public static bool IsPointerTouchGameObjectFirst(Vector2 v2MousePosition, GameObject targetObj)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = v2MousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, raycastResults);
        if (raycastResults[0].gameObject.GetInstanceID() == targetObj.GetInstanceID())
        {
            return true;
        }

        return false;
    }

    public static List<int> GetIdsByCardNumber(CardInfoSO infoSO, E_CardNumber eCardNumber)
    {
        var lsCards = CardContainer.Instance.m_lsCards;

        var ids = infoSO.m_infos.FindAll(_d => _d.m_eNumber == eCardNumber);

        var lsResult = new List<int>();
        foreach (var id in ids)
        {
            lsResult.Add(id.m_iId);
        }

        return lsResult;
    }

    public static E_CardNumber GetCardNumberById(CardInfoSO infoSO, int id)
    {
        int index = infoSO.m_infos.FindIndex(_info => _info.m_iId == id);

        if (index != -1)
        {
            return infoSO.m_infos[index].m_eNumber;
        }

        return E_CardNumber.none;
    }

    public static int GetLandlordRealOrder(E_CardNumber eNum)
    {
        int iResult = (int)eNum;

        if (eNum <= E_CardNumber.two)
        {
            iResult = (int)eNum + (int)E_CardNumber.joker;
        }

        return iResult;
    }

    public static E_CardNumber PlugLandlordNumber(E_CardNumber eTarget, int iNum)
    {
        int result = (int)(eTarget + iNum);
        if (result >= (int)E_CardNumber.joker)
        {
            result -= 13;
        }

        return (E_CardNumber)result;
    }

    // 线与矩形是否相交
    public static bool IsLineIntersectRect(Vector2 v2LineStart, Vector2 v2LineEnd, Rect rect)
    {
        var v2LeftDown = new Vector2(rect.xMin, rect.yMin);
        var v2LeftUp = new Vector2(rect.xMin, rect.yMax);
        var v2RightUp = new Vector2(rect.xMax, rect.yMax);
        var v2RigtDown = new Vector2(rect.xMax, rect.yMin); ;

        if (LineIntersectLine(v2LineStart, v2LineEnd, v2LeftDown, v2LeftUp))
            return true;
        if (LineIntersectLine(v2LineStart, v2LineEnd, v2LeftUp, v2RightUp))
            return true;
        if (LineIntersectLine(v2LineStart, v2LineEnd, v2RightUp, v2RigtDown))
            return true;
        if (LineIntersectLine(v2LineStart, v2LineEnd, v2RigtDown, v2LeftDown))
            return true;

        return false;
    }

    // 线与线是否相交
    public static bool LineIntersectLine(Vector2 l1Start, Vector2 l1End, Vector2 l2Start, Vector2 l2End)
    {
        return QuickReject(l1Start, l1End, l2Start, l2End) && Straddle(l1Start, l1End, l2Start, l2End);
    }

    // 快速排序
    public static bool QuickReject(Vector2 v3L1Start, Vector2 v3L1End, Vector2 v3L2Start, Vector2 v3L2End)
    {
        float l1xMax = Mathf.Max(v3L1Start.x, v3L1End.x);
        float l1yMax = Mathf.Max(v3L1Start.y, v3L1End.y);
        float l1xMin = Mathf.Min(v3L1Start.x, v3L1End.x);
        float l1yMin = Mathf.Min(v3L1Start.y, v3L1End.y);

        float l2xMax = Mathf.Max(v3L2Start.x, v3L2End.x);
        float l2yMax = Mathf.Max(v3L2Start.y, v3L2End.y);
        float l2xMin = Mathf.Min(v3L2Start.x, v3L2End.x);
        float l2yMin = Mathf.Min(v3L2Start.y, v3L2End.y);

        if (l1xMax < l2xMin || l1yMax < l2yMin || l2xMax < l1xMin || l2yMax < l1yMin)
            return false;

        return true;
    }

    // 跨立实验
    public static bool Straddle(Vector3 v3L1Start, Vector3 v3L1End, Vector3 v3L2Start, Vector3 v3L2End)
    {
        float l1x1 = v3L1Start.x;
        float l1x2 = v3L1End.x;
        float l1y1 = v3L1Start.y;
        float l1y2 = v3L1End.y;
        float l2x1 = v3L2Start.x;
        float l2x2 = v3L2End.x;
        float l2y1 = v3L2Start.y;
        float l2y2 = v3L2End.y;

        if ((((l1x1 - l2x1) * (l2y2 - l2y1) - (l1y1 - l2y1) * (l2x2 - l2x1)) *
             ((l1x2 - l2x1) * (l2y2 - l2y1) - (l1y2 - l2y1) * (l2x2 - l2x1))) > 0 ||
            (((l2x1 - l1x1) * (l1y2 - l1y1) - (l2y1 - l1y1) * (l1x2 - l1x1)) *
             ((l2x2 - l1x1) * (l1y2 - l1y1) - (l2y2 - l1y1) * (l1x2 - l1x1))) > 0)
        {
            return false;
        }

        return true;
    }

}