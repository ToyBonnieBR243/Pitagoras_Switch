using UnityEngine;

public class SphereController : MonoBehaviour
{
    public Transform fimDaRampa;
    public float velocidade = 5f;
    public float distanciaDeImpacto = 0.1f;
    private bool impactoDetectado = false;

    void Update()
    {
        if (impactoDetectado) return;

        // Passo 1: A Direção
        Vector3 direcao = fimDaRampa.position - transform.position;

        // Passo 3: O Gatilho (Sem uso de raiz quadrada)
        // Elevamos a distância desejada ao quadrado para comparar com o sqrMagnitude
        if (direcao.sqrMagnitude <= (distanciaDeImpacto * distanciaDeImpacto))
        {
            Debug.Log("Impacto Detectado: Iniciando a Etapa 2!");
            impactoDetectado = true;
            return;
        }

        // Passo 2: Normalização e Movimentação
        transform.position += direcao.normalized * velocidade * Time.deltaTime;
    }
}