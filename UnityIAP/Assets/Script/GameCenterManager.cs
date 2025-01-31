﻿using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenterManager : System.Object
{
    private static GameCenterManager instance;
    private static object _lock = new object();
    private GameCenterManager() { }
    public static GameCenterManager GetInstance()
    {
        if (instance == null)
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new GameCenterManager();
                }
            }
        }
        return instance;
    }

    public void Start()
    {
        Social.localUser.Authenticate(HandleAuthenticated);
    }

    /// <summary>
    /// 处理登陆
    /// </summary>
    private void HandleAuthenticated(bool success)
    {
        Debug.Log("*** HandleAuthenticated: success = " + success);
        if (success)
        {
            string userInfo = "UserName:" + Social.localUser.userName + "\nUser ID:" +
                Social.localUser.id + " \nIsUnderage: " + Social.localUser.underage;
            Debug.Log(userInfo);

            //下面三行看个人需要，需要什么信息就取什么信息，这里注释掉是因为担心有的朋友没有在iTunesConnect里设置排行、成就之类的东西，运行起来可能会报错
            //    Social.localUser.LoadFriends(HandleFriendsLoaded);
            //    Social.LoadAchievements(HandleAchievementsLoaded);
            //    Social.LoadAchievementDescriptions(HandleAchievementDescriptionsLoaded);
        }
    }

    /// <summary>
    /// 处理好友
    /// </summary>
    private void HandleFriendsLoaded(bool success)
    {
        Debug.Log("*** HandleFriendsLoaded: success = " + success);
        foreach (IUserProfile friend in Social.localUser.friends)
        {
            Debug.Log("* friend = " + friend.ToString());
        }
    }

    /// <summary>
    /// 处理成就
    /// </summary>
    private void HandleAchievementsLoaded(IAchievement[] achievements)
    {
        Debug.Log("* HandleAchievementsLoaded");
        foreach (IAchievement achievement in achievements)
        {
            Debug.Log("* achievement = " + achievement.ToString());
        }
    }

    /// <summary>
    /// 处理成就描述
    /// </summary>
    private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)
    {
        Debug.Log("*** HandleAchievementDescriptionsLoaded");
        foreach (IAchievementDescription achievementDescription in achievementDescriptions)
        {
            Debug.Log("* achievementDescription = " + achievementDescription.ToString());
        }
    }

    // achievements

    public void ReportProgress(string achievementId, double progress)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(achievementId, progress, HandleProgressReported);
        }
    }

    private void HandleProgressReported(bool success)
    {
        Debug.Log("*** HandleProgressReported: success = " + success);
    }

    /// <summary>
    /// 显示成就UI
    /// </summary>
    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    // leaderboard

    public void ReportScore(string leaderboardId, long score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboardId, HandleScoreReported);
        }
    }

    public void HandleScoreReported(bool success)
    {
        Debug.Log("*** HandleScoreReported: success = " + success);
    }

    /// <summary>
    /// 显示排行榜UI
    /// </summary>
    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

}