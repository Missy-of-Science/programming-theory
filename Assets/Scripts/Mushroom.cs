using UnityEngine;

public class Mushroom : Plant // INHERITANCE
{
    void Awake()
    {
        growth = new Vector3(0.334f, 0.334f, 0.334f);
    }
}
