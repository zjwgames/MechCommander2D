using UnityEngine;

public class ClickDetector2D : MonoBehaviour
{
    int layer;
    LayerMask layerMask;

    private void Start()
    {
        layer = LayerMask.NameToLayer("ClickDetect");
        layerMask = 1 << layer;
    }

    void Update()
    {
        if (GetComponent<Player>().PlayerState == Player.State.Free)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    Debug.Log("Clicked on " + hit.collider.gameObject.name);
                    GetComponent<Player>().mech = hit.collider.gameObject.transform.GetChild(0).gameObject;
                }
            }
        }
    }
}