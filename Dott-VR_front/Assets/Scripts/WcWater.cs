using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WcWater : MonoBehaviour
{
    [Tooltip("Effet de particule � lancer lors de la collision avec un objet grapable")]
    public ParticleSystem WcParticules;

    [Tooltip("Premier effet sonore � lanser lors de collision avec un objet grapable")]
    public AudioSource waterSound;

    [Tooltip("Second effet sonore � lanser lors de collision avec un objet grapable")]
    public AudioSource teleportMusic;

    [Tooltip("Panneau de controle g�rant l'envoie lors de collision avec un objet grapable ")]
    public ControlPannel controlPannel;


    // D�clech� lorsque le trigger du Collider est d�clench�.
    private void OnTriggerEnter(Collider other)
    {
        // n'agit que si l'objet d�clencheur est un object grapable.
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
