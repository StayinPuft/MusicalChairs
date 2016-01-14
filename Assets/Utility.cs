using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
public static class Utility{

	public static int GetNote(int x, int y){
		return (Mathf.Abs (y - 7)*16) + x;
	}

	public static Vector2 GetVec2(int note){
		for(int i = 7; i >= 0; i--){
			if(note >= i*16){
				return new Vector2(note % 16, 7 - i);
			}
		}
		return new Vector2(-1,-1);
	}

	public static List<Vector2> GetMovePoints(SType type, Vector2 pos){
		List<Vector2> points = new List<Vector2>();
		switch(type){
		case SType.ROOK:
			for(int i = (int)pos.x + 1; i < 8; i++){
				points.Add (new Vector2(i, pos.y));
			}
			for(int i = (int)pos.y + 1; i < 8; i++){
				points.Add (new Vector2(pos.x, i ));
			}
			for(int i = (int)pos.x-1; i > -1; i--){
				points.Add (new Vector2(i , pos.y));
			}
			for(int i = (int) pos.y - 1; i > -1; i--){
				points.Add (new Vector2(pos.x, i));
			}
			break;
		case SType.BOMB:
			if(pos.x < 7) points.Add (new Vector2(pos.x + 1, pos.y));
			if(pos.x > 0) points.Add (new Vector2(pos.x - 1, pos.y));
			if(pos.y < 7) points.Add (new Vector2(pos.x, pos.y + 1));
			if(pos.y > 0) points.Add (new Vector2(pos.x, pos.y - 1));
			break;	
		}
		return points;
	}

	public static List<Vector2> GetAttackPoints(SType type, Vector2 pos){
		List<Vector2> points = new List<Vector2>();
		switch(type){
		case SType.ROOK:
			for(int i = (int)pos.x + 1; i < 8; i++){
				points.Add (new Vector2(i, pos.y));
			}
			for(int i = (int)pos.y + 1; i < 8; i++){
				points.Add (new Vector2(pos.x, i ));
			}
			for(int i = (int)pos.x-1; i > -1; i--){
				points.Add (new Vector2(i , pos.y));
			}
			for(int i = (int) pos.y - 1; i > -1; i--){
				points.Add (new Vector2(pos.x, i));
			}
			break;
		case SType.BOMB:
			for(int i = (int)pos.x - 2; i <= pos.x + 2; i++){
				if(i >= 0 && i < 8){
					for(int j = (int)pos.y; j <= pos.y + 2; j++){
						if(i > pos.x - 2 && i < pos.x + 2){
						}
//						if(j >=0 && j < 
					}
				}
			}
			break;
		}
		return points;
	}
}

namespace Util{
	public enum Colors{
		LIGHTRED = 13,
		RED = 15,
		ORANGE = 47,
		YELLOW = 62,
		AMBER = 63,
		GREEN = 60,
		LIGHTGREEN = 28,
		OFF = 12
	};

	public enum SType{
		ROOK,
		BOMB
	};

	public enum Light{
		BLINK,
		ON,
		OFF
	};
}
