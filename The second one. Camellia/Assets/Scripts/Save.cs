using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {
	public float charPositionX;
	public float charPositionY;
	public float health;
	public bool[] itemsState;
	public int notesFound;
	public int currentDialog;
	public int currentLine;
	public bool haveKey; 
	public bool haveBook; 
	public bool pillarFalled;
	public bool noBridge;

	public Save(float posX, float posY, float health, bool[] items, int notes, int dialog, int line, bool key, bool book, bool pillar, bool bridge){
		charPositionX = posX;
		charPositionY = posY;
		this.health = health;
		itemsState = items;
		notesFound = notes;
		currentDialog = dialog;
		currentLine = line;
		haveKey = key;
		haveBook = book;
		pillarFalled = pillar;
		noBridge = bridge;
	}
}
