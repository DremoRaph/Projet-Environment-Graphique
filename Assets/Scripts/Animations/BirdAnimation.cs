using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    public GameObject WingTarget;
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
            StartCoroutine(WingFlap());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Body").localRotation = Quaternion.Euler(new Vector3((-WingsAngle + 90) / 1.5f, 0, 0));
        transform.Find("Wings").localRotation = Quaternion.Euler(new Vector3(-WingsAngle + 90, 0, 0));
        float t_TransformedWingSpan = Mathf.Lerp(WingsSpan, 0.4f, Velocity / 5);
        m_Wing1.transform.localPosition = new Vector3(m_Wing1.transform.localPosition.x, m_Wing1.transform.localPosition.y, t_TransformedWingSpan);
        m_Wing2.transform.localPosition = new Vector3(m_Wing2.transform.localPosition.x, m_Wing2.transform.localPosition.y, -t_TransformedWingSpan);
    }

    private IEnumerator WingFlap()
    {
        float t_TimeElapsed = 0f;
        float t_Direction = 1f;
        while (WingTarget)
        {
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
