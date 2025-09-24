using UnityEngine;

public class Flower : Plant // INHERITANCE
{
    Material[] materials;
    private Color blossom;

    void Awake()
    {
        growth = new Vector3(0.5f, 0.5f, 0.5f);
        materials = gameObject.GetComponent<Renderer>().materials;
        foreach (var mat in materials)
        {
            if (mat.name != "PlantMat (Instance)")
            {
                blossom = mat.color;
                mat.color = new Color(0.427f, 0.698f, 0.161f, 1f);
            }
        }
    }

    public override void Grow() // POLYMORPHISM
    {
        transform.localScale += growth;

        if (transform.localScale.x > 1)
        {
            transform.localScale = Vector3.one;
            foreach (var mat in materials)
            {
                if (mat.name != "PlantMat (Instance)")
                {
                    mat.color = blossom;
                }
            }
        }
    }
}
