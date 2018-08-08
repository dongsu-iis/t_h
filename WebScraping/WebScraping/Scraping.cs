using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebScraping
{
    class Scraping
    {
        // ベースURL
        private string BaseUrl { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="baseUrl"></param>
        public Scraping(string baseUrl)
        {
            // 内部URLを辿るため、LocalPathを除外する
            // 例）http://localhost:8080/Sample → http://localhost:8080
            var url = new Uri(baseUrl);
            BaseUrl = url.Scheme.ToString() + "://" + url.Authority.ToString();
        }

        /// <summary>
        /// URLに対してリクエストを投げ、HTMLを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IHtmlDocument> GethtmlAsync(string url)
        {

            var doc = default(IHtmlDocument);

            using (var client = new HttpClient())
            using (var stream = await client.GetStreamAsync(new Uri(url)))
            {
                // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                var parser = new HtmlParser();
                doc = await parser.ParseAsync(stream);

            }

            return doc;

        }

        /// <summary>
        /// ページに検索ワードに有無をチェック
        /// </summary>
        /// <param name="html"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public bool CheckExistKeyword(IHtmlDocument html, string keyword)
        {
            return html.All.Where(x => x.InnerHtml.Contains(keyword)).Any();
        }


        /// <summary>
        /// htmlに存在するURLリンクを配列に追加する
        /// </summary>
        /// <returns></returns>
        public void SetUrlList(IHtmlDocument html, List<string> urls)
        {
            foreach (var item in html.QuerySelectorAll("a"))
            {
                // リンクのみ対象
                // ※スタイルシートやイメージのURLの追加を防止する
                if (item.Attributes["href"] != null)
                {
                    // 外部リンクの場合：そのまま形式でURLを配列に追加
                    // 内部リンクの場合：ベースURLと連結した形式で配列に追加
                    if (IsUrl(item.Attributes["href"].Value))
                    {
                        //urls.Add(item.Attributes["href"].Value);
                    }
                    else
                    {
                        urls.Add(BaseUrl + item.Attributes["href"].Value);
                    }
                }
            }
        }

        /// <summary>
        /// 正規表現を用いて、対象文字列がURL形式かどうかを判定する
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsUrl(string input)
        {
            return Regex.IsMatch(input,@"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$");
        }
    }
}
