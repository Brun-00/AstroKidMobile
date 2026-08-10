using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particlePrefab;
    public AudioSource audioSource;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag) || collision.transform.CompareTag("Aura"))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        OnCollect();
        if (audioSource != null && audioSource.clip != null)
        {
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

        }
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {

        if (particlePrefab != null)
        {
            ParticleSystem ps = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            ps.Play();

            Destroy(ps.gameObject, 5);
            
        }
    }
}
