using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(UITable), true)]
public class UITableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UITable myTarget = (UITable)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Update"))
        {
            myTarget.UpdateTable(null);
        }
    }
}
#endif

public class UITable : MonoBehaviour
{
    [Tooltip("How much space should there be between items?")]
    public float offset;

    [Tooltip("Add new the new items below existing items.")]
    public bool down;

    public void UpdateTable(GameObject newItem)
    {
        if (newItem != null) newItem.GetComponent<RectTransform>().localScale = Vector3.one;

        // Place all children at the same position
        Vector2 fixedPosition = Vector2.zero;
        fixedPosition.y = 0; // Set this to the desired Y position
        fixedPosition.x = 0; // Set this to the desired X position

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform child = transform.GetChild(i).GetComponent<RectTransform>();
            child.anchoredPosition = fixedPosition;
        }
    }
}
