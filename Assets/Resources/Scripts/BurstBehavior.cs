using UnityEngine;
using System.Collections;

public class BurstBehavior : MonoBehaviour {

    #region sprite
    public Sprite mPurpleSprite;
    public Sprite mYellowSprite;
    #endregion

    #region privateVar
    private float timeLived = 0;
    public float speed = 6;
    public float dacayRate = .9f;
    #endregion

    // Use this for initialization
    void Start () {

        // load objects
        var myRender = (SpriteRenderer)GetComponent<Renderer>();

        // load and set sprites        
        int randomInt = Random.Range (0, 2);
        myRender.sprite = (randomInt == 0) ? mPurpleSprite : mYellowSprite;
    }
    
    // Update is called once per frame
    void Update () {
      if (this.transform.localScale.x < .1f) {
          Destroy(this.gameObject);
      }
      transform.position += (speed * Time.smoothDeltaTime) * transform.up;
      this.transform.localScale *= dacayRate;
    }
}
