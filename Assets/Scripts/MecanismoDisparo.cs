using UnityEngine;
using UnityEngine.InputSystem;

public class MecanismoDisparo : MonoBehaviour
{
    private Rigidbody rb;
    private bool jaDisparou = false;

    [Header("Configuração de Disparo Forte")]
    public Vector3 direcaoImpulso = new Vector3(0f, -0.5f, 5f); // Aumente o Z positivo (ou negativo dependendo do eixo da sua rampa)
    public float forcaImpulso = 8f; // Força bem mais forte para garantir o tranco inicial

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Começa travado
        }
    }

    void Update()
    {
        if (!jaDisparou && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Disparar();
        }
    }

    void Disparar()
    {
        if (rb != null)
        {
            rb.isKinematic = false; // Libera a física

            // Aplica um impulso forte na direção da rampa
            rb.AddForce(direcaoImpulso.normalized * forcaImpulso, ForceMode.Impulse);

            jaDisparou = true;
            Debug.Log("[Mecanismo 01] Cubo disparado com força total!");
        }
    }
}