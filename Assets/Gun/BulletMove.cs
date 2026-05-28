using UnityEngine;

public class BulletMove : MonoBehaviour {

    //void OnBecameInvisible()
    //{
    //    //Destroy(this.gameObject);
    //    this.gameObject.SetActive(false);
    //}
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }


    void Update()
    {
        this.transform.Translate(0, 0, 0.5f);
    }
}
