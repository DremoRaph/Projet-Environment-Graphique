using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatableData : ScriptableObject
{

    public event System.Action OnvaluesUpdated;
    public bool autoUpdate;

    protected virtual void OnValidate()
    {
        if (autoUpdate)
        {
            UnityEditor.EditorApplication.update += NotifyOfUpadtedValues;
        }
    }

    public void NotifyOfUpadtedValues()
    {
        UnityEditor.EditorApplication.update -= NotifyOfUpadtedValues;
        if (OnvaluesUpdated != null)
        {
            OnvaluesUpdated();
        }
    }

}
