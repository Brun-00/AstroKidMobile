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
            GameObject tempGO = new GameObject("TempAudio");
            tempGO.transform.position = transform.position;

            AudioSource tempSource = tempGO.AddComponent<AudioSource>();

            tempSource.clip = audioSource.clip;
            tempSource.volume = audioSource.volume;
            tempSource.pitch = Random.Range(0.6f, 1.4f);
            tempSource.spatialBlend = audioSource.spatialBlend;
            tempSource.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;

            tempSource.Play();

            Destroy(tempGO, audioSource.clip.length / tempSource.pitch);
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
