using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace dictionary
{
	class settingIniFile
	{
		public string path;

		// 必要かなと思ったけど使用せんかも
		///// <summary>
		///// settinginiファイルの値を保持
		///// </summary>
		//public class Setting
		//{
		//	public string excelName { get; set; }
		//	public int topX { get; set; }
		//	public int topY { get; set; }
		//	public int topWidth { get; set; }
		//	public int topHeight { get; set; }
		//	public int contentsX { get; set; }
		//	public int contentsY { get; set; }
		//	public int contentsWidth { get; set; }
		//	public int contentsHeight { get; set; }
		//	public int IndexX { get; set; }
		//	public int IndexY { get; set; }
		//	public int IndexWidth { get; set; }
		//	public int IndexHeight { get; set; }
		//	public string iconName { get; set; }
		//}

		/**
		 * @brief		INIファイル読込み、セクションとキー・値のペアを辞書形式で返却
		 * @param		path		: INIファイルのパス
		 * @return		セクションごとのキーと値のペアを格納した辞書
		 * @date	2024.10.02	Created		: Nagasawa
		 * @date	2024.10.02	Modified	: Nagasawa
		 */
		public static Dictionary<string, Dictionary<string, string>> ReadIniFile(string path)
		{
			var iniData = new Dictionary<string, Dictionary<string, string>>();
			string currentSection = null;

			foreach (var line in File.ReadAllLines(path)) {
				string trimmedLine = line.Trim();

				// セクションの開始を検出
				if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]")) {
					currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
					iniData[currentSection] = new Dictionary<string, string>();
				}
				// セクション内のキーと値を検出
				else if (!string.IsNullOrEmpty(trimmedLine) && trimmedLine.Contains("=")) {
					// 現在のセクションが null でないことを確認
					if (currentSection == null) {
						throw new InvalidOperationException("セクションが定義されていません。");
					}

					var keyValue = trimmedLine.Split(new[] { '=' }, 2);
					string key = keyValue[0].Trim();
					string value = keyValue[1].Trim();

					// iniDataのセクションとキーを正しく追加
					iniData[currentSection][key] = value;
				}
			}

			return iniData;
		}

		public settingIniFile(string iniPath)
		{
			path = iniPath;
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern int GetPrivateProfileString(string section, string key, string defaultValue,
			System.Text.StringBuilder returnValue, int size, string filePath);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern int WritePrivateProfileString(string section, string key, string value, string filePath);

		public string Read(string section, string key, string defaultValue = "")
		{
			var returnValue = new System.Text.StringBuilder(255);
			GetPrivateProfileString(section, key, defaultValue, returnValue, (int)returnValue.Capacity, path);
			return returnValue.ToString();
		}

		public void Write(string section, string key, string value)
		{
			WritePrivateProfileString(section, key, value, path);
		}
	}
}
