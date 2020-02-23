using System.Collections.Generic;
using TPOne.Datas;
using UnityEngine;


namespace TPOne.CardSelector
{
    public class CommonModule
    {
        public static bool UpdateOffset(List<E_CardNumber> lsNumbers, ref int iOffset)
        {
            if (lsNumbers == null || lsNumbers.Count == 0)
            {
                return false;
            }

            // Check offset
            if (iOffset < lsNumbers.Count - 1)
            {
                ++iOffset;
            }
            else
            {
                iOffset = 0;
            }

            return true;
        }
    }
}