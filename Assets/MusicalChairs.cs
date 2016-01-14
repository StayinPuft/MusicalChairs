using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Util;

public class MusicalChairs : MonoBehaviour {

	static float bpm = 131;
	static float startTime = 1.058333f;
	public int numPlayers;
	public AudioSource audio;
	public bool start;
	public InputHandler input;
	public LightHandler lightHandler;
	public bool pos;
	public IEnumerator cool;
	public int stopTime;
	public int counter;
	public int[] sideNotes = new int[]{8, 24, 40, 56, 72, 88, 104, 120};
	public InputField inputField;

	// Use this for initialization
	void Start () {
		numPlayers = 5;
		audio = this.GetComponent<AudioSource>();
		StartCoroutine (StartSequence ());
	}

	// Update is called once per frame
	void Update () {
		if(numPlayers == 0){
			StopAllCoroutines ();
			audio.UnPause();
			foreach(int i in input.notes){
				lightHandler.Blink (i, Colors.GREEN);
			}
			foreach(int i in sideNotes){
				lightHandler.Light (i, Colors.OFF);
			}
			numPlayers --;
		}
	}

	public IEnumerator StartSequence(){
		yield return StartCoroutine(WaitingForInput());
		audio.Play	();
		yield return new WaitForSeconds(startTime);
		input.Reset ();
		StartCoroutine (Go ());
	}

	public IEnumerator WaitingForInput(){
		int i = 0;
		while(i <= 1){
			if(Input.GetKeyDown(KeyCode.Return)){
				int.TryParse (inputField.text, out i);
			}
			yield return null;
		}
		numPlayers = i;
	}

	IEnumerator Cool(){
		if(pos){
			for(int i = 0; i < 8; i++){
				int note = sideNotes[Mathf.Abs (i)];
				lightHandler.Light (note, Colors.GREEN);
				yield return new WaitForSeconds(.05f);
				lightHandler.Light (note, Colors.OFF);
			}
		}else{
			for(int i = 7; i >= 0; i--){
				int note = sideNotes[Mathf.Abs (i)];
				lightHandler.Light (note, Colors.GREEN);
				yield return new WaitForSeconds(.05f);
				lightHandler.Light (note, Colors.OFF);
			}
		}
	}
	IEnumerator Go(){
		List<int> notes = input.notes;
		List<int> lights = new List<int>();
		this.cool = Cool ();
		StartCoroutine (this.cool);
		stopTime = Random.Range (40,60);
		Debug.Log(stopTime);
		counter = 0;
		while(true){
			Debug.Log (counter);
			lights = new List<int>();
			for(int i = 0; i < numPlayers; i++){
				int n = notes[Random.Range (0, notes.Count)];
				if(!lights.Contains (n)) lights.Add (n);
				else i--;
			}
			foreach(int note in lights){
				lightHandler.Light (note, Colors.AMBER);
			}
			yield return new WaitForSeconds(.44982f);
	//		yield return new WaitForSeconds(0.343916666f);
		//	counter++;
			if(counter == stopTime){
				counter = 0;
				StopCoroutine(this.cool);
				audio.Pause ();
				yield return StartCoroutine (WaitingForInput(lights));
				StartCoroutine (this.cool);
				audio.UnPause ();
				stopTime = Random.Range (40, 60);
			}
			foreach(int note in lights){
				lightHandler.Light (note, Colors.OFF);
			}
			StopCoroutine (this.cool);
			pos = !pos;
			this.cool = Cool ();
			StartCoroutine (this.cool);
		}
	}
	IEnumerator WaitingForInput(List<int> lights){
		bool[] pressed = new bool[lights.Count];
		bool done = false;
		foreach(int i in sideNotes){
			lightHandler.Light(i, Colors.RED);
		}
		while(!done){
			for(int i = 0; i < lights.Count; i++){
				if(MidiInput.GetKeyDown (lights[i])){
					pressed[i] = true;
					lightHandler.Light (lights[i], Colors.GREEN);
				}
			}
			foreach(bool b in pressed){
				done = b;
				if(!b)break;
			}
			yield return null;
		}
		foreach(int i in sideNotes){
			lightHandler.Light (i, Colors.GREEN);
		}
		input.Reset();
		numPlayers--;
	}
}
