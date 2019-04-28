using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Niu;

namespace 日志类
{
    public partial class MianForm : Form
    {
        public MianForm()
        {
            InitializeComponent();
        }
        bool guanbi = false;
        private void MianForm_Load(object sender, EventArgs e)
        {
            NiuLog.wenjianqianzui = "ceshi";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            guanbi = false;
            Thread th = new Thread(new ThreadStart(kaishi));
            th.Start();
        }

        private void kaishi()
        {
            while (true)
            {
                string neirong = "海外网4月19日电据天空新闻报道，禁止化学武器组织负责人说，联合国安全保障小组在叙利亚杜马镇疑似“化武袭击”地点进行侦查时遭到小规模枪击和爆炸袭击。据报道，联合国安全小组在本周二抵达杜马镇，对该地区的安全形势进行评估，如果其认为当前的杜马安全局势良好，那么真正的调查团将会对叙疑似“化武袭击”展开调查，据悉，禁化武组织已于周末抵达大马士革。";
                neirong += "777";
                //this.richTextBox1.Text += neirong + "\r\n";
                NiuLog.rizhi(neirong);
                NiuLog.rizhi("分类测试", neirong);
                if (guanbi)
                {
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            guanbi = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text ="";
        }

        private void yitiao_Click(object sender, EventArgs e)
        {
            NiuLog.rizhi("如果其认为当前的杜马安全局势良好，那么真正的调查团将会对叙疑似“化武袭击”展开调查，据悉，禁化武组织已于周末抵达大马士革");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NiuLog.rizhi("如果其认为当前的杜马安全局势良好，那么真正的调查团将会对叙疑似“化武袭击”展开调查，据悉，禁化武组织已于周末抵达大马士革666");
        }
    }

}
