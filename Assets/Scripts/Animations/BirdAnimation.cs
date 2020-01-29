using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    public GameObject WingTarget;
    [Range(0.0f, 180.0f)]
    public float WingsAngle = 90f;
    public float FlapHeight = 0.44f;
    public float TimeForCycle = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (WingTarget)
        {
            StartCoroutine(WingFlap());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Body").localRotation = Quaternion.Euler(new Vector3((-WingsAngle + 90) / 1.5f, 0, 0));
        transform.Find("Wings").localRotation = Quaternion.Euler(new Vector3(-WingsAngle + 90, 0, 0));
    }

    private IEnumerator WingFlap()
    {
        float t_TimeElapsed = 0f;
        float t_Direction = 1f;
        while (WingTarget)
        {
            float t_Height = Mathf.Lerp(-FlapHeight, FlapHeight, t_TimeElapsed / TimeForCycle);
            WingTarget.transform.localPosition = new Vector3(WingTarget.transform.localPosition.x, t_Height, WingTarget.transform.localPosition.z);
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
