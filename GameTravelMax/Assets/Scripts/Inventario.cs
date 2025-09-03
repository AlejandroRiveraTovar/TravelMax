using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Tooltip("Prefab de texto que se instancia para cada objeto eliminado.")]
    public TextMeshProUGUI itemPrefab;

    [Tooltip("Contenedor donde se añaden los textos.")]
    public Transform contentParent;

    private void Start()
    {
        // Buscar al GameManager en la escena
        GameManager gm = FindFirstObjectByType<GameManager>();

        if (gm == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
            return;
        }

        Dictionary<string, (int cantidad, int valorTotal)> inventoryCount = new Dictionary<string, (int, int)>();

        foreach (var obj in gm.eliminatedObjects)
        {
            if (inventoryCount.ContainsKey(obj.objectName))
            {
                inventoryCount[obj.objectName] = (
                    inventoryCount[obj.objectName].cantidad + 1,
                    inventoryCount[obj.objectName].valorTotal + obj.objectValue
                );
            }
            else
            {
                inventoryCount[obj.objectName] = (1, obj.objectValue);
            }
        }

        // Mostrar agrupado con valor total
        foreach (var kvp in inventoryCount)
        {
            TextMeshProUGUI newItem = Instantiate(itemPrefab, contentParent);
            newItem.text = $"{kvp.Key} x{kvp.Value.cantidad}  (Total: {kvp.Value.valorTotal})";
        }

    }
}
