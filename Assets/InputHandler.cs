using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour {

	public List<int> notes = new List<int>();
//	public MusicalChairs mc;
//	public Arena arena;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 8; i++){
			for(int j = 0; j < 8; j++){
				notes.Add ((i*16) + j)	;
				MidiOut.SendNoteOn (MidiChannel.Ch1, (i*16) + j, 12/127f);
			}
		}
//		StartCoroutine(mc.StartSequence());
	}
	
	// Update is called once per frame
	void Update () {
		foreach(int i in notes){
			if(MidiInput.GetKeyDown(i)){
//				arena.ButtonPressed (i);
			}
		}
		if(Input.GetKeyDown (KeyCode.R)){
			Reset ();
		}
	}

	public void Reset(){
		foreach(int i in notes){
			MidiOut.SendNoteOn (MidiChannel.Ch1, i, 12/127f);
		}
	}
}
