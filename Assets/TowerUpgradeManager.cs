using UnityEngine;
using System.Collections;

/// <summary>
/// Determines the availabilty of upgrades based on tower selection and funds available etc.
/// Controls the presentation of the upgrade UI elements and handles the user selection thereof.
/// </summary>
public class TowerUpgradeManager : MonoBehaviour {


    public void MeleeUpgrade()
    {

    }

    void OnEnable()
    {
        Tower.onTowerTouched += TowerTouched;
    }

    void OnDisable()
    {
        Tower.onTowerTouched -= TowerTouched;
    }

    private void TowerTouched(Tower tower)
    {
        ///
    }

    private void showButtons(bool melee, bool ranged, bool slow)
    {

    }


}
