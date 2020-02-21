/* 
    create by baihan 2020.02.20 
    创建棋牌的规则 
*/

using TPOne;
using TPOne.Datas;
using UnityEngine;

namespace TPOne.Submodule
{
    public class CardRuler : MonoBehaviour, ICardRule
    {
        [SerializeField]
        CardInfoSO m_cardInfoSo;

        public void UpdateRule()
        {
            // Set card rule data
            m_cardInfoSo.m_infos.Clear();

            // Set nomal card
            for (int i = 0; i < m_cardInfoSo.m_iCardTotalCount - 2; ++i)
            {
                var cardInfo = new InfoData();
                cardInfo.m_iId = i;

                cardInfo.m_bJoker = false;

                // set order
                // cardInfo.m_order = i / 13 + i % 13;
                cardInfo.m_iOrder = i % 13 - 2;
                if (cardInfo.m_iOrder < 0)
                {
                    cardInfo.m_iOrder += 13;
                }

                // Set number texture
                var t2Num = m_cardInfoSo.m_lsNum[i % 13];
                if (i > 26)
                {
                    cardInfo.m_t2Num = t2Num.m_t2BlckNum;
                }
                else
                {
                    cardInfo.m_t2Num = t2Num.m_t2RedNum;
                }

                // Set flower texture
                Texture2D t2Flower1;
                Texture2D t2Flower2;
                switch (i / 13)
                {
                    case 0:
                        t2Flower1 = m_cardInfoSo.m_cardLittleFlowerItem.m_t2Block;
                        t2Flower2 = m_cardInfoSo.m_cardBigFlowerItem.m_t2Block;
                        break;

                    case 1:
                        t2Flower1 = m_cardInfoSo.m_cardLittleFlowerItem.m_t2Heart;
                        t2Flower2 = m_cardInfoSo.m_cardBigFlowerItem.m_t2Heart;
                        break;

                    case 2:
                        t2Flower1 = m_cardInfoSo.m_cardLittleFlowerItem.m_t2Plum;
                        t2Flower2 = m_cardInfoSo.m_cardBigFlowerItem.m_t2Plum;
                        break;

                    case 3:
                        t2Flower1 = m_cardInfoSo.m_cardLittleFlowerItem.m_t2Spade;
                        t2Flower2 = m_cardInfoSo.m_cardBigFlowerItem.m_t2Spade;
                        break;

                    default:
                        t2Flower1 = m_cardInfoSo.m_cardLittleFlowerItem.m_t2Block;
                        t2Flower2 = m_cardInfoSo.m_cardBigFlowerItem.m_t2Block;
                        break;
                }
                cardInfo.m_t2Flower1 = t2Flower1;
                cardInfo.m_t2Flower2 = t2Flower2;

                m_cardInfoSo.m_infos.Add(cardInfo);
            }

            // Set joker
            m_cardInfoSo.m_infos.Add(new InfoData() 
                { 
                    m_iId = 52, 
                    m_bJoker = true,
                    m_t2Flower1 = null, 
                    m_iOrder = 52, 
                    m_t2Flower2 = null, 
                    m_t2Num = m_cardInfoSo.m_specialItem.m_t2SJoker 
                });
            m_cardInfoSo.m_infos.Add(new InfoData() 
                { 
                    m_iId = 53, 
                    m_bJoker = true,
                    m_t2Flower1 = null, 
                    m_iOrder = 53, 
                    m_t2Flower2 = null, 
                    m_t2Num = m_cardInfoSo.m_specialItem.m_t2BJoker 
                });

        }
    }
}