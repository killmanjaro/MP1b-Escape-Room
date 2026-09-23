using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Coin : MonoBehaviour
{
	public CoinUIManager CoinManager;
    public void CollectCoin(SelectEnterEventArgs args)
    {
		CoinManager.AddCoin(1);
        // Update UI via manager instance
        if (CoinUIManager.Instance != null)
        {
            CoinUIManager.Instance.AddCoin(1);
        }

        // Destroy the coin object
        Destroy(gameObject);
    }
}