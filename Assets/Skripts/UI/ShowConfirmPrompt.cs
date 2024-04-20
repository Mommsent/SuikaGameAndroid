using System.Collections;
using UnityEngine;

public class ShowConfirmPrompt : MonoBehaviour
{
    [Header("Confirmetion")]
    [SerializeField] private GameObject _comfirmationPrompt; 

    public void ShowConfirmMassage()
    {
        StartCoroutine(ConfirmationPrompt());
    }

    private IEnumerator ConfirmationPrompt()
    {
        _comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _comfirmationPrompt.SetActive(false);
    }
}
