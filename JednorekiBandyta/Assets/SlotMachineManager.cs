using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour {
    #region vars

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text maxScoreText;

    [SerializeField]
    AudioClip won;

    [SerializeField]
    AudioSource audiosource;

    [SerializeField]
    Button startBtn;
    [SerializeField]
    Button resetBtn;

    private float [] speed = { 2100f, 2200f, 2300f };
    private bool [] spinning = { false, false, false };
    private int[] lastImgID = { 0, 0, 0 };

    private int score = 150;
    private int maxScore = 150;

    [SerializeField]
    private List<Node> RowOne = new List<Node>();
    [SerializeField]
    private List<Node> RowTwo = new List<Node>();
    [SerializeField]
    private List<Node> RowThree = new List<Node>();

    #endregion

    public void StartSpin() {
        spinning[0] = spinning[1] = spinning[2] = true;
        startBtn.interactable = false;
        resetBtn.interactable = false;
        speed[0] = 2100f;
        speed[1] = 2500f;
        speed[2] = 3020f;
    }

    private void StopSpin(int index) {
        spinning[index] = false;
        if (!spinning[0] && !spinning[1] && !spinning[2]) {
            startBtn.interactable = true;
            resetBtn.interactable = true;


            if (lastImgID[0] == lastImgID[1] && lastImgID[1] == lastImgID[2]) {
                score *= 5;
                audiosource.PlayOneShot(won);
            } else if (lastImgID[0] == lastImgID[1] || lastImgID[1] == lastImgID[2] || lastImgID[0] == lastImgID[2]) {
                score += 100;
                audiosource.PlayOneShot(won);
            } else {
                score -= 30;
            }

            Debug.Log(lastImgID[0] + " " + lastImgID[1] + " " + lastImgID[2]);

            if (score <= 0) {
                scoreText.text = "PRZEGRAŁEŚ";
                startBtn.interactable = false;
            } else {
                scoreText.text = score.ToString();
            }

            if(score > maxScore) {
                maxScore = score;
                maxScoreText.text = maxScore.ToString();
            }
        }

    }

    public void Reset() {
        spinning[0] = spinning[1] = spinning[2] = false;
        lastImgID[0] = lastImgID[1] = lastImgID[2] = 0;
        startBtn.interactable = true;
        score = 150;
        scoreText.text = score.ToString();

        foreach (Node node in RowOne) {
            node.ImgIndex = Random.Range(0, 8);
            node.Display();
        }
        foreach (Node node in RowTwo) {
            node.ImgIndex = Random.Range(0, 8);
            node.Display();
        }
        foreach (Node node in RowThree) {
            node.ImgIndex = Random.Range(0, 8);
            node.Display();
        }

    }



    private void Spin() {


        for (int rowID = 0; rowID < 3; rowID++) {

                speed[rowID] -= Time.deltaTime * 285f;

            if (speed[rowID] < 110.0f)
                speed[rowID] = 110.0f;
        }
        if (spinning[0]) {
            int i = 0;
            foreach (Node node in RowOne) {
                Transform tr = node.gameObject.transform;

                //jeżeli jest poza ekranem jest przenoszone na górę
                if (tr.localPosition.y <= -328) {

                    if (i < 3)
                        tr.localPosition = RowOne[i + 1].transform.localPosition + new Vector3(0.0f, 168.0f);
                    else
                        tr.localPosition = RowOne[0].transform.localPosition + new Vector3(0.0f, 168.0f);

                    node.ImgIndex = Random.Range(0, 8);
                    node.Display();
                    audiosource.Play();
                }

                tr.Translate(new Vector2(0, -speed[0] * Time.deltaTime));

                if (speed[0] == 110.0f && tr.localPosition.y < 1f && tr.localPosition.y > -1f) {
                    lastImgID[0] = node.ImgIndex;
                    StopSpin(0);

                    Debug.Log("lastImgID[0] " + node.ImgIndex);
                }

                i++;
            }
        }

        if (spinning[1]) {
            int i = 0;
            foreach (Node node in RowTwo) {
                Transform tr = node.gameObject.transform;

                //jeżeli jest poza ekranem jest przenoszone na górę
                if (tr.localPosition.y <= -328) {

                    if (i < 3)
                        tr.localPosition = RowTwo[i + 1].transform.localPosition + new Vector3(0.0f, 168.0f);
                    else
                        tr.localPosition = RowTwo[0].transform.localPosition + new Vector3(0.0f, 168.0f);

                    node.ImgIndex = Random.Range(0, 8);
                    node.Display();
                    audiosource.Play();
                }

                tr.Translate(new Vector2(0, -speed[1] * Time.deltaTime));

                if (speed[1] == 110.0f && tr.localPosition.y < 1f && tr.localPosition.y > -1f) {
                    lastImgID[1] = node.ImgIndex;
                    StopSpin(1);

                    Debug.Log("lastImgID[1] " + node.ImgIndex);
                }

                i++;
            }
        }

        if (spinning[2]) {
            int i = 0;
            foreach (Node node in RowThree) {
                Transform tr = node.gameObject.transform;

                //jeżeli jest poza ekranem jest przenoszone na górę
                if (tr.localPosition.y <= -328) {

                    if (i < 3)
                        tr.localPosition = RowThree[i + 1].transform.localPosition + new Vector3(0.0f, 168.0f);
                    else
                        tr.localPosition = RowThree[0].transform.localPosition + new Vector3(0.0f, 168.0f);

                    node.ImgIndex = Random.Range(0, 8);
                    node.Display();
                    audiosource.Play();
                }

                tr.Translate(new Vector2(0, -speed[2] * Time.deltaTime));

                if (speed[2] == 110.0f && tr.localPosition.y < 1f && tr.localPosition.y > -1f) {
                    lastImgID[2] = node.ImgIndex;
                    StopSpin(2);

                    Debug.Log("lastImgID[2] " + node.ImgIndex);
                }

                i++;
            }
        }
    }

        
    



    void Start () {
		foreach(Node node in RowOne) {
            node.ImgIndex = Random.Range(0, 8);
            node.Display();
        }
        foreach (Node node in RowTwo) {
            node.ImgIndex = Random.Range(0, 8);
            node.Display();
        }
        foreach (Node node in RowThree) {
            node.ImgIndex = Random.Range(0, 8);
            node.Display();
        }
    }
	

	void Update () {
        Spin();
	}
}
