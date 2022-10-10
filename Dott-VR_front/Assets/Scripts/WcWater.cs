using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WcWater : MonoBehaviour
{
    [Tooltip("Effet de particule à lancer lors de la collision avec un objet grapable")]
    public ParticleSystem WcParticules;

    [Tooltip("Premier effet sonore à lanser lors de collision avec un objet grapable")]
    public AudioSource waterSound;

    [Tooltip("Second effet sonore à lanser lors de collision avec un objet grapable")]
    public AudioSource teleportMusic;

    [Tooltip("Panneau de controle gérant l'envoie lors de collision avec un objet grapable ")]
    public ControlPannel controlPannel;


    // Décleché lorsque le trigger du Collider est déclenché.
    private void OnTriggerEnter(Collider other)
    {
        // n'agit que si l'objet déclencheur est un object grapable.
        if(other.tag == "GrapableObject")
        {
            PlaySounds();
            WcParticules.Play();
            controlPannel.SendObject(other.gameObject );
        }

    }

    private void PlaySounds()
    {
        waterSound.Play();
        teleportMusic.Play();
    }
}
