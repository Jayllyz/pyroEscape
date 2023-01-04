using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // La vitesse de déplacement de la plateforme
    public float speed = 1.0f;

    // La direction de déplacement de la plateforme (1 = droite/haut, -1 = gauche/bas)
    public int direction = 1;

    // La distance maximale que la plateforme peut parcourir dans la direction choisie
    public float distance = 10.0f;

    // La position de départ de la plateforme
    private Vector3 startPosition;

    void Start()
    {
        // Enregistre la position de départ de la plateforme
        startPosition = transform.position;
    }

    void Update()
    {
        // Déplace la plateforme dans la direction choisie
        transform.position = transform.position + (Vector3.up * direction * speed * Time.deltaTime);
        

        // Vérifie si la plateforme a atteint la distance maximale de déplacement
        if (Mathf.Abs(transform.position.x - startPosition.x) >= distance)
        {
            // Inverse la direction de déplacement de la plateforme
            direction *= -1;
        }
    }
}