using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedBubbleHandler : MonoBehaviour
{
    public GameObject dropPrefab;

    public void SpawnParticle(Sprite sprite)
    {
        GameObject clone = Instantiate(dropPrefab, transform.position, Quaternion.identity, transform.parent);

        clone.GetComponentInChildren<Image>().sprite = sprite;

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.up * 30;
        rb.angularVelocity = 500;
    }
}
