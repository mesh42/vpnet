﻿using System;
using VpNet.Core.EventData;

namespace VpNet.Core.Structs
{
    public struct WorldAttributes
    {
        /// <summary>
        /// Gets or sets the object path. db key: objectpath
        /// </summary>
        /// <value>
        /// The object path.
        /// </value>
        /// <Author>8/5/2012 6:27 PM cube3</Author>
        public Uri ObjectPath { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to enable terrain. db key: terrain
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable terrain]; otherwise, <c>false</c>.
        /// </value>
        /// <Author>8/5/2012 6:28 PM cube3</Author>
        public bool EnableTerrain { get; set; }
        /// <summary>
        /// Gets or sets the terrain scale. db key: terrainkey
        /// </summary>
        /// <value>
        /// The terrain scale.
        /// </value>
        /// <Author>8/5/2012 6:28 PM cube3</Author>
        public float TerrainScale { get; set; }
        /// <summary>
        /// Gets or sets the ground model. db key: gound
        /// </summary>
        /// <value>
        /// The ground model.
        /// </value>
        /// <Author>8/5/2012 6:28 PM cube3</Author>
        public string GroundModel { get; set; }
        /// <summary>
        /// Gets or sets the skybox. The skybox is the name of the skybox. The world server will automatically look for jpg images and append the following:
        /// _
        /// </summary>
        /// <value>
        /// The skybox.
        /// </value>
        /// <Author>8/5/2012 6:30 PM cube3</Author>
        public string Skybox { get; set; }
        public bool SkyboxSwapLr { get; set; }
        public Color WorldLightAmbient { get; set; }

//        objectpath, terrain, terrainscale, ground, skybox, skybox_swaplr, worldlight_ambient, worldlight_diffuse, worldlight_specular, worldlight_position, w
//elcome, fov, nearplane, farplane
    }
}
