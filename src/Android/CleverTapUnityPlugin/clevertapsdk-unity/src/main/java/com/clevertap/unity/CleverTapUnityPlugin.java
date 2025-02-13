package com.clevertap.unity;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.location.Location;
import android.net.Uri;
import android.os.Build.VERSION_CODES;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import com.clevertap.android.sdk.ActivityLifecycleCallback;
import com.clevertap.android.sdk.CTFeatureFlagsListener;
import com.clevertap.android.sdk.CTInboxListener;
import com.clevertap.android.sdk.CTInboxStyleConfig;
import com.clevertap.android.sdk.CleverTapAPI;
import com.clevertap.android.sdk.InAppNotificationButtonListener;
import com.clevertap.android.sdk.InAppNotificationListener;
import com.clevertap.android.sdk.InboxMessageButtonListener;
import com.clevertap.android.sdk.SyncListener;
import com.clevertap.android.sdk.UTMDetail;
import com.clevertap.android.sdk.displayunits.DisplayUnitListener;
import com.clevertap.android.sdk.displayunits.model.CleverTapDisplayUnit;
import com.clevertap.android.sdk.events.EventDetail;
import com.clevertap.android.sdk.inbox.CTInboxMessage;
import com.clevertap.android.sdk.product_config.CTProductConfigListener;
import com.clevertap.android.sdk.pushnotification.PushConstants;
import com.unity3d.player.UnityPlayer;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class CleverTapUnityPlugin implements SyncListener, InAppNotificationListener,
        CTInboxListener, InAppNotificationButtonListener,
        InboxMessageButtonListener, DisplayUnitListener, CTFeatureFlagsListener, CTProductConfigListener {

    private static final String LOG_TAG = "CleverTapUnityPlugin";

    private static final String CLEVERTAP_GAME_OBJECT_NAME = "CleverTapUnity";

    private static final String CLEVERTAP_PROFILE_INITIALIZED_CALLBACK = "CleverTapProfileInitializedCallback";

    private static final String CLEVERTAP_PROFILE_UPDATES_CALLBACK = "CleverTapProfileUpdatesCallback";

    private static final String CLEVERTAP_DEEP_LINK_CALLBACK = "CleverTapDeepLinkCallback";

    private static final String CLEVERTAP_PUSH_OPENED_CALLBACK = "CleverTapPushOpenedCallback";

    private static final String CLEVERTAP_INAPP_NOTIFICATION_DISMISSED_CALLBACK
            = "CleverTapInAppNotificationDismissedCallback";

    private static final String CLEVERTAP_INBOX_DID_INITIALIZE = "CleverTapInboxDidInitializeCallback";

    private static final String CLEVERTAP_INBOX_MESSAGES_DID_UPDATE = "CleverTapInboxMessagesDidUpdateCallback";

    private static final String CLEVERTAP_ON_INBOX_BUTTON_CLICKED = "CleverTapInboxCustomExtrasButtonSelect";

    private static final String CLEVERTAP_ON_INAPP_BUTTON_CLICKED = "CleverTapInAppNotificationButtonTapped";

    private static final String CLEVERTAP_DISPLAY_UNITS_UPDATED = "CleverTapNativeDisplayUnitsUpdated";

    private static final String CLEVERTAP_FEATURE_FLAG_UPDATED = "CleverTapFeatureFlagsUpdated";

    private static final String CLEVERTAP_PRODUCT_CONFIG_INITIALIZED = "CleverTapProductConfigInitialized";

    private static final String CLEVERTAP_PRODUCT_CONFIG_FETCHED = "CleverTapProductConfigFetched";

    private static final String CLEVERTAP_PRODUCT_CONFIG_ACTIVATED = "CleverTapProductConfigActivated";

    private static CleverTapUnityPlugin instance = null;

    private CleverTapAPI clevertap = null;

    private static void changeCredentials(final String accountID, final String accountToken, final String region) {
        CleverTapAPI.changeCredentials(accountID, accountToken, region);
    }

    static void handleIntent(Intent intent) {
        if (intent == null) {
            return;
        }
        if (intent.getAction() == null) {
            return;
        }

        if (intent.getAction().equals(Intent.ACTION_VIEW)) {
            Uri data = intent.getData();
            if (data != null) {
                handleDeepLink(data);
            }
        } else {
            Bundle extras = intent.getExtras();
            boolean isPushNotification = (extras != null && extras.get("wzrk_pn") != null);
            if (isPushNotification) {
                JSONObject data = new JSONObject();

                for (String key : extras.keySet()) {
                    try {
                        Object value = extras.get(key);
                        if (value instanceof Map) {
                            JSONObject jsonObject = new JSONObject((Map) value);
                            data.put(key, jsonObject);
                        } else if (value instanceof List) {
                            JSONArray jsonArray = new JSONArray((List) value);
                            data.put(key, jsonArray);
                        } else {
                            data.put(key, extras.get(key));
                        }

                    } catch (JSONException e) {
                        // no-op
                    }
                }
                handlePushNotification(data);
            }
        }
    }

    static private void handlePushNotification(final JSONObject data) {
        final String json = data.toString();
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_PUSH_OPENED_CALLBACK, json);
    }

    static private void handleDeepLink(final Uri data) {
        final String json = data.toString();
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_DEEP_LINK_CALLBACK, json);
    }

    public static void initialize(final String accountID, final String accountToken, final Activity activity) {
        initialize(accountID, accountToken, null, activity);
    }

    public static void initialize(final String accountID, final String accountToken, final String region,
            final Activity activity) {
        try {
            changeCredentials(accountID, accountToken, region);
            ActivityLifecycleCallback.register(activity.getApplication());
            CleverTapAPI.setAppForeground(true);
            getInstance(activity.getApplicationContext());
            CleverTapAPI.onActivityResumed(activity);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "initialize error", t);
        }
    }

    public static void setDebugLevel(int level) {
        CleverTapAPI.setDebugLevel(level);
    }

    public static synchronized CleverTapUnityPlugin getInstance(final Context context) {
        if (instance == null && context != null) {
            instance = new CleverTapUnityPlugin(context.getApplicationContext());
        }
        return instance;
    }

    private CleverTapUnityPlugin(final Context context) {
        try {
            clevertap = CleverTapAPI.getDefaultInstance(context);
            if (clevertap != null) {
                Log.d(LOG_TAG, "getDefaultInstance-" + clevertap);
                clevertap.setInAppNotificationListener(this);
                clevertap.setSyncListener(this);
                clevertap.setCTNotificationInboxListener(this);
                clevertap.setInboxMessageButtonListener(this);
                clevertap.setInAppNotificationButtonListener(this);
                clevertap.setDisplayUnitListener(this);
                clevertap.setCTFeatureFlagsListener(this);
                clevertap.setCTFeatureFlagsListener(this);
                clevertap.setLibrary("Unity");
            }
        } catch (Throwable t) {
            Log.e(LOG_TAG, "initialization error", t);
        }
    }

    public static void createNotificationChannel(Context context, String channelId, String channelName,
            String channelDescription, int importance, boolean showBadge) {
        try {
            CleverTapAPI.createNotificationChannel(context, channelId, channelName, channelDescription, importance,
                    showBadge);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "Error creating Notification Channel", t);
        }
    }

    @RequiresApi(api = VERSION_CODES.O)
    public static void createNotificationChannelWithSound(Context context, String channelId, String channelName,
            String channelDescription, int importance, boolean showBadge, String sound) {
        try {
            CleverTapAPI.createNotificationChannel(context, channelId, channelName, channelDescription, importance,
                    showBadge, sound);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "Error creating Notification Channel", t);
        }
    }

    @RequiresApi(api = VERSION_CODES.O)
    public static void createNotificationChannelWithGroup(Context context, String channelId, String channelName,
            String channelDescription, int importance, String groupId, boolean showBadge) {
        try {
            CleverTapAPI.createNotificationChannel(context, channelId, channelName, channelDescription, importance,
                    groupId, showBadge);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "Error creating Notification Channel with groupId", t);
        }
    }

    @RequiresApi(api = VERSION_CODES.O)
    public static void createNotificationChannelWithGroupAndSound(Context context, String channelId,
            String channelName, String channelDescription, int importance, String groupId, boolean showBadge,
            String sound) {
        try {
            CleverTapAPI.createNotificationChannel(context, channelId, channelName, channelDescription, importance,
                    groupId, showBadge, sound);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "Error creating Notification Channel with groupId", t);
        }
    }

    @RequiresApi(api = VERSION_CODES.O)
    public static void createNotificationChannelGroup(Context context, String groupId, String groupName) {
        try {
            CleverTapAPI.createNotificationChannelGroup(context, groupId, groupName);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "Error creating Notification Channel Group", t);
        }
    }

    @RequiresApi(api = VERSION_CODES.O)
    public static void deleteNotificationChannel(Context context, String channelId) {
        try {
            CleverTapAPI.deleteNotificationChannel(context, channelId);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "Error deleting Notification Channel", t);
        }
    }

    @RequiresApi(api = VERSION_CODES.O)
    public static void deleteNotificationChannelGroup(Context context, String groupId) {
        try {
            CleverTapAPI.deleteNotificationChannelGroup(context, groupId);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "Error deleting Notification Channel Group", t);
        }
    }

    public void setPushToken(String token, String type) {
        if (PushConstants.PushType.valueOf(type.toLowerCase()).equals(PushConstants.PushType.FCM)) {
            clevertap.pushFcmRegistrationId(token, true);
        } else if (PushConstants.PushType.valueOf(type.toLowerCase()).equals(PushConstants.PushType.XPS)) {
            clevertap.pushXiaomiRegistrationId(token, true);
        } else if (PushConstants.PushType.valueOf(type.toLowerCase()).equals(PushConstants.PushType.BPS)) {
            clevertap.pushBaiduRegistrationId(token, true);
        } else if (PushConstants.PushType.valueOf(type.toLowerCase()).equals(PushConstants.PushType.HPS)) {
            clevertap.pushHuaweiRegistrationId(token, true);
        }
    }

    public void setOptOut(boolean value) {
        try {
            clevertap.setOptOut(value);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "setOptOut error", t);
        }
    }

    public void enableDeviceNetworkInfoReporting(boolean value) {
        try {
            clevertap.enableDeviceNetworkInfoReporting(value);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "enableDeviceNetworkInfoReporting error", t);
        }
    }

    public void enablePersonalization() {
        try {
            clevertap.enablePersonalization();
        } catch (Throwable t) {
            Log.e(LOG_TAG, "enablePersonalization error", t);
        }
    }

    public void disablePersonalization() {
        try {
            clevertap.disablePersonalization();
        } catch (Throwable t) {
            Log.e(LOG_TAG, "disablePersonalization error", t);
        }
    }

    public void setLocation(final double lat, final double lon) {
        try {
            final Location location = new Location("CleverTapUnityPlugin");
            location.setLatitude(lat);
            location.setLongitude(lon);
            clevertap.setLocation(location);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "setLocation error", t);
        }
    }

    public void onUserLogin(final String jsonString) {
        try {
            Map<String, Object> profile = toMap(new JSONObject(jsonString));
            clevertap.onUserLogin(profile);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "onUserLogin error", t);
        }
    }

    public void profilePush(final String jsonString) {
        try {
            Map<String, Object> profile = toMap(new JSONObject(jsonString));
            clevertap.pushProfile(profile);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "profilePush error", t);
        }
    }
