using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[ExecuteAlways]
	public class Moving : MonoBehaviour
	{

		[System.Serializable]
		public struct Bound
		{
			public float top;
			public float right;
			public float bottom;
			public float left;
		}

		[Header("Water Settings")]
		public Bound bound;
		public int quality;

		public Material waterMaterial;

		private Vector3[] vertices;

		private Mesh mesh;

		[Header("Physics Settings")]
		public float springconstant = 0.02f;
		public float damping = 0.1f;
		public float spread = 0.1f;
		public float collisionVelocityFactor = 0.04f;

		float[] velocities;
		float[] accelerations;
		float[] leftDeltas;
		float[] rightDeltas;

		private void Start()
		{
			InitializePhysics();
			GenerateMesh();

		// MeshCollider col = gameObject.AddComponent<MeshCollider>();
		MeshCollider col = gameObject.GetComponent<MeshCollider>();

		col.convex = true;
		col.isTrigger = true;


		SetBoxCollider2D();
	}

		private void InitializePhysics()
		{
			velocities = new float[quality];
			accelerations = new float[quality];
			leftDeltas = new float[quality];
			rightDeltas = new float[quality];
		}

		private void GenerateMesh()
		{
			float range = (bound.right - bound.left) / (quality - 1);
			vertices = new Vector3[quality * 2];
			Vector2[] uvs = new Vector2[vertices.Length];
			// generate vertices
			// top vertices
		    for (int i = 0; i < quality; i++)
			{
				vertices[i] = new Vector3(bound.left + (i * range), bound.top, 0);
               

                uvs[i] = new Vector2(i/(quality-1), 1);
			}
			// bottom vertices
			for (int i = 0; i < quality; i++)
			{
				vertices[i + quality] = new Vector2(bound.left + (i * range), bound.bottom);

				uvs[i+quality] = new Vector2(i / (quality - 1), 0);
		    }

			// generate tris. the algorithm is messed up but works. lol.
			int[] template = new int[6];

			// 삼각형 하나
			template[0] = quality;
			template[1] = 0;
			template[2] = quality + 1;

			// 삼각현 하나
			template[3] = 0;
			template[4] = 1;
			template[5] = quality + 1;

			int marker = 0;
			int[] tris = new int[((quality - 1) * 2) * 3];
			for (int i = 0; i < tris.Length; i++)
			{
				tris[i] = template[marker++]++;
				if (marker >= 6) marker = 0;
			}

			// generate mesh
			MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
			if (waterMaterial) meshRenderer.material = waterMaterial;

			MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

			mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.triangles = tris;
			mesh.uv = uvs;
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			// set up mesh
			meshFilter.mesh = mesh;
		}

		private void SetBoxCollider2D()
		{
		GetComponent<MeshCollider>().sharedMesh = mesh;
		}

		private void FixedUpdate()
		{
			// optimization. we don't want to calculate all of this on every update.
		
			// updating physics
			for (int i = 0; i < quality; i++)
			{
				float force = springconstant * (vertices[i].y - bound.top) + velocities[i] * damping;
				accelerations[i] = -force;
				vertices[i].y += velocities[i];
				velocities[i] += accelerations[i];
			}

			for (int i = 0; i < quality; i++)
			{
				if (i > 0)
				{
					leftDeltas[i] = spread * (vertices[i].y - vertices[i - 1].y);
					velocities[i - 1] += leftDeltas[i];
				}
				if (i < quality - 1)
				{
					rightDeltas[i] = spread * (vertices[i].y - vertices[i + 1].y);
					velocities[i + 1] += rightDeltas[i];
				}
			}

			// updating mesh
			mesh.vertices = vertices;

			SetBoxCollider2D();
		}

		private void OnTriggerEnter(Collider col)
		{
			Rigidbody rb = col.GetComponent<Rigidbody>();
			Splash(col, rb.velocity.y * collisionVelocityFactor);
			col.gameObject.SetActive(false);	
		}

		public void Splash(Collider col, float force)
		{
			float radius = col.bounds.max.x - col.bounds.min.x;
            Vector2 center = new Vector2(col.bounds.center.x, bound.top);
			// instantiate splash particle
			//GameObject splashGO = Instantiate(splash, new Vector3(center.x, center2.y-1f, -9), Quaternion.Euler(-90, 0, 0));
			//Destroy(splashGO, 2f);

			// applying physics
			for (int i = 0; i < quality; i++)
			{
				if (PointInsideCircle(vertices[i], center, radius))
				{

					velocities[i] = force;
				}
			}
		}

		bool PointInsideCircle(Vector2 point, Vector2 center, float radius)
		{
			return Vector2.Distance(point, center) < radius;
		}

	}
