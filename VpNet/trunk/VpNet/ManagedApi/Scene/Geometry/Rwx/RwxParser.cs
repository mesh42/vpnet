using System;
using System.Globalization;
using System.Linq;
using VpNet.Extensions;

namespace VpNet.Geometry.Rwx
{
    public static class RwxParser
    {
        private static char[] split = new[] { ' ', '\t' };

        public static RwxModel Parse(string modelData)
        {
            var rwx = new RwxModel();
            rwx.SourceFile = "unit-test.rwx";
            RwxClump a = null;
            RwxClump proto = null;
            RwxClump model = null;
            string protoName = null;
            bool isProto = false;

            foreach (string line in modelData.ToLower().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Trim().StartsWith("#"))
                    continue;
                var items = line.Split(split, StringSplitOptions.RemoveEmptyEntries);
                switch (items[0])
                {
                    case "triangle":
                        a.Indices.Add(int.Parse(items[1], CultureInfo.InvariantCulture));
                        a.Indices.Add(int.Parse(items[2], CultureInfo.InvariantCulture));
                        a.Indices.Add(int.Parse(items[3], CultureInfo.InvariantCulture));
                        break;
                    case "quad":
                        for (int i = 1; i < items.Length; i++)
                        {
                            a.Indices.Add(int.Parse(items[i], CultureInfo.InvariantCulture));
                        }
                        break;
                    //case "modelbegin":
                    //    break;
                    case "addmaterialmode":
                        if (items[1] == "double")
                            if (model == null)
                                // global double material.
                                rwx.IsDouble = true;
                        break;
                    case "protobegin":
                        proto = new RwxClump();
                        protoName = items[1];
                        proto.Name = items[1];
                        a = proto;
                        break;
                    case "surface":
                        a.Material.Ambient = float.Parse(items[1], CultureInfo.InvariantCulture);
                        a.Material.Diffuse = float.Parse(items[2], CultureInfo.InvariantCulture);
                        a.Material.Specular = float.Parse(items[3], CultureInfo.InvariantCulture);
                        break;
                    case "texturemodes":
                        for (int i = 1; i < items.Length; i++)
                        {
                            switch (items[i])
                            {
                                case "lit":
                                    a.Material.IsTextureLit = true;
                                    break;
                                case "foreshorten":
                                    a.Material.IsTextureForeshorten = true;
                                    break;
                                case "filter":
                                    a.Material.IsTexturFilter = true;
                                    break;
                            }

                        }
                        break;
                    case "translate":
                        a.Transforms.Add(new RwxTranslate() { Translate = new Vector3(items[1], items[2], items[3]) });
                        break;
                    case "transform":
                        a.Transforms.Add(new RwxTransform { Matrix = new Matrix(items.Skip(1).Take(16).ToArray()) });
                        break;
                    case "vertex":
                        a.Vertices.Add(float.Parse(items[1], CultureInfo.InvariantCulture));
                        a.Vertices.Add(float.Parse(items[2], CultureInfo.InvariantCulture));
                        a.Vertices.Add(float.Parse(items[3], CultureInfo.InvariantCulture));
                        a.Uvs.Add(float.Parse(items[5], CultureInfo.InvariantCulture));
                        a.Uvs.Add(float.Parse(items[6], CultureInfo.InvariantCulture));
                        a.Uvs.Add(0);
                        break;
                    case "color":
                        if (items.Length > 3)
                            a.Material.Color = new Vector3(items[1], items[2], items[3]);
                        break;
                    case "opacity":
                        if (items.Length > 1)
                            a.Material.Opacity = (float.Parse(items[1], CultureInfo.InvariantCulture));
                        break;
                    case "texture":
                        if (items.Length > 1)
                        {
                            a.Material.Texture = items[1];
                        }
                        break;
                    case "lightsampling":
                        if (items.Length > 1)
                        {
                            a.Material.IsVertexLighting = (items[1] == "vertex");
                        }
                        break;
                    case "protoend":
                        rwx.Prototypes.Add(protoName, proto);
                        proto = null;
                        break;
                    case "clumpbegin":
                        model = new RwxClump();
                        a = model;
                        break;
                    case "clumpend":
                        rwx.Models.Add(model);
                        model = null;
                        break;
                    case "ambient":
                        if (items.Length > 1)
                            a.Material.Ambient = (float.Parse(items[1], CultureInfo.InvariantCulture));
                        break;
                    case "diffuse":
                        if (items.Length > 1)
                            a.Material.Diffuse = (float.Parse(items[1], CultureInfo.InvariantCulture));
                        break;
                    case "rotate":
                        if (items.Length > 4)
                            a.AddTransform(new Vector3(items[1], items[2], items[3]), float.Parse(items[4], CultureInfo.InvariantCulture));
                        break;
                    case "scale":
                        if (items.Length > 3)
                            a.AddScale(new Vector3(items[1], items[2], items[3]));
                        break;
                    case "protoinstance":
                        if (items.Length > 1)
                        {
                            var c = rwx.Prototypes[items[1]].Copy();
                            c.Transforms.AddRange(a.Transforms.Copy());
                            rwx.Models.Add(c);
                        }
                        break;
                    case "modelend":
                        break;
                    default:
                        break;

                }
            }
            foreach (var m in rwx.Models)
            {
                int ij = 0;
                if (m.Name == string.Empty)
                {
                    m.Name = "unnamed_" + ij;
                    ij++;
                }
            }
            return rwx;
        }
    }
}
