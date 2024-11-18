using System;
using Sandbox.UI;
using Sandbox.Events;

namespace astral_base.SCPRP;

public partial class Chat
{
	[HostSync]
	public static Chat Instance { get; private set; }
	public List<ChatMessage> Messages { get; set; } = new();
	private bool ShouldShowMessages => Messages.Count > 0 || ChatToggle;
	private bool ChatToggle;
	private string MessageText { get; set; } = "";
	private TextEntry InputBox;

	protected override int BuildHash() => System.HashCode.Combine( Messages, ChatToggle, MessageText );
	[Authority]
	protected override void OnAwake() { Instance = this; }
	[Authority]
	protected override void OnDestroy() { Instance = null; }
	
	public static Chat GetChat() { return Instance; }

	[Authority]
    private void HideInput() {
		ChatToggle = false;
		StateHasChanged();
	}

	[Authority]
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

	[Authority]
	private void SendMessage()
	{
        ChatToggle = false;
        StateHasChanged();

		var sender = Players.GetPlayer(Rpc.Caller.Id);

		if(!sender.IsValid()) {
			return;
		}

		if ( string.IsNullOrWhiteSpace( MessageText ) ) { return;}
		var msg = new ChatMessage()
		{
			Text = MessageText.Substring(2).Trim(),
			Author = sender.GetSteamID(),
            DisplayName = sender.DisplayName,
			Time = DateTime.Now,
			IsActive = true,
			IsOOC = MessageText.StartsWith("//")
		};
		Scene.Dispatch( new PlayerSendMessage( sender, msg ) );
        MessageText = "";
	}

	[Broadcast]
	public void AddMessage( ChatMessage message, Player player)
	{
		if(!(Player.Local.GetCharacterController().WorldPosition.Distance( player.GetCharacterController().WorldPosition ) < 300) && !(Player.Local == player) && !message.IsOOC) {
			return;
		}

		this.Messages.Add( message );

		StateHasChanged();
	}
}