using GameFramework;

namespace MyGame
{
    using d4160.GameFramework;
    using UnityEngine;
    using Photon.Pun;
    using Photon.Realtime;

    public class MultiplayerModeLauncher : DefaultPUNModeLauncher
    {
        [Tooltip("The prefab to use for representing the player.")]
        public GameObject playerPrefab;

        public override void OnEnable()
        {
            base.OnEnable();

            PhotonNetwork.OnSyncLevelLoad += OnSyncLevelLoad;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            PhotonNetwork.OnSyncLevelLoad -= OnSyncLevelLoad;
        }

        protected void OnSyncLevelLoad(int sceneBuildIndex)
        {
            Debug.Log($"OnSyncLevelLoad{sceneBuildIndex}");

            GameManager.Instance.UnloadLevel(LevelType.GameMode, 1, () => {

                GameManager.Instance.LoadLevel(LevelType.GameMode, 1);
            });
        }

        public override void SetReadyToPlay()
        {
			if (!PhotonNetwork.IsConnected)
			{
				GameManager.Instance.UnloadAllStartedLevels(() => {
                    GameManager.Instance.LoadLevel(LevelType.General, 1);
                });

				return;
			}

            base.SetReadyToPlay();

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
			}
            else
            {
				if (NetworkingPlayer.LocalEntityInstance == null)
				{
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

                    //	we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    NetworkingPlayer.LocalEntityInstance = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
                }
			}
        }

        public override void Play()
        {
            base.Play();
        }

        public override void SetGameOver(PlayResult result)
        {
            base.SetGameOver(result);
        }

        /// <summary>
        /// Called when a Photon Player got connected. We need to then load a bigger scene.
        /// </summary>
        /// <param name="other">Other.</param>
        public override void OnPlayerEnteredRoom(Player other  )
		{
			Debug.Log( "OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

			if ( PhotonNetwork.IsMasterClient )
			{
                // Don't need load again the level, since is the same
				// LoadLevel();
			}
		}

        /// <summary>
		/// Called when a Photon Player got disconnected. We need to load a smaller scene.
		/// </summary>
		/// <param name="other">Other.</param>
		public override void OnPlayerLeftRoom( Player other  )
		{
			Debug.Log( "OnPlayerLeftRoom() " + other.NickName ); // seen when other disconnects

			if ( PhotonNetwork.IsMasterClient )
			{
                // Don't need load again the level, since is the same
                // LoadLevel();
            }
        }

        /// <summary>
		/// Called when the local player left the room. We need to load the launcher scene.
		/// </summary>
		public override void OnLeftRoom()
		{
            Debug.Log( "OnLeftRoom()" );

            // Load the Menu level
            GameManager.Instance.UnloadLevel(LevelType.GameMode, 1, () => {
                GameManager.Instance.LoadLevel(LevelType.General, 1);
            });
        }

        public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}

        #region Private Methods

		protected void LoadLevel()
		{
			if ( !PhotonNetwork.IsMasterClient )
			{
				Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
			}

			Debug.LogFormat($"PhotonNetwork : Loading Level for {PhotonNetwork.CurrentRoom.PlayerCount} players");

            GameManager.Instance.UnloadLevel(LevelType.GameMode, 1, () => {
                GameManager.Instance.LoadLevel(LevelType.GameMode, 1);
            });
        }
        #endregion

        #region Misc

        // How to change scene of chapter without more nodes
        //if (GameManager.Instance.GetPlayLevelLauncher(1) is DefaultPlayLauncher)
        //{
        //    var chapter = CurrentChapter;
        //    var chapter = (GameManager.Instance.GetPlayLevelLauncher(1) as DefaultPlayLauncher).CurrentChapter;
        //    chapter.LevelScene = new LevelScene() { levelCategory = 6, levelScene = PhotonNetwork.CurrentRoom.PlayerCount };
        //}

        #endregion
    }
}