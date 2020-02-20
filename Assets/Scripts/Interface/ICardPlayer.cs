/* 
    create by baihan 2020.02.20 
    发牌接口 
*/

using System.Collections.Generic;

namespace TPOne
{
    public interface ICardPlayer
    {
        void PlayCard(List<int> cards);
    }
}