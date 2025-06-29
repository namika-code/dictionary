using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;


namespace dictionary
{
	struct TabInfo
	{
		public string tabname;
	}

	public partial class contents : Form
	{
		#region ｲﾝｽﾀﾝｽ
		settingIniFile settingIniFile;
		Variable IVariable = new Variable();
		#endregion ｲﾝｽﾀﾝｽ

		#region メンバー変数
		private static contents instance;   // フォーム既存有無
		private int currentTabIndex = 0;    // 現在のタブ
		private TabPage[] tabPages;         // タブ配列
		private TabInfo[] tabInfos;         // タブの情報配列
		private Panel[,] panels;            // パネル配列(タブごと)
		private static int panelCount = 10; // パネル数
		private int[] currentPanelIndex;    // 現在のパネル数(タブごと)
		private int[,] nextPanelIndex;      // 次のパネル数(タブごと)
		private int[,] backPanelIndex;      // 前のパネル数(タブごと)
		private const int zero = 0;         // 定数ゼロ
		#endregion メンバー変数

		public contents()
		{
			InitializeComponent();
			tabInfos = new TabInfo[]
			{
			new TabInfo { tabname = "あいうえお" },
			new TabInfo { tabname = "かきくけこ" },
			new TabInfo { tabname = "さしすせそ" },
			};
			// タブの数取得し、次へ＆戻るの配列初期化
			int tabPageCount = tabInfos.Length;
			nextPanelIndex = new int[tabPageCount, panelCount];
			backPanelIndex = new int[tabPageCount, panelCount];
			// 各タブごと用のパネル配列初期化
			currentPanelIndex = new int[tabPageCount];
			panels = new Panel[tabPageCount, panelCount];
			InitializeTabs(tabPageCount);

			// iniファイル読込み
			IVariable.exePath = Application.ExecutablePath;
			IVariable.directory = Path.GetDirectoryName(IVariable.exePath);
			IVariable.settingIniPath = Path.Combine(IVariable.directory, Variable.settingIniName);
			IVariable.iniData = settingIniFile.ReadIniFile(IVariable.settingIniPath);
			settingIniFile = new settingIniFile(IVariable.settingIniPath);  // 初期化
		}

