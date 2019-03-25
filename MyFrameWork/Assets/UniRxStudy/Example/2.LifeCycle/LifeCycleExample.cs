using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace UniRxStudy
{
    public class LifeCycleExample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Observable.EveryUpdate().Subscribe(_ => { Debug.Log("UniRx -Update"); });
            //Observable.EveryFixedUpdate().Subscribe(_ => { Debug.Log("UniRx -FixedUpdate"); });
            //Observable.EveryEndOfFrame().Subscribe(_ => { Debug.Log("UniRx -EndOfFrame"); });
            //Observable.EveryLateUpdate().Subscribe(_ => { Debug.Log("UniRx -LateUpdate"); });

            Observable.EveryUpdate()
                .Subscribe(_ => { Debug.Log("UniRx -Update"); })
                .AddTo(this);

            this.FixedUpdateAsObservable()
                .Subscribe(_ => { Debug.Log("UniRx -FixedUpdate"); });
        }
    }
}