using System;
using DDSHeaders;
using UnityEngine;

namespace NavBallAdjustor
{
    /// <summary>
    /// Help methods from linuxgurugamer.
    /// </summary>
    internal static class LinuxGuruGamer
    {
        public static Texture2D LoadTextureDXT(byte[] ddsBytes, TextureFormat textureFormat)
        {
            if (textureFormat != TextureFormat.DXT1 && textureFormat != TextureFormat.DXT5)
                throw new Exception("Invalid TextureFormat. Only DXT1 and DXT5 formats are supported by this method.");

            byte ddsSizeCheck = ddsBytes[4];
            if (ddsSizeCheck != 124)
                throw new Exception("Invalid DDS DXTn texture. Unable to read");  //this header byte should be 124 for DDS image files

            int height = ddsBytes[13] * 256 + ddsBytes[12];
            int width = ddsBytes[17] * 256 + ddsBytes[16];

            int DDS_HEADER_SIZE = 128;
            byte[] dxtBytes = new byte[ddsBytes.Length - DDS_HEADER_SIZE];
            Buffer.BlockCopy(ddsBytes, DDS_HEADER_SIZE, dxtBytes, 0, ddsBytes.Length - DDS_HEADER_SIZE);

            Texture2D texture = new Texture2D(width, height, textureFormat, false);
            texture.LoadRawTextureData(dxtBytes);
            texture.Apply();

            return (texture);
        }

        static string[] imgSuffixes = new string[] { ".png", ".jpg", ".gif", ".PNG", ".JPG", ".GIF", ".dds", ".DDS" };
        public static bool LoadImageFromFile(ref Texture2D tex, string fileNamePath)
        {

            bool blnReturn = false;
            bool dds = false;
            try
            {
                string path = fileNamePath;
                if (!System.IO.File.Exists(fileNamePath))
                {
                    // Look for the file with an appended suffix.
                    for (int i = 0; i < imgSuffixes.Length; i++)

                        if (System.IO.File.Exists(fileNamePath + imgSuffixes[i]))
                        {
                            path = fileNamePath + imgSuffixes[i];
                            dds = imgSuffixes[i] == ".dds" || imgSuffixes[i] == ".DDS";
                            break;
                        }
                }

                //File Exists check
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        if (dds)
                        {
                            byte[] bytes = System.IO.File.ReadAllBytes(path);

                            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(new System.IO.MemoryStream(bytes));
                            uint num = binaryReader.ReadUInt32();

                            if (num != DDSValues.uintMagic)
                            {
                                UnityEngine.Debug.LogError("DDS: File is not a DDS format file!");
                                return false;
                            }
                            DDSHeader ddSHeader = new DDSHeader(binaryReader);

                            TextureFormat tf = TextureFormat.Alpha8;
                            if (ddSHeader.ddspf.dwFourCC == DDSValues.uintDXT1)
                                tf = TextureFormat.DXT1;
                            if (ddSHeader.ddspf.dwFourCC == DDSValues.uintDXT5)
                                tf = TextureFormat.DXT5;
                            if (tf == TextureFormat.Alpha8)
                                return false;

                            tex = LoadTextureDXT(bytes, tf);
                        }
                        else
                        {
                            tex.LoadImage(System.IO.File.ReadAllBytes(path));
                        }
                        blnReturn = true;
                    }
                    catch (Exception)
                    {
                        ScreenMessages.PostScreenMessage("Failed to load the texture:" + path, 10f);
                    }
                }
                else
                {
                    ScreenMessages.PostScreenMessage("Cannot find texture to load:" + fileNamePath, 10f);
                }
            }
            catch (Exception)
            {
                ScreenMessages.PostScreenMessage("Failed to load (are you missing a file):" + fileNamePath, 10f);
            }
            return blnReturn;
        }

        public static Texture2D GetTexture(string path)
        {
            Texture2D tex = new Texture2D(16, 16, TextureFormat.ARGB32, false);

            if (LoadImageFromFile(ref tex, KSPUtil.ApplicationRootPath + "GameData/" + path))
                return tex;

            return null;
        }
    }
}
