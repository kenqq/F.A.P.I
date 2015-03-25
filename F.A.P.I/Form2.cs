using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace F.A.P.I
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            String asd = @"
　　　　　 ( ⌒ ⌒ ）
　 　　　（､ ,　　　,）
　　　　　　　||　|‘　　　　　
　＿_　　 _____　　 ＿_____
 ,´　_,, '-´￣￣｀-ゝ 、_ イ、
_'r ´　　　　　　　　　　ヽ、ﾝ、 ﾌﾟｸｰ
 ,'＝=─-　　　 　 -─=＝',　i
 i　ｲ　iゝ、ｲ人レ／_ルヽｲ i　|　　＿人人人人人人人人人人人人人人人人人人人人人人＿
ﾚﾘｲi (ﾋ_] 　　 　ﾋ_ﾝ)　| .|、i .|　　＞　　 ひどい！ゆっくりできないひとはキライだよ！！　　＜
!Y!　""U.,‐―（ 　U""　「 !ﾉ i |　 ￣^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^￣
 L.',.　 　　　　　　 　 　 L」 ﾉ|.|
| ||ヽ、　　　　　　　　 ,ｲ| |ｲ/
 レ ル｀ ー---─ ´ルﾚ ﾚ´
　　 　|　.　　　　　　　　　.|　　
　　 　|　 |　　　　　　　|　 | 

　＜おーいゆっくり、一緒にゆっくり出来るところにいこうぜ！

　　　　　＿人人人人人人人人人人人人人人人人人人人人人＿
　　　　　＞　　　　ゆっくりぃー！！ゆっくりしようね！！！！　＜
　　　　　￣^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Y^Ｙ^￣
　　　.　.　　　　　　　　　＿_　　 _____　　 ＿_____
　　　　.　　　　　　　　 ,´　_,, '-´￣￣｀-ゝ 、_ イ、
　　　　.　　　　　　　　.'r ´　　　　　　　　　　ヽ、ﾝ、
　　　　.　　　　　　　..,'＝=iゝ、ｲ人レ／_ル=＝',　i
　　　..　　　　　　　　 i　ｲi　 ⌒ ,＿__, ⌒　 ヽｲ i　|
　　　.　　　　　　　　 ﾚﾘｲ ///　ヽ_ ﾉ　/// | .|、i .||
　　　　　　　　　　　　 !Y!　　　　　　　　　　「 !ﾉ i　|
　　　　　　　　　　　　 .L.',.　 　　　　　　　　L」 ﾉ| .|
　　　　　　　　　　　　　| ||ヽ、　　　　　　 ,ｲ| ||ｲ| /
　　　　　　　　　　　　　 ⊂ル｀ ー--─ ´ルﾚ　ﾚ´
　　　　　　　　　　　　　　　　　 ＼　⊂ ）
　　　　　　　 　　 　　　　　　　　 （⌒）｜　ﾀﾞｯ　　
　　　　　 　　 　　　　　　　　　　　三 `Ｊ
";

            textBox1.Text = asd;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
