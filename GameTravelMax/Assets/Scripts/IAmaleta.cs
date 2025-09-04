using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAmaleta : MonoBehaviour
{
    public Transform Objetivo;
    public float Velocidad;
    public NavMeshAgent IA;

    void Update()
    {
        IA = GetComponent<NavMeshAgent>();
        IA.speed = Velocidad;
        IA.SetDestination(Objetivo.position);
    }
}
