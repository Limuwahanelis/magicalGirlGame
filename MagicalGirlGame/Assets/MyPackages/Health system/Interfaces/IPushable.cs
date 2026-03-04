using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    void Push(PushInfo pushInfo);
    void LongPush(PushInfo pushInfo, ForceMode2D pushMode);
    void EndLongPush();
    void StartLongPush();
}
