using UnityEngine;

public class VisibilityTest : MonoBehaviour
{
    [SerializeField] VariablesTest vt;

    private void Start()
    {   // "!=" significa diverso
        if(vt != null)
        {
            Debug.Log($"{vt.shopIndex}");
        }

        vt.setShopIndexValue(3);
    }
}