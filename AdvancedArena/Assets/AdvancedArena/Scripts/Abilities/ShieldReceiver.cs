using UnityEngine;
using System.Collections;

public class ShieldReceiver : MonoBehaviour
{
    public bool IsShieldActive
    {
        get;
        private set;
    }

    public void Activate(float duration)
    {
        StopAllCoroutines();

        StartCoroutine(
            ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(
        float duration)
    {
        IsShieldActive = true;

        Debug.Log("Shield aktif.");

        yield return
            new WaitForSeconds(duration);

        IsShieldActive = false;

        Debug.Log("Shield kapandı.");
    }
}