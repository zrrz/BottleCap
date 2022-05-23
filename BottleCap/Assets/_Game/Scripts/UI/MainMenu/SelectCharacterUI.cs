using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterUI : MonoBehaviour
{
    [SerializeField] private Button nextCostumeButton;
    [SerializeField] private Button prevCostumeButton;
    [SerializeField] private PlayerAnimator playerAnimator;

    private int costumeIndex;

    void Start()
    {
        costumeIndex = UserManager.GetCostumeIndex();

        nextCostumeButton.onClick.AddListener(() =>
        {
            costumeIndex = (costumeIndex + 1) % playerAnimator.characterCostumes.Length;
            playerAnimator.SetCostume(costumeIndex);
            UserManager.SetCostumeIndex(costumeIndex);
        });

        prevCostumeButton.onClick.AddListener(() =>
        {
            costumeIndex--;
            if(costumeIndex < 0)
            {
                costumeIndex = playerAnimator.characterCostumes.Length - 1;
            }
            playerAnimator.SetCostume(costumeIndex);
            UserManager.SetCostumeIndex(costumeIndex);
        });
    }
}
