/* 
    create by baihan 2020.02.20 
    CardInputSystem: 管理用户输入，将输入进行分发 
*/

using TPOne.Events;
using UnityEngine;

public class CardInputSystem : MonoBehaviour
{
    public bool m_bSlide;
    public float m_fDragDistance;
    public Vector3 m_v3MouseCurrentPosion;
    public Vector3 m_v3StartPosition;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_v3StartPosition = Input.mousePosition;
            m_fDragDistance = 0f;

            EventSystem.OnTouchBegin();
        }

        if (Input.GetMouseButton(0))
        {
            m_fDragDistance += Input.mousePosition.x - m_v3MouseCurrentPosion.x;
    
            if (Mathf.Abs(m_fDragDistance) > 10f)
            {
                EventSystem.OnDrag(m_v3StartPosition, m_v3MouseCurrentPosion);
            }
            m_v3MouseCurrentPosion = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            EventSystem.OnTouchOver();
        }
    }
}