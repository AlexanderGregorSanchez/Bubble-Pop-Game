using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedBubbleHandler : MonoBehaviour
{
    public GameObject dropPrefab;

    [Header("Particle Physics Data")]
    public int horizontalLaunchImpulse = 30;
    public int verticalLaunchImpulse = 30;
    
    public int maxAngularSpeed = 500;
    public int minAngularSpeed = 100;

    public void SpawnParticle(GameObject obj)
    {
        GameObject clone = Instantiate(dropPrefab, transform.position, Quaternion.identity, transform.parent);

        Image particleImage = clone.GetComponentInChildren<Image>();
        Image objImage = obj.GetComponentInChildren<Image>();

        particleImage.sprite = objImage.sprite;
        particleImage.color = objImage.color;

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        
        int randomHorizontalSpeed = Random.Range(-horizontalLaunchImpulse, horizontalLaunchImpulse);
        rb.velocity = new Vector2(randomHorizontalSpeed, verticalLaunchImpulse);

        int randomAngularSpeed = Random.Range(-maxAngularSpeed, maxAngularSpeed);

        if (Mathf.Abs(randomAngularSpeed) < minAngularSpeed) 
        { 
            randomAngularSpeed = (int)(minAngularSpeed * Mathf.Sign(randomAngularSpeed)); 
        }
        rb.angularVelocity = randomAngularSpeed;

    }
}
