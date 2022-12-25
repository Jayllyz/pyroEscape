using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlatform : MonoBehaviour
{
    // La vitesse à laquelle la plateforme monte
    public float riseSpeed = 1.0f;

    // La hauteur maximale à laquelle la plateforme peut monter
    public float maxHeight = 10.0f;

    void Update()
    {
        // On calcule la nouvelle position de la plateforme en utilisant la vitesse de montée et le temps écoulé depuis la dernière frame
        float newY = transform.position.y + riseSpeed * Time.deltaTime;

        // On s'assure que la plateforme ne dépasse pas la hauteur maximale
        newY = Mathf.Min(newY, maxHeight);

        // On met à jour la position de la plateforme
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
