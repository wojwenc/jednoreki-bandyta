using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class Node : MonoBehaviour {
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();
    private int imgIndex;

    public Node(int _imgIndex) {
        ImgIndex = _imgIndex;
    }

    public int ImgIndex {
        get {
            return imgIndex;
        }

        set {
            imgIndex = value;
        }
    }

    public void Display() {
        GetComponent<Image>().sprite = sprites[imgIndex];
    }
}
