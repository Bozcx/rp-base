using Sandbox.Utility;
using Sandbox.Events;

namespace astral_base.SCPRP;

public partial class Player
{
    [HostSync]
	[Property]
    private Job Job { get; set; }

	[Authority]
	[Broadcast( NetPermission.HostOnly )]
    public void SetJob(Job job)
    {
        Log.Info( $"{this} has changed their Job from {job} to: {job}" );
        Job = job;
    }

    public Job GetJob()
    {
        return this.Job;
    }
}