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
                var urls = new List<string> { url};

                // 検索ワード存在フラグ
                bool isHaveKeyword = false;

                var scr = new Scraping(url);

                // URLリストを辿り、ワードを検索する
                //   終了条件：リンクの数がなくなる
                //            リンクの数が上限（1000）を越える
                do
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
                    scr.OrganizeUrl(urls);

                } while (urls.Count() > 0 && urls.Count() < 1000);

                MessageBox.Show("検索完了しました。", "Info");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
    }
}
