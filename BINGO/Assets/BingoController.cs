using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BingoController : MonoBehaviour
{
    public List<int> num = new List<int>();
    List<int> oldnum = new List<int>(); 
    //一度出た値が出ないようにする仕組み（oldnumに出た数字を保存している。)

    int bingoCount = 0;
    //今何回ビンゴを行ったか
    int Limited = 75;
    //ビンゴの上限数
    int holizonMax = 10;
    //どれだけ横にビンゴ表示をするか

    GameObject Bingo;
    //今出たビンゴの数字表示
    GameObject BingoResult;
    //出たビンゴの履歴表示(現在未使用)
    GameObject Canvas;
    //使うキャンバス用
    GameObject AudioController;
    //SE再生用オブジェクト
    public GameObject numberObjectPrefab;
    //履歴に表示させるイメージ

    bool isShow;
    //ランダム表示をさせるか
    float timer;
    //ランダム表示用のタイマー
    float span = 0.15f;
    //ランダム表示の間隔
    int RandomCount = 0;
    //ランダム表示の回数カウント
    int RandomSpan = 13;
    //ランダム表示の最大値
    //注意:この値を変更するとSEと実際の動作にズレが生じます。(SEとして使用しているドラムロールが、2音源の一体型であるため)

    Color chandecolor;
    //色変更
    string htmlColor;
    //HTMLから変化用

    // Start is called before the first frame update
    void Start()
    {
        Bingo = GameObject.Find("Bingo");
        BingoResult = GameObject.Find("BingoResult");
        Canvas = GameObject.Find("Canvas");
        AudioController = GameObject.Find("AudioController");
        //それぞれのゲームオブジェクトのアタッチ作業

        int xPos;
        int yPos = 595;
        //既出番号生成用の下準備。

        for(int i = 0; i < Limited; i++){

            xPos = -330 + 135 * (i % 10);

            //[holizonMax]回横に生成した後、改行する操作
            if(i % holizonMax == 0){
                yPos -= 130;            
            }
            

            GameObject Create = Instantiate(numberObjectPrefab, Vector2.zero, Quaternion.identity) as GameObject;
            Create.transform.parent = Canvas.transform;
            int numText = i + 1;
            chandecolor = ChangeColor(i);
            Create.name = numText.ToString("D2");
            Create.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            Create.GetComponent<Image>().color = chandecolor;

            Transform Text = Create.transform.Find("Text");
            Text.gameObject.GetComponent<NumberController>().number = numText;
            Create.SetActive(false);
            
        }
        
        for (int i = 0; i < Limited; i++){
            int rand;

            do{
                rand = Random.Range(0, Limited) + 1;
            }while(oldnum.Contains(rand));
            oldnum.Add(rand);
            num.Add(rand);
        }
    }

    public void OpenNumber(){
        if(bingoCount < Limited){
            string str = num[bingoCount].ToString("D2");
            Transform hitNumber = Canvas.transform.Find(str);
            hitNumber.gameObject.SetActive(true);

            Bingo.GetComponent<Text>().text = num[bingoCount].ToString("D2");

            bingoCount++;
        }
    }

    public void NextNumber(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed && !isShow){
            isShow = true;
            AudioController.GetComponent<AudioController>().PlayedSound();
       }
       //Debug.Log(bingoCount);
    }

    public void BackNumber(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            if(bingoCount > 1){
                bingoCount--;

                string str = num[bingoCount].ToString("D2");
                Transform hitNumber = Canvas.transform.Find(str);
                hitNumber.gameObject.SetActive(false);

                Bingo.GetComponent<Text>().text = num[bingoCount - 1].ToString("D2");
            }
        }
        //Debug.Log(bingoCount);
    }

    public void BingoEnd(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            SceneManager.LoadScene("StartScenes");
        }
    }

    Color ChangeColor(int i){
        
        Color rgb;
        switch(i / 15){

            case 0:
                htmlColor = "#989898";
                break;
            case 1:
                htmlColor = "#0ABF19";
                break;
            case 2:
                htmlColor = "#62AAFF";
                break;
            case 3:
                htmlColor = "#ee82ee";
                break;
            case 4:
                htmlColor = "#ff7575";
                break;
            case 5:
                htmlColor = "#adff2f";
                break;
            case 6:
                htmlColor = "#ffdab9";
                break;
            case 7:
                htmlColor = "#90ee90";
                break;
            case 8:
                htmlColor = "#dcdcdc";
                break;
            case 9:
                htmlColor = "#000000";
                break;
            case 10:
                htmlColor = "#000000";
                break;
        }

        if (ColorUtility.TryParseHtmlString(htmlColor, out rgb)){
            // Color型への変換成功（colorにColor型の赤色が代入される）
        } else {
            // Color型への変換失敗（colorはColor型の初期値のまま）
        }

        return rgb;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(isShow){
            
            if(timer >= span && RandomCount <= RandomSpan - 1){
            timer = 0;    
            int rand = Random.Range(0, Limited) + 1;
            Bingo.GetComponent<Text>().text = rand.ToString("D2");
            RandomCount++;
            Debug.Log(RandomCount);
            }
            
            if(RandomCount >= RandomSpan && timer >= 0.8f){
                timer = 0;
                RandomCount = 0;
                isShow = false;
                OpenNumber();
            }
            timer += Time.deltaTime;
        }
    }
}
