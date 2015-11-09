using UnityEngine;
using System.Collections;

public class BurstManager : MonoBehaviour {

    public GameObject mBurstPrefab;

    #region privateVar
    private float timeElapsed;
    private float duration = 3;
    #endregion

    // Update is called once per frame
    void Update () {
        timeElapsed++;
        if (timeElapsed > duration) {
            Destroy(this.gameObject);
        }
        makeBurstPoint ();
    }

    private void makeBurstPoint() {
        GameObject e = Instantiate(mBurstPrefab) as GameObject;
        BurstBehavior spawnedEnemy = e.GetComponent<BurstBehavior>();

        if(spawnedEnemy != null) {
          e.transform.position = this.transform.position;
          spawnedEnemy.transform.Rotate(new Vector3(0,0,Random.Range(0,360)));
        }
    }
}
