﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using Tanks.Web.UI;

namespace Tanks.Web.GameObjects
{
    public class Wall : BaseGameObject
    {
        Geom geometry;
        Vector2 size;
        RectangleBrush brush;
        public Wall(PhysicsSimulator simulator)
            : base(simulator)
        {
            this.simulator = simulator;
            this.Size = new Vector2(20, Screen.Height);
            this.Initialize();
        }

        public Vector2 Size
        {
            get { return this.size; }
            set { this.size = value; }
        }



        void Initialize()
        {
            this.Body = BodyFactory.Instance.CreateRectangleBody(this.simulator, this.Size.X, this.Size.Y, 1);
            this.Body.IsStatic = true;

            geometry = GeomFactory.Instance.CreateRectangleGeom(this.simulator, this.Body, this.Size.X, this.Size.Y);
            geometry.RestitutionCoefficient = .1f;
            geometry.FrictionCoefficient = 1f;
            geometry.CollisionGridCellSize = .05f;
            this.brush = new RectangleBrush();
            this.brush.Extender.Body = this.Body;
            this.brush.Size = this.Size;
            this.brush.rectangle.Fill = new SolidColorBrush(Colors.Transparent);
            this.brush.rectangle.Stroke = new SolidColorBrush(Colors.Transparent);
        }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }

    }
}
