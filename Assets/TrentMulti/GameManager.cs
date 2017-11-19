﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 


namespace Com.MyCompany.MyGame
{
	public class GameManager : Photon.PunBehaviour {

		public GameObject playerPrefab;


		void Start() {
			if (playerPrefab == null) 
			{
				Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			} 
			else
			{
				if (PlayerManage2P.LocalPlayerInstance==null)
				{
					Debug.Log("We are Instantiating LocalPlayer from "+Application.loadedLevelName);
					// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
                   
				}
				else
				{
					Debug.Log("Ignoring scene load for "+Application.loadedLevelName);
				}
			}
		}

		#region Photon Messages

		public override void OnPhotonPlayerConnected(PhotonPlayer other)
		{
			Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting


			if (PhotonNetwork.isMasterClient) 
			{
				Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


				LoadArena();
			}
		}


		public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
		{
			Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects


			if (PhotonNetwork.isMasterClient) 
			{
				Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


				LoadArena();
			}
		}


		#endregion
		#region

		/// <summary>
		/// Called when the local player left the room. We need to load the launcher scene.
		/// </summary>
		public override void OnLeftRoom()
		{
			SceneManager.LoadScene(0);
		}


		#endregion

		#region Private Methods

		void LoadArena()
		{
			if (!PhotonNetwork.isMasterClient) 
			{
				Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			}
			Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount);
			//PhotonNetwork.LoadLevel("Room for "+PhotonNetwork.room.playerCount);
		}


		#endregion


		#region Public Methods


		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}


		#endregion  
	}
}