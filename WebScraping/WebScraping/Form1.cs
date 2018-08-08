using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WebScraping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                // 結果格納テキストボックスを初期化
                textBoxResult.Text = "";

                // 入力したURL取得
                var url = textBoxUrl.Text;
                // 検索キーワードを取得
                var keyword = textBoxKeyword.Text;

                // URLリスト
                var urls = new List<string> { url };

                // 検索ワード存在フラグ
                bool isHaveKeyword = false;

                // URLリストを辿り、ワードを検索する
                var scr = new Scraping(url);
                while (urls.Count() > 0)
                {
                    var urlPath = urls[0];
                    urls.RemoveAt(0);

                    // スクレイピング処理
                    var html = await scr.GethtmlAsync(urlPath);
                    isHaveKeyword = scr.CheckExistKeyword(html, keyword);

                    // キーワードが見つかった場合、最短URL経路表示して処理終了
                    if (isHaveKeyword)
                    {
                        textBoxResult.Text = urlPath;
                        break;
                    }

                    // URLリスト追加
                    scr.SetUrlList(html, urls);

                    //リンクの数が多すぎる場合、処理中止。
                    //TODO 上限個数を指定できるようにする
                    if (urls.Count() > 100)
                    {
                        break;
                    }
                }

                if (!isHaveKeyword)
                {
                    textBoxResult.Text = "見つかりません";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "エラー");
            }
        }
    }
}
