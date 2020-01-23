using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArm : MonoBehaviour
{

    public float SamplingDistance = 0f;
    public float LearningRate = 0f;

    public GameObject Target;
    [SerializeField]
    private List<GameObject> m_Joints = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<float> t_Angles = new List<float>();
        for (int i = 0; i < m_Joints.Count; i++)
        {
            if(m_Joints[i].GetComponent<TowerArmJoint>().Axis == new Vector3(1, 0, 0))
            {
                t_Angles.Add(m_Joints[i].transform.localRotation.x);
            } else if (m_Joints[i].GetComponent<TowerArmJoint>().Axis == new Vector3(0, 1, 0))
            {
                t_Angles.Add(m_Joints[i].transform.localRotation.y);
            } else if (m_Joints[i].GetComponent<TowerArmJoint>().Axis == new Vector3(0, 0, 1))
            {
                t_Angles.Add(m_Joints[i].transform.localRotation.z);
            }



        }
        //Debug.Log(t_Angles.Count);
        InverseKinematics(Target.transform.position, t_Angles);
        
    }


    
    public Vector3 ForwardKinematics(List<float> angles)
    {
        Vector3 prevPoint = m_Joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < m_Joints.Count; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], m_Joints[i - 1].GetComponent<TowerArmJoint>().Axis);
            Vector3 nextPoint = prevPoint + rotation * m_Joints[i].GetComponent<TowerArmJoint>().StartOffset;

            prevPoint = nextPoint;
        }
        return prevPoint;
    }
    

    public float DistanceFromTarget(Vector3 target, List<float> angles)
    {
        Vector3 point = ForwardKinematics(angles);
        return Vector3.Distance(point, target);
    }


    public float PartialGradient(Vector3 target, List<float> angles, int i)
    {
        // Saves the angle,
        // it will be restored later
        float angle = angles[i];

        // Gradient : [F(x+SamplingDistance) - F(x)] / h
        float f_x = DistanceFromTarget(target, angles);

        angles[i] += SamplingDistance;
        float f_x_plus_d = DistanceFromTarget(target, angles);

        float gradient = (f_x_plus_d - f_x) / SamplingDistance;

        // Restores
        angles[i] = angle;

        return gradient;
    }

    public void InverseKinematics(Vector3 target, List<float> angles)
    {
        for (int i = 0; i < m_Joints.Count; i++)
        {
            // Gradient descent
            // Update : Solution -= LearningRate * Gradient
            float gradient = PartialGradient(target, angles, i);
            Debug.Log(gradient);
            angles[i] -= LearningRate * gradient;
            if (m_Joints[i].GetComponent<TowerArmJoint>().Axis == new Vector3(1, 0, 0))
            { 
                
                m_Joints[i].transform.localEulerAngles = new Vector3(angles[i],0,0);
            } else if (m_Joints[i].GetComponent<TowerArmJoint>().Axis == new Vector3(0, 1, 0))
            {
                
                m_Joints[i].transform.localEulerAngles = new Vector3(0, angles[i], 0);
            } else if (m_Joints[i].GetComponent<TowerArmJoint>().Axis == new Vector3(0, 0, 1))
            {
                
                m_Joints[i].transform.localEulerAngles = new Vector3(0, 0, angles[i]);
            }
        
        }
        
    }


}
