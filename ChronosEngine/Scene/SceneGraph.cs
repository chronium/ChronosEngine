using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletSharp;
using ChronosEngine.Models3D;
using OpenTK;

namespace ChronosEngine.Scene {
	public class SceneGraph {
		public List<Model> Models { get; set; } = new List<Model>();
		public World World { get; set; }
		public PairCachingGhostObject CharacterGhostObject { get; set; }
		public KinematicCharacterController CharacterController { get; set; }

		public void CreateWorld(Vector3 gravity) {
			World = new World();

            World.world.Gravity = gravity;
			World.world.DispatchInfo.AllowedCcdPenetration = 0.0001f;
			World.broadphase.OverlappingPairCache.SetInternalGhostPairCallback(new GhostPairCallback());
		}

		public RigidBody CreateBoxBody(Vector3 halfSize, Matrix4 motionState, float mass = 0, Vector3? localInertia = null) {
			CollisionShape boxShape = new BoxShape(halfSize);
			DefaultMotionState boxMotionState = new DefaultMotionState(motionState);
			RigidBodyConstructionInfo boxCI = new RigidBodyConstructionInfo(mass, boxMotionState, boxShape, localInertia ?? Vector3.Zero);
			RigidBody body = new RigidBody(boxCI);
			return body;
		}

		public RigidBody BodyFromShape(CollisionShape shape, Matrix4 motionState, float mass = 0, Vector3? localInertia = null) {
			DefaultMotionState defMotionState = new DefaultMotionState(motionState);
			RigidBodyConstructionInfo boxCI = new RigidBodyConstructionInfo(mass, defMotionState, shape, localInertia ?? Vector3.Zero);
			RigidBody body = new RigidBody(boxCI);
			return body;
		}

		public void AddModel(Model model) {
			Models.Add(model);
			World.world.AddRigidBody(model.RigidBody);
		}

		public void AddCharacter(Matrix4 startMatrix, float radius, float height) {
			CharacterGhostObject = new PairCachingGhostObject();
			CharacterGhostObject.WorldTransform = startMatrix;
			ConvexShape shape = new CapsuleShape(radius, height);
			CharacterGhostObject.CollisionShape = shape;
			CharacterGhostObject.CollisionFlags = CollisionFlags.CharacterObject;
			CharacterController = new KinematicCharacterController(CharacterGhostObject, shape, .35f);
			World.world.AddCollisionObject(CharacterGhostObject, CollisionFilterGroups.CharacterFilter, CollisionFilterGroups.StaticFilter | CollisionFilterGroups.DefaultFilter);
			World.world.AddAction(CharacterController);
		}
	}

	public class World {
		public BroadphaseInterface broadphase;
		public DefaultCollisionConfiguration config;
		public CollisionDispatcher dispatcher;
		public SequentialImpulseConstraintSolver solver;
		public DiscreteDynamicsWorld world;

		public World() {
			broadphase = new DbvtBroadphase();
			config = new DefaultCollisionConfiguration();
			dispatcher = new CollisionDispatcher(config);
			solver = new SequentialImpulseConstraintSolver();
			world = new DiscreteDynamicsWorld(dispatcher, broadphase, solver, config);
		}
	}
}
