using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class ManjaroMehanic : MonoBehaviour
    {
        [SerializeField] private Transform collectPosition;
        [SerializeField] private ComboUI comboUI;
        [SerializeField] private TextMeshProUGUI counter;

        [SerializeField] private SceneGeneration scneGeneration;

        private List<ChipData> _stackPrefabs = new List<ChipData>();
        private int countCollected;

        public void AddItemOnStack(ChipData chip)
        {
            if (_stackPrefabs.Count > chip.MaxCombo) FailStackChips();

            if (!CheckChipWithLastChip(chip))
            {
                return;
            }
            
            _stackPrefabs.Add(chip);
            comboUI.CreateNewItem(chip);
            
            if (_stackPrefabs.Count >= chip.MaxCombo) ReadCombo();
        }

        private void ReadCombo()
        {
            for (int i = 0; i < _stackPrefabs.Count - 1; i++)
            {
                if (!CheckChipWithLastChip(_stackPrefabs[i]))
                {
                    return;
                }
            }

            CollectedCombo();
        }

        private bool CheckChipWithLastChip(ChipData chip)
        {
            if (_stackPrefabs.Count < 1) return true;

            if (_stackPrefabs.Last().ItemName != chip.ItemName)
            {
                FailStackChips();
                return false;
            }
            
            return true;
        }
        
        private void CollectedCombo()
        {
            countCollected++;
            counter.text = countCollected.ToString();

            scneGeneration.DeleteChips(_stackPrefabs.Count, _stackPrefabs);
            
            for (int i = 0; i < _stackPrefabs.Count; i++)
            {
                Destroy(_stackPrefabs[i].gameObject);
            }
            
            ClearCollections();
        }

        private void ClearCollections()
        {
            _stackPrefabs.Clear();
            comboUI.ClearUI();
        }

        private void FailStackChips()
        {
            foreach (var item in _stackPrefabs)
            {
                item.ReturnItemOnBoard();
            }

            comboUI.ClearUI();
            _stackPrefabs.Clear();
        }
    }
}