using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading; // Mutexのために必要

namespace dictionary
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
			bool createdNew;
			using (Mutex mutex = new Mutex(true, "dictionary_Mutex_v1", out createdNew)) {
				if (!createdNew) {
					// アプリケーションがすでに起動されている
					MessageBox.Show("このアプリケーションは既に起動しています。", "Error : 100", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// 通常のアプリケーションの実行処理
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new top());
			}
		}
    }
}
