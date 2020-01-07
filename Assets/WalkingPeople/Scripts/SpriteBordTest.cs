using UnityEngine;

public class SpriteBordTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sprite = GetComponent<SpriteRenderer>().sprite.border;
        var size = GetComponent<SpriteRenderer>().size;
        var extentsSprite = GetComponent<SpriteRenderer>().sprite.bounds.extents;

        Debug.Log(sprite + "    " + size + "   " + extentsSprite);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
