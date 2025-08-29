using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// Controla el movimiento del jugador en un entorno 3D y la interacción
/// con objetos "Pickable" (agarrar, soltar y mover con fuerza hacia un punto de agarre).
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    [Tooltip("Velocidad de movimiento del jugador.")]
    public float speed = 5f;

    private Vector2 move;                           // Entrada de movimiento del jugador
    private Vector3 lastDirection = Vector3.forward; // Última dirección válida (por defecto en +Z)

    [Header("Interacción")]
    [Tooltip("Distancia máxima para detectar objetos que se pueden agarrar.")]
    public float pickDistance = 3f;

    [Tooltip("Script que detecta si hay un objeto en el área de agarre.")]
    public CanHold holdPointSC;

    [Tooltip("Objeto actualmente en rango para agarrar.")]
    public GameObject holdableObj = null;

    [Tooltip("Fuerza con la que el objeto se acerca al punto de agarre.")]
    public float pickForce = 50f;

    private bool canGrab;                           // Indica si hay un objeto disponible para agarrar

    [SerializeField, Header("Punto de agarre")]
    [Tooltip("Transform vacío donde se coloca el objeto agarrado.")]
    private Transform holdPoint;

    private GameObject heldObject = null;           // Objeto actualmente agarrado

    /// <summary>
    /// Lee el input de movimiento proveniente del Input System.
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        MovePlayer();

        // Actualizamos estado de interacción con objetos
        canGrab = holdPointSC.canHold;
        holdableObj = holdPointSC.holdableObject;

        if (heldObject != null)
        {
            MoveObject();
        }
    }

    /// <summary>
    /// Mueve al jugador según la entrada y mantiene la última dirección de rotación.
    /// </summary>
    private void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement.magnitude > 0.01f) // Si hay movimiento
        {
            lastDirection = movement; // Actualizamos la última dirección válida

            // Rotación hacia la dirección de movimiento
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(lastDirection),
                0.15f
            );

            // Movimiento en el mundo
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // Si no hay movimiento, mantenemos la última rotación
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(lastDirection),
                0.15f
            );
        }
    }

    /// <summary>
    /// Acción de interacción (Input System).
    /// Agarra un objeto disponible o suelta el objeto actual.
    /// </summary>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (heldObject == null && canGrab)
            {
                PickObject(holdableObj);
            }
            else if (heldObject != null)
            {
                DropObject();
            }
        }
    }

    /// <summary>
    /// Agarra un objeto con Rigidbody y lo asigna al punto de agarre.
    /// </summary>
    private void PickObject(GameObject pickObject)
    {
        Rigidbody rb = pickObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        rb.useGravity = false;
        rb.linearDamping = 10f;
        rb.angularDamping = 10f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        rb.transform.parent = holdPoint;
        heldObject = pickObject;
    }

    /// <summary>
    /// Suelta el objeto actualmente agarrado y restaura sus propiedades físicas.
    /// </summary>
    private void DropObject()
    {
        if (heldObject == null) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.linearDamping = 0f;
            rb.angularDamping = 0.05f;
            rb.constraints = RigidbodyConstraints.None;

            rb.transform.parent = null;
        }

        heldObject = null;
    }

    /// <summary>
    /// Aplica fuerza al objeto agarrado para mantenerlo en el punto de agarre.
    /// </summary>
    private void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdPoint.position) > 0.1f)
        {
            Vector3 direction = (holdPoint.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(direction * pickForce);
        }
    }
}
