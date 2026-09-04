using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SphereDynamics : MonoBehaviour
{
    public Transform fimDaRampa;
    public float massa = 1f;
    public float gravidade = 9.8f;
    public float anguloRampaGraus = 30f; // Ajuste para a inclinação da sua rampa (Theta)
    public float forcaImpulsoInicial = 5f;

    private float velocidadeAtual = 0f;
    private bool emMovimento = false;
    private Vector3 direcaoNormalizada;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Restrição do projeto: Física e gravidade nativas devem ser desativadas
        rb.isKinematic = true;

        // Calculamos a direção exata uma única vez no Start
        direcaoNormalizada = (fimDaRampa.position - transform.position).normalized;

        // Impulso inicial aplicado uma única vez (tira da inércia e inicia movimento)
        // Pela 2ª Lei de Newton, a variação instantânea de velocidade gerada pelo impulso é F / m
        velocidadeAtual += (forcaImpulsoInicial / massa);
        emMovimento = true;
    }

    void Update()
    {
        if (!emMovimento) return;

        // Converter o ângulo para radianos para usar nas funções trigonométricas da Unity
        float anguloRadianos = anguloRampaGraus * Mathf.Deg2Rad;

        // Calcular a Força Peso relativa à inclinação (P = m * g)
        float forcaPeso = massa * gravidade;

        // Calcular a Força Normal
        // A componente perpendicular do peso é P * cos(theta). 
        // A Força Normal calculada anula essa componente perfeitamente.
        float forcaNormal = forcaPeso * Mathf.Cos(anguloRadianos);

        // A Força Resultante que gera o movimento é a componente paralela à rampa: P * sin(theta)
        float forcaResultante = forcaPeso * Mathf.Sin(anguloRadianos);

        // 2ª Lei de Newton: a = F / m
        float aceleracao = forcaResultante / massa;

        // Acelerar a esfera ladeira abaixo (MRUV)
        velocidadeAtual += aceleracao * Time.deltaTime;
        transform.position += direcaoNormalizada * velocidadeAtual * Time.deltaTime;
    }

    // Detecção de colisão no fim da rampa usando Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FimDaRampa"))
        {
            emMovimento = false;
            velocidadeAtual = 0f;
            Debug.Log("Impacto Detectado: A esfera parou pelo gatilho!");
        }
    }
}