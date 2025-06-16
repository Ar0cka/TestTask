using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ManjaroMehanic manjaro;
        [SerializeField] private SceneGeneration sceneGeneration;
        
        [SerializeField] private LayerMask hitLayers;
        [SerializeField] private Camera playerCamera;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(playerCamera.transform.position.z);
                
                var hitPos = playerCamera.ScreenToWorldPoint(mousePos);
                
                var hit = Physics2D.OverlapPoint(hitPos, hitLayers);
                
                if (hit != null && hit.CompareTag("Chip"))
                {
                    var itemData = hit.GetComponent<ChipData>();
                    
                    if (!itemData.ChipClosed) 
                        manjaro.AddItemOnStack(itemData);
                    else
                        Debug.Log("Chip closed");
                }
            }
        }

        public void AutoLeveling()
        {
            for (int i = sceneGeneration.CurrentChipCount; i > 0; i++)
            {
                
            }
        }
    }
}