using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class websocket : MonoBehaviour {

	WebSocket ws;
	Queue wsQueue;
	int messageCount;
	public Text textObject;
	public GameObject parentObject;
	public InputField inputField;

	// Use this for initialization
	void Start () {
		wsQueue = Queue.Synchronized(new Queue());
		ws = new WebSocket("ws://127.0.0.1:8080");

		ws.OnOpen += (sender, e) => {
			Debug.Log ("WebSocket Open");
		};
		
		ws.OnMessage += (sender, e) => {
			Debug.Log("WebSocket Message Type: " + e.GetType() + ", Data: " + e.Data);
			if (e.IsText) {
				wsQueue.Enqueue(e.Data);
			}
		};

		ws.OnError += (sender, e) => {
			Debug.Log("WebSocket Error Message: " + e.Message);
		};

		ws.OnClose += (sender, e) => {
			Debug.Log("WebSocket Close");
		};

		ws.Connect();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp("s")) {
			ws.Send("Test Message");
		}
		if (Input.GetKeyUp("q")) {
			ws.Close();
		}
		lock (wsQueue.SyncRoot) {
			if (wsQueue.Count > 0) {
				var data = wsQueue.Dequeue () as string;
				Debug.Log("Message Received: " + data);
				var text = Instantiate (textObject) as Text;
				text.transform.SetParent(parentObject.transform, false);
				text.text = data;
			}
		}
	}

	public void emitMessage(string text) {
		if (ws.IsAlive) {
			ws.Send (text);
		}
	}

	public void submitText() {
		if (ws.IsAlive) {
			ws.Send (inputField.text);
			inputField.text = "";
		}
	}
}
