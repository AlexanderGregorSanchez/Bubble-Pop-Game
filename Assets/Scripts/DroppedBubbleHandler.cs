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

        float randomHorizontalSpeed = Random.Range(-30f, 30f);
        rb.velocity = new Vector2(randomHorizontalSpeed, 30);

        float randomAngularSpeed = Random.Range(-500f, 500f);
        rb.angularVelocity = randomAngularSpeed;

    }
}
