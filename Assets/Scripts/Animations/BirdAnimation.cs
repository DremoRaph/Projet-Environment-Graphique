using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    public GameObject WingTarget;
    public bool IsFlying = true;
    [Range(0.0f, 5f)]
    public float Velocity = 0f;
    [Range(0.0f, 180.0f)]
    public float WingsAngle = 90f;
    [Range(0.0f, 2f)]
    public float WingsSpan = 1f;
    [Range(0.0f, 1f)]
    public float WingsDrawBack = 0.5f;
    public float MaxFlapHeight = 0.44f;
    public float TimeForCycle = 0.7f;
    private GameObject m_Wing1;
    private GameObject m_Wing2;
    private float m_InitialWingAngle;
    private Vector3 m_InitialWingTargetPosition;
    private bool m_IsFlapping = false;
    private float m_AnimationProgression = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (WingTarget)
        {
            m_Wing1 = WingTarget.transform.Find("Wing1Target").gameObject;
            m_Wing1.transform.localPosition = new Vector3(0, 0, WingsSpan);
            m_Wing2 = WingTarget.transform.Find("Wing2Target").gameObject;
            m_Wing2.transform.localPosition = new Vector3(0, 0, -WingsSpan);
            m_InitialWingAngle = WingTarget.transform.localPosition.x;
            m_InitialWingTargetPosition = m_Wing1.transform.parent.localPosition;
}
    }

    // Update is called once per frame
    void Update()
    {
        InitiateFlyingAnimation();
        transform.Find("Body").localRotation = Quaternion.Euler(new Vector3((-WingsAngle + 90) / 1.5f, 0, 0));
        transform.Find("Wings").localRotation = Quaternion.Euler(new Vector3(-WingsAngle + 90, 0, 0));
        float t_TransformedWingSpan = Mathf.Lerp(WingsSpan, 0.4f, Velocity / 5);
        m_Wing1.transform.localPosition = new Vector3(m_Wing1.transform.localPosition.x, m_Wing1.transform.localPosition.y, t_TransformedWingSpan);
        m_Wing2.transform.localPosition = new Vector3(m_Wing2.transform.localPosition.x, m_Wing2.transform.localPosition.y, -t_TransformedWingSpan);
    }

    public void InitiateFlyingAnimation()
    {
        if(IsFlying && !m_IsFlapping)
        {
            StartCoroutine(WingFlap(m_AnimationProgression));
            Debug.Log("ANIMATION STARTING");
        } else if(!IsFlying && m_IsFlapping)
        {
            //StopCoroutine(WingFlap());
            Debug.Log("ANIMATION STOPPED");
            m_IsFlapping = false;
        }
    }

    private IEnumerator WingFlap(float a_TimeRecuperation)
    {
        m_IsFlapping = true;
        Debug.Log(a_TimeRecuperation);
        float t_TimeElapsed = a_TimeRecuperation;
        float t_Direction = 1f;
        while (WingTarget)
        {
            if (!m_IsFlapping)
            {
                m_AnimationProgression = t_TimeElapsed;
                yield break;
            }
            float t_FlapHeight = Mathf.Lerp(MaxFlapHeight, 0, Velocity / 5);
            float t_Height = Mathf.Lerp(-t_FlapHeight, t_FlapHeight, t_TimeElapsed / TimeForCycle);
            WingTarget.transform.localPosition = new Vector3( -WingsDrawBack - Velocity / 10, t_Height, WingTarget.transform.localPosition.z);
            if(t_TimeElapsed >= TimeForCycle)
            {
                t_Direction = -1;
            }
            if (t_TimeElapsed <= 0)
            {
                t_Direction = 1;
            }
            t_TimeElapsed += Time.deltaTime * t_Direction;
            yield return null;
        }

        yield return null;
    }
}
