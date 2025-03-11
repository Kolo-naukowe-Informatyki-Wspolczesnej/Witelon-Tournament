using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public string tag;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject[] gates = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject go in gates)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer gateSpriteRenderer = go.GetComponent<SpriteRenderer>();
            Collider2D gateCollider = go.GetComponent<Collider2D>();

            if (collision.CompareTag("Player"))
            {
                if (Mathf.Approximately(spriteRenderer.color.a, 1f))
                {
                    Color newColor = spriteRenderer.color;
                    newColor.a = 0.25f;
                    spriteRenderer.color = newColor;
                }
                else
                {
                    Color newColor = spriteRenderer.color;
                    newColor.a = 1f;
                    spriteRenderer.color = newColor;
                }

                gateCollider.enabled = !gateCollider.enabled;
                if (Mathf.Approximately(gateSpriteRenderer.color.a, 1f))
                {
                    Color newColor = gateSpriteRenderer.color;
                    newColor.a = 0.25f;
                    gateSpriteRenderer.color = newColor;
                }
                else
                {
                    Color newColor = gateSpriteRenderer.color;
                    newColor.a = 1f;
                    gateSpriteRenderer.color = newColor;
                }
            }
        }
    }
}
