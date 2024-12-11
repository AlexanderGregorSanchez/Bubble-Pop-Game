using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteParticlesHandler : MonoBehaviour
{
    public GameObject particleSystemPrefab;

    public void SpawnParticle(Sprite sprite)
    {
        GameObject clone = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity, transform.parent);

        clone.name += $" {gameObject.name}";

        ParticleSystem p = clone.GetComponent<ParticleSystem>();

        p.textureSheetAnimation.SetSprite(0, sprite);

        
        p.Play();
    }
}
