
using TMPro;
using UnityEngine;

public class NumberMovement : MonoBehaviour
{
    private TextMeshPro textMeshPro;

    float duration = 1.0f;
    float time = 0.0f;

    float speed = 1.0f;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.localPosition + Vector3.forward * Time.deltaTime * speed;

        time += Time.deltaTime;

        if (time >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void SetNumber(int damage, Color color)
    {
        textMeshPro.color = color;
        textMeshPro.text = damage.ToString();
    }
}