/*
    public void profilePushFacebookUser(final String jsonString) {
        try {
            clevertap.pushFacebookUser(new JSONObject(jsonString));
        } catch (Throwable t) {
            Log.e(LOG_TAG, "profilePushFacebookUser error", t);
        }
    }*/

    public String profileGet(final String key) {
        try {
            String val = null;
            Object _val = clevertap.getProperty(key);
            if (_val != null) {
                val = _val.toString();
            }
            return val;

        } catch (Throwable t) {
            Log.e(LOG_TAG, "profileGet error", t);
            return null;
        }
    }

    public String profileGetCleverTapAttributionIdentifier() {
        try {
            return clevertap.getCleverTapAttributionIdentifier();
        } catch (Throwable t) {
            Log.e(LOG_TAG, "profileGetCleverTapAttributionIdentifier error", t);
            return null;
        }
    }

    public String profileGetCleverTapID() {
        try {
            return clevertap.getCleverTapID();
        } catch (Throwable t) {
            Log.e(LOG_TAG, "profileGetCleverTapID error", t);
            return null;
        }
    }

    public void profileRemoveValueForKey(final String key) {
        clevertap.removeValueForKey(key);
    }

    public void profileAddMultiValueForKey(final String key, final String val) {
        clevertap.addMultiValueForKey(key, val);
    }

    public void profileRemoveMultiValueForKey(final String key, final String val) {
        clevertap.removeMultiValueForKey(key, val);
    }

    public void profileSetMultiValuesForKey(final String key, final String[] values) {
        try {
            clevertap.setMultiValuesForKey(key, new ArrayList<String>(Arrays.asList(values)));
        } catch (Throwable t) {
            Log.e(LOG_TAG, "profileSetMultiValuesForKey error", t);
        }
    }

    public void profileAddMultiValuesForKey(final String key, final String[] values) {
        try {
            clevertap.addMultiValuesForKey(key, new ArrayList<String>(Arrays.asList(values)));
        } catch (Throwable t) {
            Log.e(LOG_TAG, "profileAddMultiValuesForKey error", t);
        }
    }

    public void profileRemoveMultiValuesForKey(final String key, final String[] values) {
        try {
            clevertap.removeMultiValuesForKey(key, new ArrayList<String>(Arrays.asList(values)));
        } catch (Throwable t) {
            Log.e(LOG_TAG, "profileRemoveMultiValuesForKey error", t);
        }
    }

    public void recordScreenView(final String screenName) {
        try {
            clevertap.recordScreen(screenName);
        } catch (Throwable t) {
            Log.e(LOG_TAG, "recordScreenView error", t);
        }
    }

    public void recordEvent(final String eventName, final String propertiesJsonString) {

        if (eventName == null) {
            return;
        }

        if (propertiesJsonString == null) {
            clevertap.pushEvent(eventName);
        } else {
            try {
                JSONObject _props = new JSONObject(propertiesJsonString);
                Map<String, Object> props = toMap(_props);
                clevertap.pushEvent(eventName, props);
            } catch (Throwable t) {
                Log.e(LOG_TAG, "recordEvent error", t);
            }
        }
    }

    public void recordChargedEventWithDetailsAndItems(final String detailsJSON, final String itemsJSON) {

        try {
            JSONObject details = new JSONObject(detailsJSON);
            JSONArray items = new JSONArray(itemsJSON);
            clevertap.pushChargedEvent(toMap(details), toArrayListOfStringObjectMaps(items));
        } catch (Throwable t) {
            Log.e(LOG_TAG, "recordChargedEventWithDetailsAndItems error", t);
        }
    }

    public int eventGetFirstTime(final String event) {
        return clevertap.getFirstTime(event);
    }

    public int eventGetLastTime(final String event) {
        return clevertap.getLastTime(event);
    }

    public int eventGetOccurrences(final String event) {
        return clevertap.getCount(event);
    }

    public String eventGetDetail(final String event) {
        try {
            EventDetail details = clevertap.getDetails(event);
            return eventDetailsToJSON(details).toString();
        } catch (Throwable t) {
            Log.e(LOG_TAG, "eventGetDetail error", t);
            return null;
        }
    }

    public String userGetEventHistory() {
        try {
            Map<String, EventDetail> history = clevertap.getHistory();
            return eventHistoryToJSON(history).toString();
        } catch (Throwable t) {
            Log.e(LOG_TAG, "userGetEventHistory error", t);
            return null;
        }
    }

    public String sessionGetUTMDetails() {
        try {
            UTMDetail details = clevertap.getUTMDetails();
            return utmDetailsToJSON(details).toString();
        } catch (Throwable t) {
            Log.e(LOG_TAG, "sessionGetUTMDetails error", t);
            return null;
        }
    }

    public int sessionGetTimeElapsed() {
        return clevertap.getTimeElapsed();
    }

    public int userGetTotalVisits() {
        return clevertap.getTotalVisits();
    }

    public int userGetScreenCount() {
        return clevertap.getScreenCount();
    }

    public int userGetPreviousVisitTime() {
        return clevertap.getPreviousVisitTime();
    }

    //Notification Inbox
    public void initializeInbox() {
        clevertap.initializeInbox();
    }

    public int getInboxMessageCount() {
        return clevertap.getInboxMessageCount();
    }

    public int getInboxMessageUnreadCount() {
        return clevertap.getInboxMessageCount();
    }

    public void showAppInbox(final String jsonString) {
        try {
            if (!TextUtils.isEmpty(jsonString)) {
                CTInboxStyleConfig styleConfig = toStyleConfig(new JSONObject(jsonString));
                clevertap.showAppInbox(styleConfig);
            } else {
                clevertap.showAppInbox();
            }
        } catch (JSONException e) {
            Log.e(LOG_TAG, "JSON Exception in converting style config", e);
        }
    }

    public String getAllInboxMessages() {
        try {
            return inboxMessageListToJSONArray(clevertap.getAllInboxMessages()).toString();
        } catch (JSONException e) {
            e.printStackTrace();
            return null;
        }
    }

    public String getUnreadInboxMessages() {
        try {
            return inboxMessageListToJSONArray(clevertap.getUnreadInboxMessages()).toString();
        } catch (JSONException e) {
            e.printStackTrace();
            return null;
        }
    }

    public String getInboxMessageForId(String messageId) {
        return clevertap.getInboxMessageForId(messageId).getData().toString();
    }

    public void deleteInboxMessageForId(String messageId) {
        clevertap.deleteInboxMessage(messageId);
    }

    public void markReadInboxMessageForId(String messageId) {
        clevertap.markReadInboxMessage(messageId);
    }

    public void pushInboxNotificationViewedEventForId(String messageId) {
        clevertap.pushInboxNotificationViewedEvent(messageId);
    }

    public void pushInboxNotificationClickedEventForId(String messageId) {
        clevertap.pushInboxNotificationClickedEvent(messageId);
    }

    public void setLibrary(String library) {
        try {
            clevertap.setLibrary(library);
        } catch (Exception e) {
            Log.e(LOG_TAG, "setLibrary error", e);
        }
    }

    public void pushInstallReferrer(String source, String medium, String campaign) {
        try {
            clevertap.pushInstallReferrer(source, medium, campaign);
        } catch (Exception e) {
            Log.e(LOG_TAG, "pushInstallReferrer error", e);
        }
    }

    //Native Display Units
    public String getAllDisplayUnits() {
        try {
            ArrayList<CleverTapDisplayUnit> arrayList = clevertap.getAllDisplayUnits();
            JSONArray jsonArray = new JSONArray();
            if (arrayList != null) {
                jsonArray = displayUnitListToJSONArray(arrayList);
            }
            return jsonArray.toString();
        } catch (JSONException e) {
            e.printStackTrace();
            return null;
        }
    }

    public String getDisplayUnitForId(String unitId) {
        CleverTapDisplayUnit displayUnit = clevertap.getDisplayUnitForId(unitId);
        JSONObject jsonObject = new JSONObject();
        if (displayUnit != null) {
            jsonObject = displayUnit.getJsonObject();
        }
        return jsonObject.toString();
    }

    public void pushDisplayUnitViewedEventForID(String unitId) {
        clevertap.pushDisplayUnitViewedEventForID(unitId);
    }

    public void pushDisplayUnitClickedEventForID(String unitId) {
        clevertap.pushDisplayUnitClickedEventForID(unitId);
    }

    //Feature Flags
    public Boolean isFeatureFlagInitialized() {
        return clevertap.featureFlag().isInitialized();
    }

    public void fetchFeatureFlags() {
        clevertap.featureFlag().fetchFeatureFlags();
    }

    public Boolean getFeatureFlag(String key, Boolean defaultValue) {
        return clevertap.featureFlag().get(key, defaultValue);
    }

    //Product Config
    public Boolean isProductConfigInitialized() {
        return clevertap.productConfig().isInitialized();
    }

    public void setMapDefaults(HashMap<String, Object> map) {
        clevertap.productConfig().setDefaults(map);
    }

    public void fetch() {
        clevertap.productConfig().fetch();
    }

    public void fetch(long minimumIntervalInSeconds) {
        clevertap.productConfig().fetch(minimumIntervalInSeconds);
    }

    public void fetchAndActivate() {
        clevertap.productConfig().fetchAndActivate();
    }

    public void activate() {
        clevertap.productConfig().activate();
    }

    public String getString(String key) {
        return clevertap.productConfig().getString(key);
    }

    public Boolean getBoolean(String key) {
        return clevertap.productConfig().getBoolean(key);
    }

    public long getLong(String key) {
        return clevertap.productConfig().getLong(key);
    }

    public Double getDouble(String key) {
        return clevertap.productConfig().getDouble(key);
    }

    public void productConfigReset() {
        clevertap.productConfig().reset();
    }

    public long getLastFetchTimeStampInMillis() {
        return clevertap.productConfig().getLastFetchTimeStampInMillis();
    }

    // InAppNotificationListener
    public boolean beforeShow(Map<String, Object> var1) {
        return true;
    }

    public void onDismissed(Map<String, Object> var1, @Nullable Map<String, Object> var2) {
        if (var1 == null && var2 == null) {
            return;
        }

        JSONObject extras = var1 != null ? new JSONObject(var1) : new JSONObject();
        String _json = "{extras:" + extras.toString() + ",";

        JSONObject actionExtras = var2 != null ? new JSONObject(var2) : new JSONObject();
        _json += "actionExtras:" + actionExtras.toString() + "}";

        final String json = _json;
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_INAPP_NOTIFICATION_DISMISSED_CALLBACK, json);
    }

    // SyncListener
    public void profileDataUpdated(JSONObject updates) {

        if (updates == null) {
            return;
        }

        final String json = "{updates:" + updates.toString() + "}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_PROFILE_UPDATES_CALLBACK, json);
    }

    public void profileDidInitialize(String CleverTapID) {

        if (CleverTapID == null) {
            return;
        }

        final String json = "{CleverTapID:" + CleverTapID + "}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_PROFILE_INITIALIZED_CALLBACK, json);
    }

    //Inbox Listeners
    public void inboxDidInitialize() {
        final String json = "{CleverTap App Inbox Initialized}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_INBOX_DID_INITIALIZE, json);
    }

    public void inboxMessagesDidUpdate() {
        final String json = "{CleverTap App Inbox Messages Updated}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_INBOX_MESSAGES_DID_UPDATE, json);
    }

    //Inbox Button Click Listener
    public void onInboxButtonClick(HashMap<String, String> payload) {
        JSONObject jsonObject = new JSONObject(payload);
        final String json = "{inbox button payload:" + jsonObject.toString() + "}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_ON_INBOX_BUTTON_CLICKED, json);
    }

    //InApp Button Click Listener
    public void onInAppButtonClick(HashMap<String, String> payload) {
        JSONObject jsonObject = new JSONObject(payload);
        final String json = "{inapp button payload:" + jsonObject.toString() + "}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_ON_INAPP_BUTTON_CLICKED, json);
    }

    //Native Display Listener
    public void onDisplayUnitsLoaded(ArrayList<CleverTapDisplayUnit> units) {
        try {
            JSONArray jsonArray = displayUnitListToJSONArray(units);
            final String json = "{display units:" + jsonArray.toString() + "}";
            messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_DISPLAY_UNITS_UPDATED, json);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    //Feature Flag Listener
    public void featureFlagsUpdated() {
        final String json = "{CleverTap App Feature Flags Updated}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_FEATURE_FLAG_UPDATED, json);
    }

    //Product Config Listener
    public void onInit() {
        final String json = "{CleverTap App Product Config Initialized}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_PRODUCT_CONFIG_INITIALIZED, json);
    }

    public void onFetched() {
        final String json = "{CleverTap App Product Config Fetched}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_PRODUCT_CONFIG_FETCHED, json);
    }

    public void onActivated() {
        final String json = "{CleverTap App Product Config Activated}";
        messageUnity(CLEVERTAP_GAME_OBJECT_NAME, CLEVERTAP_PRODUCT_CONFIG_ACTIVATED, json);
    }

    /*******************
     * Helpers
     ******************/

    private static void messageUnity(final String gameObject, final String method, final String data) {
        UnityPlayer.UnitySendMessage(gameObject, method, data);
    }

    private static Object fromJson(Object json) throws JSONException {
        if (json == JSONObject.NULL) {
            return null;
        } else if (json instanceof JSONObject) {
            return toMap((JSONObject) json);
        } else {
            return json;
        }
    }

    private static HashMap<String, Object> toMap(JSONObject object) throws JSONException {
        HashMap<String, Object> map = new HashMap<String, Object>();
        Iterator keys = object.keys();
        while (keys.hasNext()) {
            String key = (String) keys.next();
            map.put(key, fromJson(object.get(key)));
        }
        return map;
    }

    private static ArrayList<HashMap<String, Object>> toArrayListOfStringObjectMaps(JSONArray array)
            throws JSONException {
        ArrayList<HashMap<String, Object>> aList = new ArrayList<HashMap<String, Object>>();

        for (int i = 0; i < array.length(); i++) {
            aList.add(toMap((JSONObject) array.get(i)));
        }

        return aList;
    }

    private static JSONObject eventDetailsToJSON(EventDetail details) throws JSONException {

        JSONObject json = new JSONObject();

        if (details != null) {
            json.put("name", details.getName());
            json.put("firstTime", details.getFirstTime());
            json.put("lastTime", details.getLastTime());
            json.put("count", details.getCount());
        }

        return json;
    }

    private static JSONObject utmDetailsToJSON(UTMDetail details) throws JSONException {

        JSONObject json = new JSONObject();

        if (details != null) {
            json.put("campaign", details.getCampaign());
            json.put("source", details.getSource());
            json.put("medium", details.getMedium());
        }

        return json;
    }

    private static JSONObject eventHistoryToJSON(Map<String, EventDetail> history) throws JSONException {

        JSONObject json = new JSONObject();

        if (history != null) {
            for (Object key : history.keySet()) {
                json.put(key.toString(), eventDetailsToJSON(history.get((String) key)));
            }
        }

        return json;
    }

    @SuppressWarnings({"rawtypes", "unchecked"})
    private static CTInboxStyleConfig toStyleConfig(JSONObject object) throws JSONException {
        CTInboxStyleConfig styleConfig = new CTInboxStyleConfig();

        if (object.has("noMessageText")) {
            styleConfig.setNoMessageViewText(object.getString("noMessageText"));
        }
        if (object.has("noMessageTextColor")) {
            styleConfig.setNoMessageViewTextColor(object.getString("noMessageTextColor"));
        }
        if (object.has("navBarColor")) {
            styleConfig.setNavBarColor(object.getString("navBarColor"));
        }
        if (object.has("navBarTitle")) {
            styleConfig.setNavBarTitle(object.getString("navBarTitle"));
        }
        if (object.has("navBarTitleColor")) {
            styleConfig.setNavBarTitleColor(object.getString("navBarTitleColor"));
        }
        if (object.has("inboxBackgroundColor")) {
            styleConfig.setInboxBackgroundColor(object.getString("inboxBackgroundColor"));
        }
        if (object.has("backButtonColor")) {
            styleConfig.setBackButtonColor(object.getString("backButtonColor"));
        }
        if (object.has("selectedTabColor")) {
            styleConfig.setSelectedTabColor(object.getString("selectedTabColor"));
        }
        if (object.has("unselectedTabColor")) {
            styleConfig.setUnselectedTabColor(object.getString("unselectedTabColor"));
        }
        if (object.has("selectedTabIndicatorColor")) {
            styleConfig.setSelectedTabIndicatorColor(object.getString("selectedTabIndicatorColor"));
        }
        if (object.has("tabBackgroundColor")) {
            styleConfig.setTabBackgroundColor(object.getString("tabBackgroundColor"));
        }
        if (object.has("firstTabTitle")) {
            styleConfig.setFirstTabTitle(object.getString("firstTabTitle"));
        }
        if (object.has("tabs")) {
            JSONArray tabsArray = object.getJSONArray("tabs");
            ArrayList tabs = new ArrayList();
            for (int i = 0; i < tabsArray.length(); i++) {
                tabs.add(tabsArray.getString(i));
            }
            styleConfig.setTabs(tabs);
        }
        return styleConfig;
    }

    private JSONArray inboxMessageListToJSONArray(ArrayList<CTInboxMessage> messageList) throws JSONException {
        JSONArray array = new JSONArray();

        for (int i = 0; i < messageList.size(); i++) {
            array.put(messageList.get(i).getData());
        }

        return array;
    }

    private JSONArray displayUnitListToJSONArray(ArrayList<CleverTapDisplayUnit> displayUnits) throws JSONException {
        JSONArray array = new JSONArray();

        for (int i = 0; i < displayUnits.size(); i++) {
            array.put(displayUnits.get(i).getJsonObject());
        }

        return array;
    }
}