		private void contents_Load(object sender, EventArgs e)
		{
			// フォームがロードされた時に、既存のインスタンスがあるかどうかをチェック
			if (instance != null) {
				if (instance == this) {
					// 既存のインスタンスがある場合は、そのインスタンスにフォーカスを与えて終了
					instance.Focus();
					this.Close();
				}
				else {
					// 既存のインスタンスが自分以外の場合は、そのインスタンスにフォーカスを与えて自身を閉じる
					instance.Focus();
					instance.Close();
				}
			}
			else {
				// 既存のインスタンスがない場合は、自身をインスタンスとして設定
				instance = this;
			}
			//this.WindowState = FormWindowState.Maximized;   // ウィンドウ最大化 自分でサイズ変更とどっちがいいかなぁ

			// settingIniから位置とサイズ読込み
			int x, y, width, height;

			try {
				// INIファイルから位置とサイズを一括で読み込む
				x = int.Parse(IVariable.iniData[Variable.sectionContents][Variable.keyX]);
				y = int.Parse(IVariable.iniData[Variable.sectionContents][Variable.keyY]);
				width = int.Parse(IVariable.iniData[Variable.sectionContents][Variable.keyWidth]);
				height = int.Parse(IVariable.iniData[Variable.sectionContents][Variable.keyHeight]);
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


		private void InitializeTabs(int tabPageCount)
		{
			// タブページを作成して配列に追加
			tabPages = new TabPage[tabPageCount];
			for (int i = 0; i < tabPageCount; i++) {
				tabPages[i] = new TabPage(tabInfos[i].tabname); // 構造体からタブ名を取得して作成
				tabControl1.TabPages.Add(tabPages[i]);

				InitializeUI(i);    // パネルをFlowLayoutPanelに追加
			}
		}

		// UIの初期化
		private void InitializeUI(int tabnumber)
		{
			// FlowLayoutPanel を作成してタブページに追加
			FlowLayoutPanel panelContainer = new FlowLayoutPanel();
			panelContainer.Dock = DockStyle.Fill;   // panelContainerが親コントロールであるタブページに全表示される
			tabPages[tabnumber].Controls.Add(panelContainer); // タブページに FlowLayoutPanel を追加

			// パネル作成、ボタンをパネルに追加
			for (int i = 0; i < panelCount; i++) {
				panels[tabnumber, i] = new Panel();
				panels[tabnumber, i].Location = new Point(0, 0);
				panels[tabnumber, i].Size = new System.Drawing.Size(1000, 500);
				panels[tabnumber, i].AutoScrollPosition = new Point(0, 0);
				panels[tabnumber, i].VerticalScroll.Value = 100;
				panels[tabnumber, i].HorizontalScroll.Value = 100;
				panels[tabnumber, i].AutoScroll = true;

				if (tabnumber == 0) {
					panels[tabnumber, i].BackColor = System.Drawing.Color.LightBlue;
					if (i == 0) {
						Label label1 = new Label();
						Label label2 = new Label();
						label1.Text = "「次へ」と「戻る」ボタンを押すと\n最大10ページまで表示できます\n(下にページ数記載。全タブ同様)";
						label1.AutoSize = true;
						label1.Location = new Point(10, 100);
						label2.Text = "ここまで表示できてるかチェック\n";
						label2.AutoSize = true;
						label2.Location = new Point(200, 1100);
						panels[tabnumber, i].Controls.Add(label1);
						panels[tabnumber, i].Controls.Add(label2);
					}
				}
				else if (tabnumber == 1) {
					panels[tabnumber, i].BackColor = System.Drawing.Color.LightGreen;
				}
				else {
					panels[tabnumber, i].BackColor = System.Drawing.Color.Orange;
				}
				panelContainer.Controls.Add(panels[tabnumber, i]); // FlowLayoutPanelにパネルを追加

				Label pageNumberLabel = new Label();        // ページ番号を表示するラベルの作成
				pageNumberLabel.Text = (i + 1).ToString();  // ページ番号を表示する
				pageNumberLabel.TextAlign = ContentAlignment.MiddleCenter;  // ラベルのテキストを中央揃えに設定
				pageNumberLabel.Dock = DockStyle.Bottom;    // ラベルをパネルの下部に配置
				panels[tabnumber, i].Controls.Add(pageNumberLabel);    // パネルにラベルを追加

				Button nextbutton = new Button();           // 次へボタン
				nextbutton.Text = "次へ";
				nextbutton.Click += (sender, e) => NextPanel_Click(sender, e);
				nextbutton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				nextbutton.Location = new Point(10, 0);
				panels[tabnumber, i].Controls.Add(nextbutton);
				if (i + 1 >= panelCount) {
					nextPanelIndex[tabnumber, i] = zero;
				}
				else {
					nextPanelIndex[tabnumber, i] = i + 1;
				}

				Button backbutton = new Button();           // 戻るボタン
				backbutton.Text = "戻る";
				backbutton.Click += (sender, e) => BackPanel_Click(sender, e);
				backbutton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				backbutton.Location = new Point(100, 0);
				panels[tabnumber, i].Visible = false;
				panels[tabnumber, i].Controls.Add(backbutton);
				if (i - 1 < zero) {
					backPanelIndex[tabnumber, i] = panelCount - 1;
				}
				else {
					backPanelIndex[tabnumber, i] = i - 1;
				}
				currentPanelIndex[tabnumber] = i;
				if (i == panelCount - 1) {
					currentTabIndex = tabnumber;
					ShowPanel(zero, zero);
					if (tabnumber == 2) {
						currentTabIndex = zero;
					}
				}
			}
		}

		// パネル次へ切替イベントハンドラ
		private void NextPanel_Click(object sender, EventArgs e)
		{
			int nextIndex = nextPanelIndex[currentTabIndex, currentPanelIndex[currentTabIndex]];
			ShowPanel(currentTabIndex, nextIndex);
			currentPanelIndex[currentTabIndex] = nextIndex;
		}

		// パネル戻る切替イベントハンドラ
		private void BackPanel_Click(object sender, EventArgs e)
		{
			int prevIndex = backPanelIndex[currentTabIndex, currentPanelIndex[currentTabIndex]];
			ShowPanel(currentTabIndex, prevIndex);
			currentPanelIndex[currentTabIndex] = prevIndex;
		}

		//タブ切替イベントハンドラ
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentTabIndex = tabControl1.SelectedIndex;
			ShowPanel(currentTabIndex, currentPanelIndex[currentTabIndex]);
		}

		private void ShowPanel(int tabindex, int panelindex)
		{
			if (currentTabIndex <= 2 && currentPanelIndex[currentTabIndex] <= 9) {
				panels[currentTabIndex, currentPanelIndex[currentTabIndex]].Visible = false; // 現在のパネル非表示
				panels[tabindex, panelindex].Visible = true;         // 指定されたインデックスのパネル表示
				currentPanelIndex[currentTabIndex] = panelindex;     // 現在のパネルのインデックスを更新
			}
		}

		// トップページに戻る
		private void topbuck_Click(object sender, EventArgs e)
		{
			contentsformClosingOperations();
			this.Close();
		}

		// フォームが閉じられる際に位置情報を保存する
		private void SaveFormPosition()
		{
			// 現在の位置とサイズをsettingIniに保存
			try {
				settingIniFile.Write(Variable.sectionContents, Variable.keyX, this.Location.X.ToString());
				settingIniFile.Write(Variable.sectionContents, Variable.keyY, this.Location.Y.ToString());
				settingIniFile.Write(Variable.sectionContents, Variable.keyWidth, this.Size.Width.ToString());
				settingIniFile.Write(Variable.sectionContents, Variable.keyHeight, this.Size.Height.ToString());
			}
			catch (Exception ex) {
				MessageBox.Show($"Error writing to INI file: {ex.Message}");
			}
		}

		// 画面が削除されたら
		private void contents_FormClosing(object sender, FormClosingEventArgs e)
		{
			contentsformClosingOperations();
		}

		// 画面を閉じる際の処理
		private void contentsformClosingOperations()
		{
			// 既存のインスタンスが自身である場合のみ処理を行う
			if (instance == this) {
				// インスタンスをnullに設定して、排他制御を解除
				instance = null;
			}
			SaveFormPosition();
		}

		/*
		public void DisplayExcelInApp()
		{
			Excel.Application excelApp = new Excel.Application();   // Excelアプリケーションのインスタンスを作成
			Excel.Workbook workbook = excelApp.Workbooks.Open(excelPath);   // ワークブックを開く
			Excel.Worksheet worksheet = workbook.Sheets[1];		// 最初のシートを取得
			Excel.Range range = worksheet.UsedRange;    // セル範囲を取得

			// Excelの各セルをDataGridViewに反映させる
			for (int row = 1; row <= range.Rows.Count; row++) {
				DataGridViewRow newRow = new DataGridViewRow();
				newRow.CreateCells(dataGridView);

				for (int col = 1; col <= range.Columns.Count; col++) {
					newRow.Cells[col - 1].Value = (range.Cells[row, col] as Excel.Range).Value?.ToString();
				}

				dataGridView.Rows.Add(newRow);
			}

			// クローズ
			workbook.Close();
			excelApp.Quit();
		}
		*/
	}
}
