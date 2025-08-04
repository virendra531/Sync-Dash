using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material[] materials;
    float value = 1f;
    float startTime;
    public float duration = 0.5f;
    bool startDissolving = false;
    void Start()
    {
        materials = GetComponent<Renderer>().materials;
        materials[0].SetFloat("_Dissolve", 0f);
    }

    void Update()
    {
        if (!startDissolving) return;

        float t = (Time.time - startTime) / duration;
        value = Mathf.Lerp(0, 1, t);
        value = Mathf.Clamp01(value);
        materials[0].SetFloat("_Dissolve", value);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startTime = Time.time;
            materials[0].SetFloat("_Dissolve", 0f);
            startDissolving = true;
        }
    }
}
