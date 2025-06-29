using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dictionary
{
	class Variable
	{
		#region 定数変数
		public string exePath;      // 現在使用している実行ファイルへのパス
		public string directory;    // 現在使用しているディレクトリへのパス
		public string solutionDirectory;    // 実行ファイルの2つ上のパス
		public string resourcesPath;        // 実行中のResourcesへのパス
		public string excelPath;
		public string settingIniPath;
		public string iconPath;
		public string excelName;  // Excel名
		public string iconName;    // アイコン名
		public const string settingIniName = "setting.ini";
		public Dictionary<string, Dictionary<string, string>> iniData = new Dictionary<string, Dictionary<string, string>>();

		// settingIni関連
		public const string sectionExcel = "Excel";
		public const string keyExcelName = "excelName";
		public const string sectionTop = "Top";
		public const string sectionContents = "Contents";
		public const string sectionIndex = "Index";
		public const string keyX = "X";
		public const string keyY = "Y";
		public const string keyWidth = "Width";
		public const string keyHeight = "Height";
		public const int initial_xy = 0;
		public const int initial_widthHight = 600;
		public const string sectionIcon = "Icon";
		public const string keyIconName = "iconName";
		#endregion 定数変数
	}
}
