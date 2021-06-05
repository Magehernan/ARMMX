using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPMManager : MonoBehaviour {
	[SerializeField]
	private Slider slider;
	[SerializeField]
	private TextMeshProUGUI textRpm;
	[SerializeField]
	private Rotator rotartor;

	private void Awake() {
		slider.onValueChanged.AddListener(OnRPMChange);
		OnRPMChange(slider.value);
	}


	private void OnRPMChange(float rpm) {
		rpm = Mathf.Round(rpm);
		textRpm.text = rpm.ToString("0");
		rotartor.SetRPM(-rpm);
	}
}
