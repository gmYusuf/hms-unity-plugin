using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiMobileServices.Analystics;
using HuaweiMobileServices.Utils;
using System;
using HuaweiMobileServices.Nearby.Discovery;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager GetInstance(string name = "AnalyticsManager") => GameObject.Find(name).GetComponent<AnalyticsManager>();

    private HiAnalyticsInstance instance;
    void InitilizeAnalyticsInstane()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");

        HiAnalyticsTools.EnableLog();
        instance = HiAnalytics.GetInstance(activity);
        instance.SetAnalyticsEnabled(true);

    }

    public void SendEventWithBundle(String eventID, String key, String value)
    {
        Bundle bundleUnity = new Bundle();
        bundleUnity.PutString(key, value);
        Debug.Log($"[HMS] : Analytics Kits Event Id:{eventID} Key:{key} Value:{value}");
        instance.OnEvent(eventID, bundleUnity);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitilizeAnalyticsInstane();
    }

    // Report an event upon app switching to the background.
    public void ReportEvent()
    {
        ReportPolicy moveBackgroundPolicy = ReportPolicy.ON_MOVE_BACKGROUND_POLICY;
        // Report an event at the specified interval.
        ReportPolicy scheduledTimePolicy = ReportPolicy.ON_SCHEDULED_TIME_POLICY;
        // Set the event reporting interval to 600 seconds.
        scheduledTimePolicy.Threshold = 600;
        ISet<ReportPolicy> reportPolicies = new HashSet<ReportPolicy>();
        // Add the ON_APP_LAUNCH_POLICY and ON_SCHEDULED_TIME_POLICY policies.
        reportPolicies.Add(scheduledTimePolicy);
        reportPolicies.Add(moveBackgroundPolicy);
        // Set the ON_MOVE_BACKGROUND_POLICY and ON_CACHE_THRESHOLD_POLICY policies.
        instance.SetReportPolicies(reportPolicies);
    }
}
