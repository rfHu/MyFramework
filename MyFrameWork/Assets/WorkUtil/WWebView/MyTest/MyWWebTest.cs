using ICODES.STUDIO.WWebView;
using UnityEngine;
using UnityEngine.UI;

namespace WorkUtil
{
    public class MyWWebTest : MonoBehaviour
    {
        public WWebView webView = null;
        public Text status = null;
        public string url = "https://720.3vjia.com/S24693714";

        protected bool showFlag = false;

        protected void Awake()
        {
            webView.OnStartNavigation += OnStartNavigation;
            webView.OnNavigationCompleted += OnNavigationCompleted;
            webView.OnNavigationFailed += OnNavigationFailed;
            webView.OnReceiveMessage += OnReceiveMessage;
            webView.OnEvaluateJavaScript += OnEvaluateJavaScript;
            webView.OnClose += OnClose;

#if UNIWEBVIEW3_SUPPORTED
        webView.Initialize(new Vector4(0, margine, 0, 0), new Vector2(Screen.width, Screen.height - (margine * 2)));
#else
            webView.Initialize(new Vector4(0, 0, 0, 80), new Vector2(0, 0));
#endif

            var buttonParent = transform.Find("Sub");

            buttonParent.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
            {
                Navigate();
            });
            buttonParent.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
            {
                GoBack();
            });
            buttonParent.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            {
                GoForward();
            });
            buttonParent.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
            {
                Refresh();
            });
            buttonParent.GetChild(4).GetComponent<Button>().onClick.AddListener(() =>
            {
                Hide();
            });
        }

        #region webView注册事件
        protected virtual void OnStartNavigation(WWebView webView, string url)
        {
            if (status != null)
                status.text = "OnStartNavigation : " + url;
        }

        protected virtual void OnNavigationCompleted(WWebView webView, string data)
        {
            if (status != null)
                status.text = "OnNavigationCompleted : " + data;
        }

        protected virtual void OnNavigationFailed(WWebView webView, int code, string url)
        {
            if (status != null)
                status.text = "OnNavigationFailed : errorcode: " + code;
        }

        protected virtual void OnReceiveMessage(WWebView webView, string message)
        {
            if (status != null)
                status.text = "OnReceiveMessage : " + message;
        }

        protected virtual void OnEvaluateJavaScript(WWebView webView, string result)
        {
            if (status != null)
                status.text = "OnEvaluateJavaScript : " + result;
        }

        protected virtual bool OnClose(WWebView webView)
        {
            if (status != null)
                status.text = "OnClose";

#if UNITY_EDITOR
            // NOTE: Keep in mind that you are just watching the DEMO now.
            // Destroy webview instance not WWebView component.
            webView.Destroy();

            // Don't destroy WWebView component for this demo.
            // In most cases, you will return true to remove the component.
            return false;
#else
        // In "real" game, you will never quit your app since the webview is closed.
        UnityEngine.Application.Quit();
        return true;
#endif
        }
        #endregion

        public void Navigate()
        {
            Navigate(url);
        }

        public void Navigate(string url)
        {
            webView.Navigate(url);
            Show();
        }

        public void Refresh()
        {
            webView.Refresh();
        }

        public void GoBack()
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        public void GoForward()
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        public void Stop()
        {
            webView.Stop();
        }

        public void ShowToggle()
        {
            if (showFlag)
                Hide();
            else
                Show();
        }

        public void Show()
        {
            webView.Show();
            showFlag = true;
        }

        public void Hide()
        {
            webView.Hide();
            showFlag = false;
        }
    }
}