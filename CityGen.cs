using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class CityGen : EditorWindow
{


	//Data about modes

	public bool totallyRandom;
	public bool pointMode;

	//Data about primitive
	public int height;
	public int width;

	//Data about building to be generated
	public int floors;
	public int minfloor;
	public bool randomFloors;
	public int xpos;
	public int zpos;

	//Data about building generation type

	public bool manygen;
	public int XrandomRange;
	public int ZrandomRange;
	public int iter;

	//Data about generation ar points
	public GameObject transformPos;
	public GameObject[] primitives;
	public bool rot;
	public bool placer;

	int f;

	[MenuItem("CityGen/Procedural City Generation tool")]

	static void Init() {
		EditorWindow window = GetWindow(typeof(CityGen));
		window.Show();
	}

	void OnGUI() {



		EditorGUILayout.BeginHorizontal();
		totallyRandom = EditorGUILayout.Toggle ("random placement mode ", totallyRandom);
		pointMode = EditorGUILayout.Toggle ("Point mode placement", pointMode);

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal();
		height = EditorGUILayout.IntField ("Height of primitive: ", height);
		width = EditorGUILayout.IntField ("width of primitive:", width);

		EditorGUILayout.EndHorizontal ();


		EditorGUILayout.BeginHorizontal();

		floors = EditorGUILayout.IntField ("max floors: ", floors);
		minfloor = EditorGUILayout.IntField ("min floors:", minfloor); 
		xpos = EditorGUILayout.IntField ("Instantiate at X position :", xpos);
		zpos = EditorGUILayout.IntField ("Instantiate at Z position :", zpos);
		randomFloors = EditorGUILayout.Toggle ("Random floors?", randomFloors);

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal();

		manygen = EditorGUILayout.Toggle ("Generate many buildings??", manygen);
		XrandomRange = EditorGUILayout.IntField ("X axis randomness:", XrandomRange);
		ZrandomRange = EditorGUILayout.IntField ("Z axis randomness:", ZrandomRange);
		iter = EditorGUILayout.IntField ("iterations:", iter);

		EditorGUILayout.EndHorizontal ();



		EditorGUILayout.BeginHorizontal();

		placer = EditorGUILayout.Toggle ("Generate at points:", placer);
		rot = EditorGUILayout.Toggle ("Allow random rotation:", rot);
		transformPos = EditorGUILayout.ObjectField ( "Transform object", transformPos, typeof(GameObject)) as GameObject;


		ScriptableObject target = this;
		SerializedObject so = new SerializedObject(target);
		SerializedProperty stringsProperty = so.FindProperty("primitives");

		EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
		so.ApplyModifiedProperties(); // Remember to apply modified properties


		EditorGUILayout.EndHorizontal ();



		if (GUILayout.Button("Start generation")) {

			if (totallyRandom) {
				if (!manygen) {
					if (!randomFloors) {
						int counter = 0;
						for (int x = 0; x < floors; x++) {

							GameObject go = PrefabUtility.InstantiatePrefab (Selection.activeObject as GameObject) as GameObject; 

							go.transform.position = new Vector3 (xpos, counter + height, 0);
							counter += height;
						}
					} else if (randomFloors) {
						int counter = 0;
						for (int x = 0; x < Random.Range (minfloor, floors); x++) {

							GameObject go = PrefabUtility.InstantiatePrefab (Selection.activeObject as GameObject) as GameObject; 

							go.transform.position = new Vector3 (xpos, counter + height, zpos);
							counter += height;
						}
					}
				
				} else if (manygen) {
					for (int c = 0; c < iter; c++) {
						int xr = Random.Range (xpos, XrandomRange);
						int yr = Random.Range (zpos, ZrandomRange);

						int counter = 0;


						if (randomFloors)
							f = Random.Range (minfloor, floors);
						else if (!randomFloors)
							f = floors;
						for (int x = 0; x < f; x++) {

							GameObject go = PrefabUtility.InstantiatePrefab (Selection.activeObject as GameObject) as GameObject; 

							go.transform.position = new Vector3 (xr, counter + height, yr);
							counter += height;
						}

					
					}
				}
			}

			else if(pointMode) {


			

					foreach (Transform child in transformPos.transform) {

					if (!randomFloors) {

						GameObject go = PrefabUtility.InstantiatePrefab (primitives [Random.Range (0, primitives.Length)] as GameObject) as GameObject; 

						go.transform.position = child.transform.position;

						if(rot) {
							go.transform.rotation = Quaternion.Euler (0, Random.Range (10, 90), 0);
						}

					}

					else if(randomFloors) {

					
							int counter = 0;
						int ff = Random.Range (0, primitives.Length);
							for (int x = 0; x < Random.Range(minfloor,floors); x++) {

							GameObject go = PrefabUtility.InstantiatePrefab (primitives [ff] as GameObject) as GameObject; 

							go.transform.position = new Vector3 (child.transform.position.x, counter + height, child.transform.position.z);
								counter += height;
							}

					}

				
					}

			}
			
		}

	}





}
