 using System;
 using Eflatun.SceneReference;
 using UnityEngine;

namespace Shmup
{
    /// <summary>
    /// 游戏管理器，负责管理游戏状态、玩家、Boss、分数和游戏结束逻辑
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneReference mainMenuScene;
        [SerializeField] private GameObject gameOverUI;
        
        public static GameManager Instance { get; private set; }
        
        public Player Player => player;
        
        private Player player;
        private Boss boss;
        private int score;
        private float restartTimer = 3f;
        
        /// <summary>
        /// 检查游戏是否结束
        /// </summary>
        /// <returns>当玩家生命值归零或燃料耗尽或Boss被击败时返回true</returns>
        public bool IsGameOver() => player.GetHealthNormalized() <= 0 || player.GetFuelNormalized() <= 0 || boss.GetHealthNormalized() <= 0;

        private void Awake()
        {
            Instance = this;

            // 获取场景中的玩家和Boss对象
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        }

        private void Update()
        {
            if (IsGameOver())
            {
                restartTimer -= Time.deltaTime;

                if (gameOverUI.activeSelf == false)
                {
                    gameOverUI.SetActive(true);
                }
                
                if (restartTimer <= 0)
                {
                    Loader.Load(mainMenuScene);
                }
            }
        }

        /// <summary>
        /// 增加游戏分数
        /// </summary>
        /// <param name="amount">要增加的分数数量</param>
        public void AddScore(int amount) => score += amount;
        
        /// <summary>
        /// 获取当前游戏分数
        /// </summary>
        /// <returns>当前游戏分数</returns>
        public int GetScore() => score;
    }
}
