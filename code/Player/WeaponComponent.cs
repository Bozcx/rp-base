namespace astral_base.RPBASE;

public class WeaponComponent : Component
{
    public Item item { get; set; }

    	[Authority]
	protected override void OnAwake()
    {
		if ( !Input.Down("attack1") ) { return; }
        if ( item == null ) { return; }
        item.FirePrimary();
    }
}