using System.Threading.Tasks;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Wallet;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Land : MonoBehaviour {

  // Static variable to keep track of attached instances of this script
  private Account accountInfo;
  private List<Solana.Unity.Wallet.Account> accountInfoList;
  private bool scriptAttached = false;
  private static readonly IRpcClient rpcSolanaClient = ClientFactory.GetClient(Cluster.MainNet);
  public int xPos = 0; // Position X.
  public int yPos = 0; // Position Y.
  public string nftSolanaAddress = ""; // NFT Solana address.

  void Awake() {
    // Check if this script is already attached to the GameObject.
    if (scriptAttached) {
      Debug.LogWarning("Script already attached to this GameObject.");
      Destroy(this);
      return;
    }

    // Attach the script to the GameObject if it's not already attached.
    scriptAttached = true;
  }

  public async void GetAccountInfo()
  {
      // Convert the NFT address string to a PublicKey object.
      PublicKey nftPublicKey = new PublicKey(nftSolanaAddress);
      // Get the account info of the NFT.
      var accountInfoResult = await rpcSolanaClient.GetAccountInfoAsync(nftPublicKey);
      if (accountInfoResult.WasSuccessful) {
        //accountInfo = accountInfoResult.Result.Value.Data;
        //accountInfoList = new List<Solana.Unity.Wallet.Account>();
        List<AccountInfo> accountInfoList = new List<AccountInfo>();
        accountInfoList.Add(accountInfoResult.Result.Value);
      }
      else
      {
          Debug.LogError($"Failed to get account info: {accountInfoResult}");
      }
      PrintAccountInfo(accountInfoList);
      // accountInfo = await rpcSolanaClient.GetAccountInfoAsync(nftPublicKey);
  }

  void OnDestroy() {
    // Reset the scriptAttached variable when the script is destroyed.
    scriptAttached = false;
  }

  public void PrintAccountInfo(List<Solana.Unity.Wallet.Account> accountInfoList)
  {
      foreach(var accountInfo in accountInfoList)
      {
          Debug.Log($"Account owner: {accountInfo.PublicKey}");
      }
  }

  // Start is called before the first frame update.
  void Start() {
    InvokeRepeating("UpdatePerMinute", 2.0f, 60.0f);
    InvokeRepeating("UpdatePerSecond", 1.0f, 1.0f);
  
    if (nftSolanaAddress.Length > 0) {
      Debug.Log("NFT: " + nftSolanaAddress);
      GetAccountInfo();
    }
  }

  // Update is called once per frame.
  void Update() {}

  // Update is called once per minute.
  void UpdatePerMinute() {
    if (nftSolanaAddress.Length > 0) {
      Debug.Log("NFT3: " + nftSolanaAddress);
    }
  }

  // Update is called once per second.
  void UpdatePerSecond() {
    if (nftSolanaAddress.Length > 0) {
      // Debug.Log("NFT2: " + nftSolanaAddress);
    }
  }

}