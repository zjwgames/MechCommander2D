using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOutline : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    void Start()
    {
        outlineRenderer = GenerateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
    }

    Renderer GenerateOutline(Material outlineMat, float scaleFactor, Color color)
    {

        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        SpriteRenderer rend = outlineObject.GetComponent<SpriteRenderer>();
        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_ScaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        outlineObject.GetComponent<CreateOutline>().enabled = false;
        outlineObject.GetComponent<BoxCollider2D>().enabled = false;
        rend.enabled = false;

        return rend;
    }

    private void OnMouseEnter()
    {
        outlineRenderer.enabled = true;
        Debug.Log("OUTLINE");
    }

    //private void OnMouseOver()
    //{
    //    transform.Rotate(Vector3.up, 1f, Space.World);
    //}

    private void OnMouseExit()
    {
        outlineRenderer.enabled = false;
    }
}