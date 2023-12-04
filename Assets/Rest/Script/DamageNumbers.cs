using TMPro;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    [SerializeField]
    private GameObject textMeshPrefab;

    public void PrintDamageNumber(int damage, Color color)
    {
        var spawnedText = Instantiate(textMeshPrefab);
        spawnedText.transform.position = transform.position;

        var number = spawnedText.GetComponent<NumberMovement>();

        number.SetNumber(damage, color);
        Destroy(spawnedText, 1.25f);
    }
}
