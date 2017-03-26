using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class pointsGen:EditorWindow {

	//data about point type
	public int npoints; 
	public bool squaregrid;
	public bool randomPlacement;
	public bool lineup;
	public int squareDis;
	public int minDisX;
	public int MaxDisX;
	public int minDisZ;
	public int MaxDisZ;
	public bool uniformdis; 


	[MenuItem("CityGen/Points Placer")]

	static void Init() {
		EditorWindow window = GetWindow(typeof(pointsGen));
		window.Show();
	}


	void OnGUI() {
		EditorGUILayout.BeginHorizontal ();
		npoints = EditorGUILayout.IntField ("array size?", npoints); 
		squaregrid = EditorGUILayout.Toggle ("Create square grid?",squaregrid);
		randomPlacement = EditorGUILayout.Toggle ("Random placement?", randomPlacement);
		lineup = EditorGUILayout.Toggle ("building lineup", lineup);
		EditorGUILayout.EndHorizontal ();

		if(squaregrid)
		{
			EditorGUILayout.BeginHorizontal ();
			squareDis = EditorGUILayout.IntField ("distance between", squareDis); 
			EditorGUILayout.EndHorizontal ();

			if(GUILayout.Button("Generate array")) {

				int xdis = 0, zdis = 0;

				for (int i = 0; i < npoints; i++)
				{

					for(int j=0;j<npoints;j++) {
						GameObject pt = new GameObject ("pt");
						zdis += squareDis;
						pt.transform.position = new Vector3 (xdis, 0, zdis); 
					}
					zdis = 0;
					xdis += squareDis;
				}


			}


			
		}

		else if(randomPlacement)
		{
			EditorGUILayout.BeginHorizontal ();
			minDisX = EditorGUILayout.IntField ("min x distance", minDisX); 
			MaxDisX = EditorGUILayout.IntField ("max x distance", MaxDisX);

			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			minDisZ = EditorGUILayout.IntField ("min z distance", minDisZ); 
			MaxDisZ = EditorGUILayout.IntField ("max z distance", MaxDisZ);

			EditorGUILayout.EndHorizontal ();


			if(GUILayout.Button("Generate random")) {
				//do things

				int zrdis=0, xrdis=0;

				for (int i = 0; i < npoints; i++)
				{

					for(int j=0;j<npoints;j++) {
						GameObject pt = new GameObject ("pt");
						xrdis = Random.Range (minDisX, MaxDisX) ;
						pt.transform.position = new Vector3 (xrdis,0,Random.Range(minDisZ,MaxDisZ)); 
					}

					minDisX += Random.Range (minDisX+10, minDisX*2);
					MaxDisX += Random.Range (MaxDisX+10, MaxDisX*2);
	
				}



			}

		}



		else if(lineup) {


			EditorGUILayout.BeginHorizontal ();
			uniformdis = EditorGUILayout.Toggle ("Uniform distance?",uniformdis);
			EditorGUILayout.EndHorizontal ();


			if(uniformdis) {

				EditorGUILayout.BeginHorizontal ();
				squareDis = EditorGUILayout.IntField ("distance?",squareDis);
				EditorGUILayout.EndHorizontal ();


				if(GUILayout.Button("generate lineup")) {

					for(int a=0;a<npoints;a++) {
						GameObject g = new GameObject ("pt");
						g.transform.position = new Vector3 (0, 0, a + squareDis);
					}
				}


			}


			if(!uniformdis) {

				EditorGUILayout.BeginHorizontal ();
				minDisZ = EditorGUILayout.IntField ("min distance?",minDisZ);
				MaxDisZ = EditorGUILayout.IntField ("max distance?",MaxDisZ);
				EditorGUILayout.EndHorizontal ();


				if(GUILayout.Button("generate lineup")) {

					for(int a=0;a<npoints;a++) {
						GameObject g = new GameObject ("pt");
						g.transform.position = new Vector3 (0, 0, a + Random.Range(minDisZ,MaxDisZ));
					}
				}


			}

			
		}





	}


}
