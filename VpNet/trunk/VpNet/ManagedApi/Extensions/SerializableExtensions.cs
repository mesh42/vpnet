#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2013 CUBE3 (Cit:36)

    VPNET is free software: you can redistribute it and/or modify it under the terms of the 
    GNU Lesser General Public License (LGPL) as published by the Free Software Foundation, either
    version 2.1 of the License, or (at your option) any later version.

    VPNET is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even
    the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the LGPL License
    for more details.

    You should have received a copy of the GNU Lesser General Public License (LGPL) along with VPNET.
    If not, see <http://www.gnu.org/licenses/>. 
*/
#endregion

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace VpNet.Extensions
{
    /// <summary>
    /// Serialization Extensions for serializable objects.
    /// </summary>
    public static class SerializableExtensions
    {
        /// <summary>
        /// Create a templated (isolated) copy without references to the original object.
        /// TODO: Update to .NET 4.5 Memory Mapped Files, research potential pitfalls on .NET MONO implementation for this.
        /// </summary>
        /// <returns>A copy of the object</returns>
        public static T Copy<T>(this T o)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("The type must be serializable.", typeof(T).ToString());

            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, o);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Saves / serializes the items to a specified file.
        /// TODO: Update to .NET 4.5 Memory Mapped Files
        /// </summary>
        public static void Serialize<T>(this T o, FileInfo file)
        {
            var x = new XmlSerializer(typeof(T));
            FileStream f = file.OpenWrite();
            x.Serialize(f, o);
            f.Close();
        }

        public static T Deserialize<T>(FileInfo file)
            where T : new()
        {
            var x = new XmlSerializer(typeof(T));
            using (FileStream f = file.OpenRead())
            {
                return (T)x.Deserialize(f);
            }
        }

        /// <summary>
        /// Saves / serializes the items to a specified file.
        /// TODO: Update to .NET 4.5 Memory Mapped Files
        /// </summary>
        public static void Serialize<T>(this T o, string path)
        {
            Serialize(o, false, true).SaveTextFile(path);
        }

        public static T Deserialize<T>(string path)
            where T : new()
        {
            return Deserialize<T>(new FileInfo(path));
        }

        /// <summary>
        /// Copies public properties from another object into your own object. 
        /// Both objects must derive from the same abstact class.
        /// 
        /// Note: Only the extended properties defined in the declaring type T1 are overwritten.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="otherObject">The other object.</param>
        /// <returns></returns>
        internal static T1 CopyFrom<T1, T2>(this T1 obj, T2 otherObject)
            where T1 : class
            where T2 : class
        {
            var srcFields = otherObject.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            var destFields = obj.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in srcFields)
            {
                var dest = destFields.FirstOrDefault(x => x.Name == property.Name);
                if (dest != null && dest.DeclaringType == typeof(T1) /* TODO: only copy the properties from the declaring type with proper presedence */ && dest.CanWrite)
                    dest.SetValue(obj, property.GetValue(otherObject, null), null);
            }

            return obj;
        }

        internal static T1 CopyFromT<T1, T2>(T1 obj, T2 otherObject)
            where T1 : class, new()
            where T2 : class
        {
            if (obj==null)
                obj = new T1();
            var srcFields = otherObject.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            var destFields = obj.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in srcFields)
            {
                var dest = destFields.FirstOrDefault(x => x.Name == property.Name);
                if (dest != null && dest.CanWrite)
                    dest.SetValue(obj, property.GetValue(otherObject, null), null);
            }

            return obj;
        }

        public static string Serialize<T>(this T o, bool ommitXmlDeclaration = false, bool indentation = true)
        {
            var xsn = new XmlSerializerNamespaces();
            var xws = new XmlWriterSettings {Indent = indentation};
            xws.OmitXmlDeclaration = ommitXmlDeclaration;
            var xmlStr = new StringBuilder();
            var x = new XmlSerializer(o.GetType()/*, defaultNamespace*/);
            using (var writer = XmlTextWriter.Create(xmlStr, xws))
            {
                x.Serialize(writer, o, xsn);
                return xmlStr.ToString();
            }
        }

    }
}
