    using UnityEngine;

    public class PlayerAbilityInput : MonoBehaviour
    {
        [SerializeField] private AbilityController controller;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                controller.Use(0);
            }
        }
    }