/* 
    create by baihan 2020.02.20 
    CardSlideSystem: 管理用户输入，将输入进行分发 
*/

using TPOne.Events;
using UnityEngine;

public class CardSlideSystem : MonoBehaviour
{
    public bool m_bSlide;
    public float m_fDragDistance;
    public Vector3 m_v3MouseCurrentPosion;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_v3MouseCurrentPosion = Input.mousePosition;
            m_fDragDistance = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            m_fDragDistance += Input.mousePosition.x - m_v3MouseCurrentPosion.x;
        }

        if (Mathf.Abs(m_fDragDistance) > 100f)
        {
            EventSystem.OnSlideCard();

            m_bSlide = true;
        }

        if (Input.GetMouseButtonUp(0) && m_bSlide)
        {
            m_bSlide = false;
            EventSystem.OnSlideOver();
        }
    }
}