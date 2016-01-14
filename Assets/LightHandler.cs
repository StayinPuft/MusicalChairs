using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Util;

public class LightHandler : MonoBehaviour{

	public Dictionary<int, Colors> map = new Dictionary<int, Colors>();
	public Dictionary<int, bool> blinkMap = new Dictionary<int, bool>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Light(int note, Colors color){
		map[note] = color;
		MidiOut.SendNoteOn (MidiChannel.Ch1, note, ((int)color)/127f);
	}

	public void Blink(int note, Colors color){
		StartCoroutine(Blink (note, (int)color));
	}

	private IEnumerator Blink(int note, int color){
		if(blinkMap.ContainsKey (note)){
			blinkMap[note] = true;
		}else blinkMap.Add (note, true);
		bool flag = true;
		Colors defaultColor;
		while(flag){
			MidiOut.SendNoteOn (MidiChannel.Ch1, note, color/127f);
			yield return new WaitForSeconds(0.5f);
	//		defaultColor = map[note];
			if(!map.TryGetValue (note, out defaultColor)){
				map.Add (note, Colors.OFF);
			}
			MidiOut.SendNoteOn (MidiChannel.Ch1, note, ((int)defaultColor)/127f);
			yield return new WaitForSeconds(0.2f);
			flag = blinkMap[note];
		}
	}
}
