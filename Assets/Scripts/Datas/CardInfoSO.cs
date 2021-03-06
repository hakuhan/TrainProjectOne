/* 
    create by baihan 2020.02.20 
    CardInfoSO：卡牌数据存储类
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TPOne.Datas
{
    [CreateAssetMenu]
    public class CardInfoSO : ScriptableObject
    {
        public int m_iCardInHandCount;
        public int m_iCardTotalCount;
        public Texture2D backTex;
        public Texture2D whiteTex;
        public List<InfoData> m_infos;

        public List<CardNumberItem> m_lsNum;
        public CardFlowerItem m_cardLittleFlowerItem;
        public CardFlowerItem m_cardBigFlowerItem;
        public CardSpecialItem m_specialItem;
    }

    [Serializable]
    public class CardNumberItem
    {
        public Texture2D m_t2RedNum;
        public Texture2D m_t2BlckNum;
    }

    [Serializable]
    public class CardFlowerItem
    {
        public Texture2D m_t2Heart;
        public Texture2D m_t2Block;
        public Texture2D m_t2Plum;
        public Texture2D m_t2Spade;
    }

    [Serializable]
    public class CardSpecialItem
    {
        public Texture2D m_t2SJoker;
        public Texture2D m_t2BJoker;

    }

    [Serializable]
    public class InfoData
    {
        public int m_iId;
        public int m_iOrder;
        public E_CardNumber m_eNumber;
        public E_CardType m_eType;
        public Texture2D m_t2Num;
        public Texture2D m_t2Flower1;
        public Texture2D m_t2Flower2;
    }

    public enum E_CardNumber
    {
        a,
        two,
        tree,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        j,
        q,
        k,
        joker,
        none,
    }

    public enum E_CardType
    {
        heart,
        block,
        plum,
        spade,
        littleJoker,
        bigJoker,
    }
}