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
        // Collect the item when the player or gather aura enters the trigger.
        if (
            collision.transform.CompareTag(compareTag) ||
            collision.transform.CompareTag("Aura")
        )
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        // Run the collection-specific behavior.
        OnCollect();

        if (audioSource != null && audioSource.clip != null)
        {
            // Create a temporary audio source so the sound can finish after the item disappears.
            GameObject tempGO =
                new GameObject("TempAudio");

            tempGO.transform.position =
                transform.position;

            AudioSource tempSource =
                tempGO.AddComponent<AudioSource>();

            tempSource.clip = audioSource.clip;
            tempSource.volume = audioSource.volume;
            tempSource.pitch =
                Random.Range(0.6f, 1.4f);
            tempSource.spatialBlend =
                audioSource.spatialBlend;
            tempSource.outputAudioMixerGroup =
                audioSource.outputAudioMixerGroup;

            tempSource.Play();

            // Destroy the temporary audio object after the sound finishes.
            Destroy(
                tempGO,
                audioSource.clip.length / tempSource.pitch
            );
        }

        // Disable the collected item.
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (particlePrefab != null)
        {
            // Spawn and play the collection particle effect.
            ParticleSystem ps =
                Instantiate(
                    particlePrefab,
                    transform.position,
                    Quaternion.identity
                );

            ps.Play();

            // Remove the particle effect after a few seconds.
            Destroy(ps.gameObject, 5);
        }
    }
}