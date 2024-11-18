namespace astral_base.RPBASE;

public class hands
{
	public GameObject ViewModelPrefab { get; set; }
	public Model WorldModel { get; set; }

	public void Deploy() {
        Log.Info("Deployed Hands");
    }
	public void UnDeploy() {}
	public void FirePrimary() {
        Log.Info("Pew!");
    }
}