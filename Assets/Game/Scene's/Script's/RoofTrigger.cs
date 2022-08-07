using UnityEngine;

public class RoofTrigger : MonoBehaviour
{

    [SerializeField] private GameObject _roofTile;

    private bool isTrigger = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = !isTrigger;
        _roofTile.SetActive(isTrigger);
    }
}
