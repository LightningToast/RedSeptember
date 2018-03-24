using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class PlayerFirebaseController : MonoBehaviour {
    int counter = 0;
    DatabaseReference reference;

    public GameObject submarine;
    SubControl subController;
	// Use this for initialization
	void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://redseptember-5c74c.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        reference.Child("Missions/Match1/Sub").Child("Dive").SetValueAsync(0);
        reference.Child("Missions/Match1/Sub").Child("Engine").SetValueAsync(0);
        reference.Child("Missions/Match1/Sub").Child("Pitch").SetValueAsync(0);
        reference.Child("Missions/Match1/Sub").Child("Torpedo").SetValueAsync(0);

        FirebaseDatabase.DefaultInstance.GetReference("Missions/Match1/Sub/Dive").ValueChanged += HandleDiveChanged;
        FirebaseDatabase.DefaultInstance.GetReference("Missions/Match1/Sub/Engine").ValueChanged += HandleEngineChanged;
        FirebaseDatabase.DefaultInstance.GetReference("Missions/Match1/Sub/Pitch").ValueChanged += HandlePitchChanged;
        FirebaseDatabase.DefaultInstance.GetReference("Missions/Match1/Sub/Torpedo").ValueChanged += HandleTorpedoChanged;

        subController = submarine.GetComponent<SubControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        print("" + args.Snapshot.Child("/Dive"));
        subController.SetDive(float.Parse(args.Snapshot.Child("/Dive").Value.ToString()));
        print("" + args.Snapshot.Child("/Engine"));
        subController.SetEngine(float.Parse(args.Snapshot.Child("/Engine").Value.ToString()));
        print("" + args.Snapshot.Child("/Pitch"));
        subController.SetPitch(float.Parse(args.Snapshot.Child("/Pitch").Value.ToString()));
        print("" + args.Snapshot.Child("/Torpedo"));
        subController.SetTorpedo();
    }
    void HandleDiveChanged(object sender, ValueChangedEventArgs args)
    {
        //print("" + args.Snapshot.Child("/Dive"));
        subController.SetDive(float.Parse(args.Snapshot.Value.ToString()));

    }
    void HandleEngineChanged(object sender, ValueChangedEventArgs args)
    {
        
        //print("" + args.Snapshot.Child("/Engine"));
        subController.SetEngine(float.Parse(args.Snapshot.Value.ToString()));

    }
    void HandlePitchChanged(object sender, ValueChangedEventArgs args)
    {
        //print("" + args.Snapshot.Child("/Pitch"));
        subController.SetPitch(float.Parse(args.Snapshot.Value.ToString()));

    }
    void HandleTorpedoChanged(object sender, ValueChangedEventArgs args)
    {
        
        //print("" + args.Snapshot.Child("/Torpedo"));
        subController.SetTorpedo();
    }
    public void SendData()
    {
        counter++;

        reference.Child("Missions/Match1/Sub").Child("Dive").SetValueAsync(counter);
    }
    public void SendDive(float val) {
        float finVal = val - 0.5f;
        reference.Child("Missions/Match1/Sub").Child("Dive").SetValueAsync(finVal);
    }
    public void SendEngine(float val)
    {
        float finVal = val - 0.5f;
        reference.Child("Missions/Match1/Sub").Child("Engine").SetValueAsync(finVal);
    }
    public void SendPitch(float val)
    {
        float finVal = val - 0.5f;
        reference.Child("Missions/Match1/Sub").Child("Pitch").SetValueAsync(finVal);
    }
    public void SendTorpedo()
    {
        counter++;
        reference.Child("Missions/Match1/Sub").Child("Torpedo").SetValueAsync(counter);
    }
}
