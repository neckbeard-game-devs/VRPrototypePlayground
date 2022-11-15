using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class GameControl : MonoBehaviour
{
    public GameObject[,] sphereGrid;
    public GameObject[] sphereGridB;
    public GameObject SpherePrefab, SphereSpwnPnt, PlayerGo;
    public bool spawnSpheresBool, warpPlayer;
    public int width, height, m_score, m_highscore;
    [SerializeField]
    private int scoreInt, highScoreInt;
    
    public TMP_Text scoreText, highScoreText;
    public Collider[] warpCol;
    public Transform[] sphereSpwnPnts, warpTrans;
    private void Start()
    {
        highScoreInt = PlayerPrefs.GetInt("highScore");
        highScoreText.text = "" + highScoreInt;
        for (int i = 0; i < warpCol.Length; i++)
        {
            warpCol[i].gameObject.GetComponent<WarpCol>().tgc = this;
            warpCol[i].gameObject.GetComponent<WarpCol>().warpColInt = i;
        }

        // for setting up sphere spawn points
        //sphereGridB = GameObject.FindGameObjectsWithTag("spwnPnts");
        //sphereSpwnPnts = new Transform[sphereGridB.Length];

        //for (int i = 0; i < sphereGridB.Length; i++)
        //{
        //    sphereSpwnPnts[i] = sphereGridB[i].gameObject.transform;
        //}
    }
    void Update()
    {
        if (spawnSpheresBool)
        {
            spawnSpheresBool = false;

            // for initial sphere spwn points, use to add levels

            //sphereGrid = new GameObject[width, height];
            //for (int i = 0; i < width - 1; i+=2)
            //{
            //    for (int j = 0; j < height - 1; j+=2)
            //    {
            //        sphereGrid[i, j] = Instantiate(SpherePrefab, new Vector3(i, -2, j), transform.rotation);
            //        // for testing
            //        sphereGrid[i, j].transform.parent = SphereSpwnPnt.transform;
            //    }
            //}

            sphereGridB = new GameObject[sphereSpwnPnts.Length];
            for (int i = 0; i < sphereSpwnPnts.Length; i++)
            {
                sphereGridB[i] = Instantiate(SpherePrefab, sphereSpwnPnts[i].position, sphereSpwnPnts[i].rotation);
            }

            //StartCoroutine(StartCountdown());
        }
    }

    public void EnterGame()
    {
        SceneManager.LoadScene(1);
    }
    public void EatPellets()
    {
        scoreInt += 1;

        if(scoreInt >= highScoreInt)
        {
            highScoreInt = scoreInt;
            m_highscore = highScoreInt;
            highScoreText.text = "" + m_highscore;
            PlayerPrefs.SetInt("highScore", highScoreInt);
        }
        m_score = scoreInt;
        scoreText.text = "" + m_score;
    }
    public void ResetGame()
    {
        Debug.Log("ResetGame");

        scoreInt = 0;
        scoreText.text = "" + scoreInt;
        highScoreText.text = "" + highScoreInt;

        for (int i = 0; i < sphereGridB.Length; i++)
        {
            if (sphereGridB[i] != null)
            {
                Destroy(sphereGridB[i]);
            }
           
        }

        // for setup and testing
        //sphereGrid = new GameObject[width, height];

        //for (int i = 0; i < width - 1; i += 2)
        //{
        //    for (int j = 0; j < height - 1; j += 2)
        //    {
        //        if (sphereGrid[i, j] != null)
        //        {
        //            Destroy(sphereGrid[i, j]);
        //        }
        //    }

        //}
        spawnSpheresBool = true;
    }
    public void PacmanHit()
    {

    }

    public void WarpPlayer(int colInt)
    {
        warpPlayer = true;

        PlayerGo.transform.position = colInt switch
        {
            (0) => warpTrans[3].position,
            (1) => warpTrans[2].position,
            (2) => warpTrans[1].position,
            _ => warpTrans[0].position,
        };

        StartCoroutine(WarpCountdown());
    }
    public void WarpNpc()
    {
        StartCoroutine(WarpCountdown());
    }
    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1f);
       
        for (int i = 0; i < width - 1; i += 2)
        {
            for (int j = 0; j < height - 1; j += 2)
            {              
                // for testing
                if(sphereGrid[i, j] != null)
                {
                    if (!sphereGrid[i, j].GetComponent<SphereCol>().startBool)
                    {
                        Destroy(sphereGrid[i, j]);
                    }
                    else
                    {
                        sphereGrid[i, j].GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                 
            }
        }      
    }

    IEnumerator WarpCountdown()
    {
        if (warpPlayer)
        {
            warpPlayer = false;


        }
        else
        {

        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < warpCol.Length; i++)
        {
            warpCol[i].gameObject.GetComponent<WarpCol>().warpBool = false;
        }

    }


}
