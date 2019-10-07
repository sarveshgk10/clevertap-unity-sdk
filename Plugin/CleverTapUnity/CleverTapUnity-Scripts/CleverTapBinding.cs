using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CleverTap.Utilities;

/// <summary>
/// These methods can be called by Unity applications to record
/// events and set and get user profile attributes.
/// </summary>

namespace CleverTap {
  public class CleverTapBinding : MonoBehaviour {
    public const string Version = "1.1.2";

#if UNITY_IOS
    void Start() {
        Debug.Log("Start: CleverTap binding for iOS.");
    }

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_launchWithCredentials(string accountID, string token);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_launchWithCredentialsForRegion(string accountID, string token, string region);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_onUserLogin(string properties);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profilePush(string properties);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profilePushGraphUser(string user);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profilePushGooglePlusUser(string user);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string CleverTap_profileGet(string key);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string CleverTap_profileGetCleverTapAttributionIdentifier();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string CleverTap_profileGetCleverTapID();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profileRemoveValueForKey(string key);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profileSetMultiValuesForKey(string key, string[] array, int size);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profileAddMultiValuesForKey(string key, string[] array, int size);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profileRemoveMultiValuesForKey(string key, string[] array, int size);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profileAddMultiValueForKey(string key, string val);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_profileRemoveMultiValueForKey(string key, string val);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_recordScreenView(string screenName);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_recordEvent(string eventName, string properties);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_recordChargedEventWithDetailsAndItems(string details, string items);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_setOffline(bool enabled);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_setOptOut(bool enabled);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_enableDeviceNetworkInfoReporting(bool enabled);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerPush();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_setApplicationIconBadgeNumber(int num);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_setDebugLevel(int level);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_enablePersonalization();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_disablePersonalization();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_setLocation(double lat, double lon);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_eventGetFirstTime(string eventName);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_eventGetLastTime(string eventName);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_eventGetOccurrences(string eventName);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string CleverTap_userGetEventHistory();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string CleverTap_sessionGetUTMDetails();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_sessionGetTimeElapsed();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string CleverTap_eventGetDetail(string eventName);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_userGetTotalVisits();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_userGetScreenCount();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_userGetPreviousVisitTime();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_pushInstallReferrerSource(string source, string medium, string campaign);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_showAppInbox(string styleConfig);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_getInboxMessageCount();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_getInboxMessageUnreadCount();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_initializeInbox();

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerStringVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerIntegerVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerDoubleVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerBooleanVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerMapOfStringVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerMapOfIntegerVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerMapOfDoubleVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerMapOfBooleanVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerListOfStringVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerListOfIntegerVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerListOfDoubleVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CleverTap_registerListOfBooleanVariable(string name);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string CleverTap_getStringVariable(string name, string defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern double CleverTap_getDoubleVariable(string name, double defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern int CleverTap_getIntegerVariable(string name, int defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool CleverTap_getBooleanVariable(string name, bool defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getMapOfBooleanVariable(string name, Dictionary defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getMapOfStringVariable(string name, Dictionary defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getMapOfIntegerVariable(string name, Dictionary defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getMapOfDoubleVariable(string name, Dictionary defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getListOfBooleanVariable(string name, List defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getListOfStringVariable(string name, List defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getListOfIntegerVariable(string name, List defaultValue);

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern  CleverTap_getListOfDoubleVariable(string name, List defaultValue);

    public static void LaunchWithCredentials(string accountID, string token) {
        CleverTap_launchWithCredentials(accountID, token);
    }

    public static void LaunchWithCredentialsForRegion(string accountID, string token, string region) {
        CleverTap_launchWithCredentialsForRegion(accountID, token, region);
    }

    public static void OnUserLogin(Dictionary<string, string> properties) {
        var propertiesString = Json.Serialize(properties);
        CleverTap_onUserLogin(propertiesString);
    }

    public static void ProfilePush(Dictionary<string, string> properties) {
        var propertiesString = Json.Serialize(properties);
        CleverTap_profilePush(propertiesString);
    }

    public static void ProfilePushGraphUser(Dictionary<string, string> user) {
        var userString = Json.Serialize(user);
        CleverTap_profilePushGraphUser(userString);
    }

    public static void ProfilePushGooglePlusUser(Dictionary<string, string> user) {
        var userString = Json.Serialize(user);
        CleverTap_profilePushGooglePlusUser(userString);
    }

    public static string ProfileGet(string key) {
        string ret = CleverTap_profileGet(key);
        return ret;
    }

    public static string ProfileGetCleverTapAttributionIdentifier() {
        string ret = CleverTap_profileGetCleverTapAttributionIdentifier();
        return ret;
    }

    public static string ProfileGetCleverTapID() {
        string ret = CleverTap_profileGetCleverTapID();
        return ret;
    }

    public static void ProfileRemoveValueForKey(string key) {
        CleverTap_profileRemoveValueForKey(key);
    }

    public static void ProfileSetMultiValuesForKey(string key, List<string> values) {
        CleverTap_profileSetMultiValuesForKey(key, values.ToArray(), values.Count);
    }

    public static void ProfileAddMultiValuesForKey(string key, List<string> values) {
        CleverTap_profileAddMultiValuesForKey(key, values.ToArray(), values.Count);
    }

    public static void ProfileRemoveMultiValuesForKey(string key, List<string> values) {
        CleverTap_profileRemoveMultiValuesForKey(key, values.ToArray(), values.Count);
    }

    public static void ProfileAddMultiValueForKey(string key, string val) {
        CleverTap_profileAddMultiValueForKey(key, val);
    }

    public static void ProfileRemoveMultiValueForKey(string key, string val) {
        CleverTap_profileRemoveMultiValueForKey(key, val);
    }

    public static void RecordScreenView(string screenName) {
        CleverTap_recordScreenView(screenName);
    }

    public static void RecordEvent(string eventName) {
        CleverTap_recordEvent(eventName, null);
    }

    public static void RecordEvent(string eventName, Dictionary<string, object> properties) {
        var propertiesString = Json.Serialize(properties);
        CleverTap_recordEvent(eventName, propertiesString);
    }

    public static void RecordChargedEventWithDetailsAndItems(Dictionary<string, object> details, List<Dictionary<string, object>>items) {
        var detailsString = Json.Serialize(details);
        var itemsString = Json.Serialize(items);
        CleverTap_recordChargedEventWithDetailsAndItems(detailsString, itemsString);
    }

    public static int EventGetFirstTime(string eventName) {
        return CleverTap_eventGetFirstTime(eventName);
    }

    public static int EventGetLastTime(string eventName) {
        return CleverTap_eventGetLastTime(eventName);
    }

    public static int EventGetOccurrences(string eventName) {
        return CleverTap_eventGetOccurrences(eventName);
    }

    public static JSONClass UserGetEventHistory() {
        string jsonString = CleverTap_userGetEventHistory();
        JSONClass json;
        try {
            json = (JSONClass)JSON.Parse(jsonString);
        } catch {
            Debug.Log("Unable to parse user event history json");
            json = new JSONClass();
        }
        return json;
    }

    public static JSONClass SessionGetUTMDetails() {
        string jsonString = CleverTap_sessionGetUTMDetails();
        JSONClass json;
        try {
            json = (JSONClass)JSON.Parse(jsonString);
        } catch {
            Debug.Log("Unable to parse session utm details json");
            json = new JSONClass();
        }
        return json;
    }

    public static int SessionGetTimeElapsed() {
        return CleverTap_sessionGetTimeElapsed();
    }

    public static JSONClass EventGetDetail(string eventName) {
        string jsonString = CleverTap_eventGetDetail(eventName);
        JSONClass json;
        try {
            json = (JSONClass)JSON.Parse(jsonString);
        } catch {
            Debug.Log("Unable to parse event detail json");
            json = new JSONClass();
        }
        return json;
    }

    public static int UserGetTotalVisits() {
        return CleverTap_userGetTotalVisits();
    }

    public static int UserGetScreenCount() {
        return CleverTap_userGetScreenCount();
    }

    public static int UserGetPreviousVisitTime() {
        return CleverTap_userGetPreviousVisitTime();
    }

    public static void RegisterPush() {
        CleverTap_registerPush();
    }

    public static void SetApplicationIconBadgeNumber(int num) {
        CleverTap_setApplicationIconBadgeNumber(num);
    }

    public static void SetDebugLevel(int level) {
        CleverTap_setDebugLevel(level);
    }

    public static void EnablePersonalization() {
        CleverTap_enablePersonalization();
    }

    public static void DisablePersonalization() {
        CleverTap_disablePersonalization();
    }

    public static void SetLocation(double lat, double lon) {
        CleverTap_setLocation(lat, lon);
    }

    public static void PushInstallReferrerSource(string source, string medium, string campaign) {
        CleverTap_pushInstallReferrerSource(source, medium, campaign);
    }

    public static void SetOffline(bool enabled) {
        CleverTap_setOffline(enabled);
    }

    public static void SetOptOut(bool enabled) {
        CleverTap_setOptOut(enabled);
    }

    public static void EnableDeviceNetworkInfoReporting(bool enabled) {
        CleverTap_enableDeviceNetworkInfoReporting(enabled);
    }

    public static void InitializeInbox() {
        CleverTap_initializeInbox();
    }

    public static void ShowAppInbox(Dictionary<string, object> styleConfig) {
        var styleConfigString = Json.Serialize(styleConfig);
        CleverTap_showAppInbox(styleConfigString);
    }

    public static int GetInboxMessageCount() {
        return CleverTap_getInboxMessageCount();
    }

    public static int GetInboxMessageUnreadCount() {
        return CleverTap_getInboxMessageUnreadCount();
    }

    public static void registerStringVariable(string name) {
        CleverTap_registerStringVariable(name);
    }

    public static void registerBooleanVariable(string name) {
        CleverTap_registerBooleanVariable(name);
    }

    public static void registerIntegerVariable(string name) {
        CleverTap_registerIntegerVariable(name);
    }

    public static void registerDoubleVariable(string name) {
        CleverTap_registerDoubleVariable(name);
    }

    public static void registerMapOfStringVariable(string name) {
        CleverTap_registerMapOfStringVariable(name);
    }

    public static void registerMapOfBooleanVariable(string name) {
        CleverTap_registerMapofBooleanVariable(name);
    }

    public static void registerMapOfIntegerVariable(string name) {
        CleverTap_registerMapOfIntegerVariable(name);
    }

    public static void registerMapOfDoubleVariable(string name) {
        CleverTap_registerMapOfDoubleVariable(name);
    }

    public static void registerListOfStringVariable(string name) {
        CleverTap_registerListOfStringVariable(name);
    }

    public static void registerListOfBooleanVariable(string name) {
        CleverTap_registerListofBooleanVariable(name, null);
    }

    public static void registerListOfIntegerVariable(string name) {
        CleverTap_registerListOfIntegerVariable(name);
    }

    public static void registerListOfDoubleVariable(string name) {
        CleverTap_registerListOfDoubleVariable(name);
    }

    public static bool getBooleanVariable(string name, bool defaultValue) {
        return CleverTap_getBooleanVariable(name, defaultValue);
    }

    public static string getStringVariable(string name, string defaultValue) {
        return CleverTap_getStringVariable(name, defaultValue);
    }

    public static int getIntegerVariable(string name, int defaultValue) {
        return CleverTap_getIntegerVariable(name, defaultValue);
    }

    public static double getDoubleVariable(string name, double defaultValue) {
        return CleverTap_getDoubleVariable(name, defaultValue);
    }

    public static Dictionary<string, bool> getMapOfBooleanVariable(string name, Dictionary<string, bool> defaultValue){
        return CleverTap_getMapOfBooleanVariable(name, defaultValue);
    }

    public static Dictionary<string, string> getMapOfStringVariable(string name, Dictionary<string, string> defaultValue){
        return CleverTap_getMapOfStringVariable(name, defaultValue);
    }

    public static Dictionary<string, double> getMapOfDoubleVariable(string name, Dictionary<string, double> defaultValue){
        return CleverTap_getMapOfDoubleVariable(name, defaultValue);
    }

    public static Dictionary<string, int> getMapOfIntegerVariable(string name, Dictionary<string, int> defaultValue){
        return CleverTap_getMapOfIntegerVariable(name, defaultValue);
    }

    public static List<bool> getListOfBooleanVariable(string name, List<bool> defaultValue){
        return CleverTap_getListOfBooleanVariable(name, defaultValue);
    }

    public static List<string> getListOfStringVariable(string name, List<string> defaultValue){
        return CleverTap_getListOfStringVariable(name, defaultValue);
    }

    public static List<double> getListOfDoubleVariable(string name, List<double> defaultValue){
        return CleverTap_getListOfDoubleVariable(name, defaultValue);
    }

    public static List<int> getListOfIntegerVariable(string name, List<int> defaultValue){
        return CleverTap_getListOfIntegerVariable(name, defaultValue);
    }

#elif UNITY_ANDROID
    private static AndroidJavaObject unityActivity;
    private static AndroidJavaObject clevertap;
	private static AndroidJavaObject CleverTapClass;

    void Start() {
        Debug.Log("Start: CleverTap binding for Android.");
    }

    #region Properties
    public static AndroidJavaObject unityCurrentActivity {
        get {
            if (unityActivity == null) {
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                    unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                }
            }
            return unityActivity;
        }
    }

	public static AndroidJavaObject CleverTapAPI {
        get {
            if (CleverTapClass == null) {
                CleverTapClass = new AndroidJavaClass("com.clevertap.unity.CleverTapUnityPlugin");
            }
            return CleverTapClass;
        }
	}

    public static AndroidJavaObject CleverTap {
        get {
            if (clevertap == null) {
                AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
                //set the UI editor flag before getting the Clevertap instance, defaults to false.
                CleverTapAPI.CallStatic("setUIEditorConnectionEnabled", true);
                clevertap = CleverTapAPI.CallStatic<AndroidJavaObject>("getInstance", context);
            }
            return clevertap;
        }
    }
    #endregion

    public static void SetDebugLevel(int level) {
        CleverTapAPI.CallStatic("setDebugLevel", level);
    }

    public static void Initialize(string accountID, string accountToken) {
        CleverTapAPI.CallStatic("initialize", accountID, accountToken, unityCurrentActivity);
    }

    public static void Initialize(string accountID, string accountToken, string region) {
        CleverTapAPI.CallStatic("initialize", accountID, accountToken, region, unityCurrentActivity);
    }

    public static void CreateNotificationChannel(string channelId,string channelName, string channelDescription, int importance, bool showBadge){
        AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
        CleverTapAPI.CallStatic("createNotificationChannel",context,channelId,channelName,channelDescription,importance,showBadge);
    }

    public static void CreateNotificationChannelWithSound(string channelId,string channelName, string channelDescription, int importance, bool showBadge, string sound){
        AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
        CleverTapAPI.CallStatic("createNotificationChannel",context,channelId,channelName,channelDescription,importance,showBadge,sound);
    }

    public static void CreateNotificationChannelWithGroup(string channelId,string channelName, string channelDescription, int importance, string groupId, bool showBadge){
        AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
        CleverTapAPI.CallStatic("createNotificationChannelwithGroup",context,channelId,channelName,channelDescription,importance,groupId,showBadge);
    }

    public static void CreateNotificationChannelWithGroupAndSound(string channelId,string channelName, string channelDescription, int importance, string groupId, bool showBadge, string sound){
        AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
        CleverTapAPI.CallStatic("createNotificationChannelwithGroup",context,channelId,channelName,channelDescription,importance,groupId,showBadge,sound);
    }

    public static void CreateNotificationChannelGroup(string groupId, string groupName){
        AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
        CleverTapAPI.CallStatic("createNotificationChannelGroup",context,groupId,groupName);
    }

    public static void DeleteNotificationChannel(string channelId){
        AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
        CleverTapAPI.CallStatic("deleteNotificationChannel",context,channelId);
    }

    public static void DeleteNotificationChannelGroup(string groupId){
        AndroidJavaObject context = unityCurrentActivity.Call<AndroidJavaObject>("getApplicationContext");
        CleverTapAPI.CallStatic("deleteNotificationChannelGroup",context,groupId);
    }

    public static void SetOptOut(bool value){
        CleverTap.Call("setOptOut",value);
    }

    public static void SetOffline(bool value){
        CleverTap.Call("setOffline",value);
    }

    public static void EnableDeviceNetworkInfoReporting(bool value){
        CleverTap.Call("enableDeviceNetworkInfoReporting",value);
    }

    public static void EnablePersonalization() {
        CleverTap.Call("enablePersonalization");
    }

    public static void DisablePersonalization() {
        CleverTap.Call("disablePersonalization");
    }

    public static void SetLocation(double lat, double lon) {
        CleverTap.Call("setLocation", lat, lon);
    }

    public static void OnUserLogin(Dictionary<string, string> properties) {
        CleverTap.Call("onUserLogin", Json.Serialize(properties));
    }

    public static void ProfilePush(Dictionary<string, string> properties) {
        CleverTap.Call("profilePush", Json.Serialize(properties));
    }

    public static void ProfilePushFacebookUser(Dictionary<string, string> user) {
        CleverTap.Call("profilePushFacebookUser", Json.Serialize(user));
    }

    public static string ProfileGet(string key) {
        return CleverTap.Call<string>("profileGet", key);
    }

    public static string ProfileGetCleverTapAttributionIdentifier() {
        return CleverTap.Call<string>("profileGetCleverTapAttributionIdentifier");
    }

    public static string ProfileGetCleverTapID() {
        return CleverTap.Call<string>("profileGetCleverTapID");
    }

    public static void ProfileRemoveValueForKey(string key) {
        CleverTap.Call("profileRemoveValueForKey", key);
    }

    public static void ProfileSetMultiValuesForKey(string key, List<string> values) {
        CleverTap.Call("profileSetMultiValuesForKey", key, values.ToArray());
    }

    public static void ProfileAddMultiValuesForKey(string key, List<string> values) {
        CleverTap.Call("profileAddMultiValuesForKey", key, values.ToArray());
    }

    public static void ProfileRemoveMultiValuesForKey(string key, List<string> values) {
        CleverTap.Call("profileRemoveMultiValuesForKey", key, values.ToArray());
    }

    public static void ProfileAddMultiValueForKey(string key, string val) {
        CleverTap.Call("profileAddMultiValueForKey", key, val);
    }

    public static void ProfileRemoveMultiValueForKey(string key, string val) {
        CleverTap.Call("profileRemoveMultiValueForKey", key, val);
    }

    public static void RecordScreenView(string screenName) {
        CleverTap.Call("recordScreenView", screenName);
    }

    public static void RecordEvent(string eventName) {
        CleverTap.Call("recordEvent", eventName, null);
    }

    public static void RecordEvent(string eventName, Dictionary<string, object> properties) {
        CleverTap.Call("recordEvent", eventName, Json.Serialize(properties));
    }

    public static void RecordChargedEventWithDetailsAndItems(Dictionary<string, object> details, List<Dictionary<string, object>>items) {
        CleverTap.Call("recordChargedEventWithDetailsAndItems", Json.Serialize(details), Json.Serialize(items));
    }

    public static int EventGetFirstTime(string eventName) {
        return CleverTap.Call<int>("eventGetFirstTime", eventName);
    }

    public static int EventGetLastTime(string eventName) {
        return CleverTap.Call<int>("eventGetLastTime", eventName);
    }

    public static int EventGetOccurrences(string eventName) {
        return CleverTap.Call<int>("eventGetOccurrences", eventName);
    }

    public static JSONClass EventGetDetail(string eventName) {
        string jsonString = CleverTap.Call<string>("eventGetDetail", eventName);
        JSONClass json;
        try {
            json = (JSONClass)JSON.Parse(jsonString);
        } catch {
            Debug.Log("Unable to event detail json");
            json = new JSONClass();
        }
        return json;
    }

    public static JSONClass UserGetEventHistory() {
        string jsonString = CleverTap.Call<string>("userGetEventHistory");
        JSONClass json;
        try {
            json = (JSONClass)JSON.Parse(jsonString);
        } catch {
            Debug.Log("Unable to parse user event history json");
            json = new JSONClass();
        }
        return json;
    }

    public static JSONClass SessionGetUTMDetails() {
        string jsonString = CleverTap.Call<string>("sessionGetUTMDetails");
        JSONClass json;
        try {
            json = (JSONClass)JSON.Parse(jsonString);
        } catch {
            Debug.Log("Unable to parse session utm details json");
            json = new JSONClass();
        }
        return json;
    }

    public static int SessionGetTimeElapsed() {
        return CleverTap.Call<int>("sessionGetTimeElapsed");
    }

    public static int UserGetTotalVisits() {
        return CleverTap.Call<int>("userGetTotalVisits");
    }

    public static int UserGetScreenCount() {
        return CleverTap.Call<int>("userGetScreenCount");
    }

    public static int UserGetPreviousVisitTime() {
        return CleverTap.Call<int>("userGetPreviousVisitTime");
    }

    public static void SetApplicationIconBadgeNumber(int num) {
        // no-op for Android
    }

    public static void PushInstallReferrerSource(string source, string medium, string campaign) {
        // no-op for Android
    }

    public static void InitializeInbox(){
        CleverTap.Call("initializeInbox");
    }

    public static void ShowAppInbox(string styleConfig){
         CleverTap.Call("showAppInbox", styleConfig);
    }

    public static int GetInboxMessageCount(){
        return CleverTap.Call<int>("getInboxMessageCount");
    }

    public static int GetInboxMessageUnreadCount(){
        return CleverTap.Call<int>("getInboxMessageUnreadCount");
    }

    public static void setUIEditorConnectionEnabled(bool enabled){
        CleverTap.CallStatic("setUIEditorConnectionEnabled", enabled);
    }

    public static void registerBooleanVariable(string name){
        CleverTap.Call("registerBooleanVariable", name);
    }

    public static void registerDoubleVariable(string name){
            CleverTap.Call("registerDoubleVariable", name);
    }

    public static void registerIntegerVariable(string name){
            CleverTap.Call("registerIntegerVariable", name);
    }

    public static void registerStringVariable(string name){
        CleverTap.Call("registerStringVariable", name);
    }

    public static void registerListOfBooleanVariable(string name){
        CleverTap.Call("registerListOfBooleanVariable", name);
    }

    public static void registerListOfDoubleVariable(string name){
        CleverTap.Call("registerListOfDoubleVariable", name);
    }

    public static void registerListOfIntegerVariable(string name){
        CleverTap.Call("registerListOfIntegerVariable", name);
    }

    public static void registerListOfStringVariable(string name){
        CleverTap.Call("registerListOfStringVariable", name);
    }

    public static void registerMapOfBooleanVariable(string name){
        CleverTap.Call("registerMapOfBooleanVariable", name);
    }

    public static void registerMapOfDoubleVariable(string name){
        CleverTap.Call("registerMapOfDoubleVariable", name);
    }

    public static void registerMapOfIntegerVariable(string name){
        CleverTap.Call("registerMapOfIntegerVariable", name);
    }

    public static void registerMapOfStringVariable(string name){
        CleverTap.Call("registerMapOfStringVariable", name);
    }

    public static bool getBooleanVariable(string name, bool defaultValue){
        return CleverTap.Call<bool>("getBooleanVariable", name, defaultValue);
    }

    public static double getDoubleVariable(string name, double defaultValue){
        return CleverTap.Call<double>("getDoubleVariable", name, defaultValue);
    }

    public static int getIntegerVariable(string name, int defaultValue){
        return CleverTap.Call<int>("getIntegerVariable", name, defaultValue);
    }

    public static string getStringVariable(string name, string defaultValue){
        return CleverTap.Call<string>("getStringVariable", name, defaultValue);
    }

    public static List<bool> getListOfBooleanVariable(string name, List<bool> defaultValue){
        return CleverTap.Call<List<bool>>("getListOfBooleanVariable", name, defaultValue);
    }

    public static List<double> getListOfDoubleVariable(string name, List<double> defaultValue){
        return CleverTap.Call<List<double>>("getListOfDoubleVariable", name, defaultValue);
    }

    public static List<int> getListOfIntegerVariable(string name, List<int> defaultValue){
        return CleverTap.Call<List<int>>("getListOfIntegerVariable", name, defaultValue);
    }

    public static List<string> getListOfStringVariable(string name, List<string> defaultValue){
        return CleverTap.Call<List<string>>("getListOfStringVariable", name, defaultValue);
    }

    public static Dictionary<string, bool> getMapOfBooleanVariable(string name, Dictionary<string, bool> defaultValue){
        return CleverTap.Call<Dictionary<string, bool>>("getMapOfBooleanVariable", name, defaultValue);
    }

    public static Dictionary<string, double> getMapOfDoubleVariable(string name, Dictionary<string, double> defaultValue){
        return CleverTap.Call<Dictionary<string, double>>("getMapOfDoubleVariable", name, defaultValue);
    }

    public static Dictionary<string, int> getMapOfIntegerVariable(string name, Dictionary<string, int> defaultValue){
        return CleverTap.Call<Dictionary<string, int>>("getMapOfIntegerVariable", name, defaultValue);
    }

    public static Dictionary<string, string> getMapOfStringVariable(string name, Dictionary<string, string> defaultValue){
        return CleverTap.Call<Dictionary<string, string>>("getMapOfStringVariable", name, defaultValue);
    }
#else

   // Empty implementations of the API, in case the application is being compiled for a platform other than iOS or Android.
    void Start() {
        Debug.Log("Start: no-op CleverTap binding for non iOS/Android.");
    }

    public static void LaunchWithCredentials(string accountID, string token, string region) {
    }

    public static void OnUserLogin(Dictionary<string, string> properties) {
    }

    public static void ProfilePush(Dictionary<string, string> properties) {
    }

    public static void ProfilePushGraphUser(Dictionary<string, string> user) {
    }

    public static void ProfilePushGooglePlusUser(Dictionary<string, string> user) {
    }

    public static string ProfileGet(string key) {
        return "test";
    }

    public static string ProfileGetCleverTapAttributionIdentifier() {
        return "testAttributionIdentifier";
    }

    public static string ProfileGetCleverTapID() {
        return "testCleverTapID";
    }

    public static void ProfileRemoveValueForKey(string key) {
    }

    public static void ProfileSetMultiValuesForKey(string key, List<string> values) {
    }

    public static void ProfileAddMultiValuesForKey(string key, List<string> values) {
    }

    public static void ProfileRemoveMultiValuesForKey(string key, List<string> values) {
    }

    public static void ProfileAddMultiValueForKey(string key, string val) {
    }

    public static void ProfileRemoveMultiValueForKey(string key, string val) {
    }

    public static void RecordScreenView(string screenName) {
    }

    public static void RecordEvent(string eventName) {
    }

    public static void RecordEvent(string eventName, Dictionary<string, object> properties) {
    }

    public static void RecordChargedEventWithDetailsAndItems(Dictionary<string, object> details, List<Dictionary<string, object>>items) {
    }

    public static int EventGetFirstTime(string eventName) {
        return -1;
    }

    public static int EventGetLastTime(string eventName) {
        return -1;
    }

    public static int EventGetOccurrences(string eventName) {
        return -1;
    }

    public static JSONClass EventGetDetail(string eventName) {
        return new JSONClass();
    }

    public static JSONClass UserGetEventHistory() {
        return new JSONClass();
    }

    public static JSONClass SessionGetUTMDetails() {
        return new JSONClass();
    }

    public static int SessionGetTimeElapsed() {
        return -1;
    }

    public static int UserGetTotalVisits() {
        return -1;
    }

    public static int UserGetScreenCount() {
        return -1;
    }

    public static int UserGetPreviousVisitTime() {
        return -1;
    }

    public static void EnablePersonalization() {
    }

    public static void DisablePersonalization() {
    }

    public static void RegisterPush() {
    }

    public static void SetApplicationIconBadgeNumber(int num) {
    }

    public static void SetDebugLevel(int level) {
    }

    public static void SetLocation(double lat, double lon) {
    }

    public static void PushInstallReferrerSource(string source, string medium, string campaign) {
    }

    public static void EnableDeviceNetworkInfoReporting(bool value){
    }

    public static void SetOptOut(bool value){
    }

    public static void SetOffline(bool value){
    }

    public static void CreateNotificationChannel(string channelId,string channelName, string channelDescription, int importance, bool showBadge){
    }

    public static void CreateNotificationChannelWithSound(string channelId,string channelName, string channelDescription, int importance, bool showBadge, string sound){
    }

    public static void CreateNotificationChannelWithGroup(string channelId,string channelName, string channelDescription, int importance, string groupId, bool showBadge){
    }

    public static void CreateNotificationChannelWithGroupAndSound(string channelId,string channelName, string channelDescription, int importance, string groupId, bool showBadge, string sound){
    }

    public static void CreateNotificationChannelGroup(string groupId, string groupName){
    }

    public static void DeleteNotificationChannel(string channelId){
    }

    public static void DeleteNotificationChannelGroup(string groupId){
    }

    public static void InitializeInbox(){
    }

    public static void ShowAppInbox(string styleConfig){
    }

    public static int GetInboxMessageCount(){
        return -1;
    }

    public static int GetInboxMessageUnreadCount(){
        return -1;
    }
#endif
    }
}
