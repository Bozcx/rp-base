// using System;
// using Sandbox;
// using Sandbox.UI;

// namespace astral_base.RPBASE;

// public partial class Vitals
// {
//     public Player LocalPlayer { get; set; }
// 	public float SaveHealth { get; set; }

// 	protected void OnFixedUpdate()
// 	{
// 		var CurHealth = LocalPlayer.GetHealth();
// 		if (CurHealth != null && CurHealth == SaveHealth) { return; }
// 		SaveHealth = LocalPlayer.GetHealth();

// 		Log.Info($"New Health For Player!");

// 		StateHasChanged();
// 		BuildHash();
// 	}
    
//     protected override int BuildHash()
// 	{
// 		return HashCode.Combine( LocalPlayer );
// 	}
// }