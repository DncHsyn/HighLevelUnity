using UnityEngine;

public class PlayerAbilityInput : MonoBehaviour
{
    [SerializeField]
    private AbilityController controller;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controller.Use(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controller.Use(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
       {
         controller.Use(2);
       }

       if (Input.GetKeyDown(KeyCode.Alpha4))
       {
        controller.Use(3);
       }
    }
}