using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace dictionary
{
	public partial class top : Form
	{
		#region ｲﾝｽﾀﾝｽ
		settingIniFile settingIniFile;
		Variable IVariable = new Variable();
		#endregion ｲﾝｽﾀﾝｽ


		public top()
		{
			InitializeComponent();
			// iniファイル読込み
			IVariable.exePath = Application.ExecutablePath;
			IVariable.directory = Path.GetDirectoryName(IVariable.exePath);
			IVariable.settingIniPath = Path.Combine(IVariable.directory, Variable.settingIniName);
			IVariable.iniData = settingIniFile.ReadIniFile(IVariable.settingIniPath);
			settingIniFile = new settingIniFile(IVariable.settingIniPath);  // 初期化

			// iniデータ関連格納
			IVariable.excelName = IVariable.iniData[Variable.sectionExcel][Variable.keyExcelName];
			IVariable.excelPath = Path.Combine(IVariable.directory, IVariable.excelName);

			IVariable.solutionDirectory = Directory.GetParent(IVariable.directory).Parent.FullName; // 実行ファイルの、2つ上のディレクトリに移動
			IVariable.resourcesPath = Path.Combine(IVariable.solutionDirectory, "Resources");
			//IVariable.iconName = IVariable.iniData[Variable.sectionIcon][Variable.keyIconName];
			//IVariable.iconPath = Path.Combine(IVariable.resourcesPath, IVariable.iconName);

			this.Text = "マニュアルになる予定です"; // タイトルバーの文言
			//this.Icon = new Icon(IVariable.iconPath);         // なんかプロパティ設定だけじゃできなかった
			


			//this.WindowState = FormWindowState.Maximized;   // ウィンドウ最大化 自分でサイズ変更とどっちがいいかなぁ
		}
		private void top_Load(object sender, EventArgs e)
		{
			// settingIniから位置とサイズ読込み
			int x, y, width, height;

			try {
				// INIファイルから位置とサイズを一括で読み込む
				x = int.Parse(IVariable.iniData[Variable.sectionTop][Variable.keyX]);
				y = int.Parse(IVariable.iniData[Variable.sectionTop][Variable.keyY]);
				width = int.Parse(IVariable.iniData[Variable.sectionTop][Variable.keyWidth]);
				height = int.Parse(IVariable.iniData[Variable.sectionTop][Variable.keyHeight]);
			}
			catch (FormatException ex) {
				MessageBox.Show($"エラー: 値の形式が不正です。デフォルト値を使用します。 {ex.Message}");
				x = y = Variable.initial_xy;
				width = height = Variable.initial_widthHight;
			}
			catch (Exception ex) {
				MessageBox.Show($"エラー: {ex.Message}");
				x = y = Variable.initial_xy;
				width = height = Variable.initial_widthHight;   // 初期化
			}

			this.Location = new Point(x, y);
			this.Size = new Size(width, height);
		}
		private void top_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 現在の位置とサイズをsettingIniに保存
			try {
				settingIniFile.Write(Variable.sectionTop, Variable.keyX, this.Location.X.ToString());
				settingIniFile.Write(Variable.sectionTop, Variable.keyY, this.Location.Y.ToString());
				settingIniFile.Write(Variable.sectionTop, Variable.keyWidth, this.Size.Width.ToString());
				settingIniFile.Write(Variable.sectionTop, Variable.keyHeight, this.Size.Height.ToString());
			}
			catch (Exception ex) {
				MessageBox.Show($"Error writing to INI file: {ex.Message}");
			}
		}


		// もくじへ
		private void contents_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			contents form2 = new contents();
			form2.ShowDialog();
		}

		// 索引へ
		private void index_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			index form3 = new index();
			form3.ShowDialog();

		}
	}
}
