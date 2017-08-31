using UnityEngine.UI;

public class OptionsMenu : SimpleMenuScaleInOut<OptionsMenu>
{
	public Slider Slider;

	public void OnMagicButtonPressed()
	{
		AwesomeMenu.Show(Slider.value);
	}
}
