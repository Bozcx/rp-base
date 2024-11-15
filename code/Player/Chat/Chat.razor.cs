using System;
using Sandbox.UI;
using Sandbox.Events;

namespace astral_base.SCPRP;

public partial class Chat
{
	public static Chat Instance { get; private set; }
	public List<ChatMessage> Messages { get; set; } = new();
	private bool ShouldShowMessages => Messages.Count > 0 || ChatToggle;
	private bool ChatToggle;
	private string MessageText { get; set; } = "";
	private TextEntry InputBox;


	protected override int BuildHash() => System.HashCode.Combine( Messages, ChatToggle, MessageText );
	protected override void OnAwake() { Instance = this; }
	protected override void OnDestroy() { Instance = null; }

	public static Chat GetChat() { return Instance; }

    private void HideInput() {
		ChatToggle = false;
		StateHasChanged();
	}

	protected override void OnFixedUpdate()
	{
		if ( Input.EscapePressed ) {
			ChatToggle = false;
			StateHasChanged();
		}

		if ( Input.Pressed( "Chat" ) ) {
			if ( !ChatToggle )
			{
				ChatToggle = true;
				StateHasChanged();
				InputBox?.Focus();
				return;
			}

			SendMessage();
		}

		if ( Messages.RemoveAll( x => x.Time < DateTime.Now.AddSeconds( -600 ) ) > 0 ) {
			StateHasChanged();
		}

		// When the message is there for 10+ seconds we want to fade it out of the screen. (If the player opens it they see it again.)
		// foreach (var message in Messages.Where(x => x.Time < DateTime.Now.AddSeconds(-10))) {
		// 	message.IsActive = false;
		// 	StateHasChanged();
		// }
	}


	private void SendMessage()
	{
        ChatToggle = false;
        StateHasChanged();

		if ( string.IsNullOrWhiteSpace( MessageText ) ) { return;}
        AddMessage( MessageText );
        MessageText = "";
	}

	[Authority]
	public void AddMessage( string message)
	{
		var msg = new ChatMessage()
		{
			Text = message,
			Author = Player.Local.GetSteamID(),
            DisplayName = Player.Local.DisplayName,
			Time = DateTime.Now,
			IsActive = true
		};

		Scene.Dispatch( new PlayerSendMessage( Player.Local, msg ) );
        
		StateHasChanged();
	}
}