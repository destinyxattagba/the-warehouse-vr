using UnityEngine;

public class VinylHighlight : MonoBehaviour
{
    public Renderer vinylRenderer;
    public Material normalMaterial;
    public Material highlightMaterial;

    void Start()
    {
        vinylRenderer.material = normalMaterial;
    }

    public void OnHoverEnter()
    {
        vinylRenderer.material = highlightMaterial;
    }

    public void OnHoverExit()
    {
        vinylRenderer.material = normalMaterial;
    }
}
