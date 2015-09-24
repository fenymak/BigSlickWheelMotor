using System;
using System.Collections.Generic;
using spaar.ModLoader;
using TheGuysYouDespise;
using UnityEngine;

namespace Blocks
{
    public class YourBlock : BlockMod
    {
        public override string Name { get { return "bigWheelMotor"; } }
        public override string DisplayName { get { return "Big Slick Wheel Motor"; } }
        public override string Author { get { return "fenymak"; } }
        public override Version Version { get { return new Version("1.0"); } }

        /// <Block-loading-info>
        /// Place .obj file in Mods/Blocks/Obj
        /// Place texture in Mods/Blocks/Textures
        /// Place any additional resources in Mods/Blocks/Resources
        /// </Block-loading-info>

        protected Block block = new Block()
            .ID(270)
            .TextureFile("bigwheelMotor.png")
            .BlockName("Big Slick Wheel Motor")
            .Obj(new List<Obj> { new Obj("bigWheelMotor.obj", new VisualOffset(Vector3.one, Vector3.zero, Vector3.zero)) })
            .IconOffset(new Icon(0.7f, new Vector3(0f, 0f, 1f), new Vector3(-90f, -45f, 0f)))
            .Scripts(new Type[] { typeof(wheelModS) })
            .Properties(new BlockProperties()/*.KeyBinding("do stuff", "g")*/
                                             .Burnable(3f)
                                             .CanBeDamaged(50)
                                             .Slider("Speed", 0f, 10f, 1f).Key1("Forward", "8").Key2("backward", "2"))
            .Mass(1f)
            .ShowCollider(false)
            .CompoundCollider(new List<ColliderComposite> {
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 10f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 20f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 30f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 40f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 50f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 60f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 70f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 80f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 90f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 100f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 110f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 120f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 130f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 140f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 150f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 160f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 170f)),
                                new ColliderComposite(new Vector3(2.8f, 0.2f, 0.8f), new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 180f))})
            .AddingPoints(new List<AddingPoint> {
                                                    (AddingPoint)new BasePoint(false, true).Motionable(true, false, false),
                                                    new AddingPoint(new Vector3(0f, 0f, 0.5f), new Vector3(-90f, 0f, 0f), true),})

            .NeededResources(new List<NeededResource>()); //Add resources through replacing this line with like below, add more resources with separating with commas:
        //.NeededResources(new List<NeededResource> {new NeededResource(ResourceType.Audio, "mySound.ogg")});

        public override void OnLoad()
        {
            LoadFancyBlock(block);
        }

        public class wheelModS : BlockScript
        {
            public Vector3 Torque;
            public Collider coll;
            public float diffx;
            public float diffy;
            public float diffz;
            public float sv;

            protected override void OnSimulateStart()
            {
                Collider[] allColliders = this.GetComponentsInChildren<Collider>();
                foreach (Collider col in allColliders)
                {
                    col.material.bounciness = 0.2f;
                    col.material.dynamicFriction = 2;
                    col.material.staticFriction = 5;
                    col.material.bounceCombine = PhysicMaterialCombine.Maximum;
                    col.material.frictionCombine = PhysicMaterialCombine.Minimum;
                }
                sv = 30 * this.GetComponent<MyBlockInfo>().sliderValue;
            }



            protected override void OnSimulateUpdate()
            {

            }
            protected override void OnSimulateFixedUpdate()
            {
                if (AddPiece.isSimulating)
                {

                    diffx = Vector3.Angle(transform.forward, Vector3.forward);
                    diffz = Vector3.Angle(transform.up, Vector3.up);
                    diffy = Vector3.Angle(transform.right, Vector3.right);
                    Torque = new Vector3(0, 0, diffz / 180 * sv);
                    if (Input.GetKey(this.GetComponent<MyBlockInfo>().key1)) { rigidbody.AddTorque(transform.TransformDirection(Torque) * 1); }
                    if (Input.GetKey(this.GetComponent<MyBlockInfo>().key2)) { rigidbody.AddTorque(transform.TransformDirection(Torque) * -1); }
                    //Physics stuff
                }
            }

            protected override void OnSimulateExit()
            {
            }
        }
    }
}
