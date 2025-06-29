using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dictionary
{
	public partial class index : Form
	{
		private static index instance;      // フォームのインスタンスを追跡する静的変数
		Dictionary<Button, Panel> _dic = new Dictionary<Button, Panel>();

		public index()
		{
			InitializeComponent();

			// ボタンとパネルの組み合わせを登録
			_dic.Add(this.button1, this.panel1);
			_dic.Add(this.button2, this.panel2);
			_dic.Add(this.button3, this.panel3);

			// ボタンのクリックイベント登録
			AddEvent();

			// 画面初期表示時はボタン1を選択
			SelectPanel(this.topbutton);
		}

		private void index_Load(object sender, EventArgs e)
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
		}

		/// <summary>
		/// イベントハンドラ紐づけ
		/// </summary>
		private void AddEvent()
		{
			foreach (KeyValuePair<Button, Panel> pair in _dic) {
				Button button = pair.Key;
				button.Click += new System.EventHandler(this.Button_Click);
			}
		}

		/// <summary>
		/// ボタンクリックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			SelectPanel(button);
		}

		/// <summary>
		/// 表示を切り替え
		/// </summary>
		/// <param name="selectedButton">選択したボタン</param>
		private void SelectPanel(Button selectedButton)
		{
			foreach (KeyValuePair<Button, Panel> pair in _dic) {
				Button button = pair.Key;
				Panel panel = pair.Value;

				if (button.Equals(selectedButton)) {
					// 選択時
					button.BackColor = Color.DarkBlue;
					button.ForeColor = Color.White;
					panel.Visible = true;
				}
				else {
					// 選択されていない時
					button.BackColor = Color.Gray;
					button.ForeColor = Color.Black;
					panel.Visible = false;
				}
			}
		}


		// 画面を閉じる際の処理
		private void indexformClosingOperations()
		{
			// インスタンスをnullに設定して排他制御を解除
			instance = null;
			this.Close();
		}

		// 画面が削除されたら
		private void index_FormClosed(object sender, FormClosedEventArgs e)
		{
			indexformClosingOperations();
		}

		// トップページに戻る
		private void topbuck_Clicked(object sender, EventArgs e)
		{
			indexformClosingOperations();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
