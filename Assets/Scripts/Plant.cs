using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    private Vector3 _growth;
    public Vector3 growth
    {
        get { return _growth; }
        set { _growth = value; }
    }

    void Start()
    {
        gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }

    public virtual void Grow()
    {
        transform.localScale += _growth;

        if (transform.localScale.x > 1)
        {
            transform.localScale = Vector3.one;
        }
    }

    public virtual bool GetFullyGrown()
    {
        return transform.localScale == Vector3.one;
    }
}
