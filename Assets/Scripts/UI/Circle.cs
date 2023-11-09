using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    [SerializeField] private int segments;
    [SerializeField] private float xradius;
    [SerializeField] private float yradius;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Tower t;

    void Awake()
    {
        t = GetComponent<Tower>();
        xradius = yradius = t.GetRange()*2;
        CreatePoints();
    }

    public void ToggleRange()
    {
        line.enabled = !line.enabled;
    }

    public void Refresh()
    {
        xradius = yradius = t.GetRange()*2;
        line.positionCount = 0;
        CreatePoints();
    }

    private void CreatePoints()
    {
        line.useWorldSpace = false;
        line.positionCount = segments + 1;

        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}

