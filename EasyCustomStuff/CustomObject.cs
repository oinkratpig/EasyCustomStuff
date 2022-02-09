using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace EasyCustomStuff
{
	public class CustomObject
	{
		private const int PIXELS_PER_UNIT = 32;

		public static BepInEx.Logging.ManualLogSource Log;
		protected static Dictionary<string, Texture2D> _textureCache;
		protected static Dictionary<string, Sprite> _spriteCache;

		/* Constructor */
		public CustomObject()
        {
			Log = new("EasyCustomStuff");
			BepInEx.Logging.Logger.Sources.Add(Log);
			_textureCache = new();
			_spriteCache = new();

		} // end constructor

		/* Returns the dictionary containing all loaded base characters */
		public static Dictionary<string, CharacterSO> GetAllLoadedCompanions()
		{
			// Keys: "CompanionName_CH"
			return Traverse.Create(typeof(LoadedAssetsHandler))
				.Field("LoadedCharacters")
				.GetValue<Dictionary<string, CharacterSO>>();

		} // end GetLoadedCompanions

		/* Create and cache a new Texture2D */
		protected void CreateTexture2D(string textureName, string filePath)
		{
			FormatPath(ref filePath);
			string textureKey = FormatKey(textureName);

			// Key already exists
			if (_textureCache.ContainsKey(textureKey))
			{
				Log.LogError($"Attempted to create cached Texture2D \"{textureKey}\" more than once!");
				return;
			}

			// File does not exist
			else if (!File.Exists(filePath))
			{
				Log.LogError($"File \"{filePath}\" does not exist!");
				return;
			}

			// Create Texture2D
			Texture2D texture2D = new(0, 0);
			if (!ImageConversion.LoadImage(texture2D, File.ReadAllBytes(filePath)))
			{
				Log.LogError($"Couldn't convert image \"{filePath}\"!");
				return;
			}

			texture2D.hideFlags = HideFlags.HideAndDontSave;
			texture2D.filterMode = FilterMode.Point;

			UnityEngine.Object.DontDestroyOnLoad(texture2D);
			_textureCache.Add(textureKey, texture2D);

		} // end CreateTexture2D

		/* Create and cache a new Sprite */
		protected void CreateSprite(string spriteName, string filePath)
		{
			CreateTexture2D(spriteName, filePath);
			FormatPath(ref filePath);

			string spriteKey = FormatKey(spriteName);

			// Key already exists
			if (_spriteCache.ContainsKey(spriteKey))
			{
				Log.LogError($"Attempted to create cached Sprite \"{spriteKey}\" more than once!");
				return;
			}

			// File does not exist
			else if (!File.Exists(filePath))
			{
				Log.LogError($"File \"{filePath}\" does not exist!");
				return;
			}

			// Get Texture2D
			if (!_textureCache.TryGetValue(spriteKey, out Texture2D texture2D))
			{
				Log.LogError($"Texture2D generated before Sprite \"{spriteKey}\" does not exist.");
				return;
			}

			// Create and cache sprite
			Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector3.zero, PIXELS_PER_UNIT);
			sprite.name = spriteName;
			_spriteCache.Add(spriteKey, sprite);

		} // end CreateSprite

		/* Sets the given Sprite to a cached Sprite */
		protected void SetSprite(ref Sprite sprite, string spriteName)
		{
			if (_spriteCache.TryGetValue(FormatKey(spriteName), out Sprite spr))
			{
				sprite = spr;
			}
			else Log.LogError($"Sprite key \"{spriteName}\" not in cache!");

		} // end SetSprite

		/* Format a key to also include the class name */
		private string FormatKey(string keyName)
        {
			return $"{GetType().Name}_{keyName}";

		} // end FormatKey

		/* Format a path to also include the plugins folder */
		private void FormatPath(ref string filePath)
        {
			filePath = $"BepInEx/plugins/{filePath}";

        } // end FormatPath

	} // end class CustomObject

} // end namespace EasyCustomStuff