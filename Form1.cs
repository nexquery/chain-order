using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chain_Order
{
    public partial class Form1 : Form
    {
        int sayac = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Panel ile forumu taşıma
            FormMover.Init(this, panel1);

            // Sayaç ayarlarını oluştur veya yükle
            if (!File.Exists(Application.StartupPath + "\\sayac.txt"))
            {
                File.WriteAllText("sayac.txt", sayac.ToString());
            }
            else
            {
                sayac = Convert.ToInt32(File.ReadAllText("sayac.txt"));
            }

            // Textboxa aktar
            textBox2.Text = sayac.ToString();

            // Callback verilerini yükle
            //listView1.HeaderStyle = ColumnHeaderStyle.None;
            Callbacks.Callbacks_Init();
            foreach(var list in Callbacks.calldata)
            {               
                ListViewItem __list = new ListViewItem();
                __list.Text = list.CallbackName;
                __list.ForeColor = ColorTranslator.FromHtml("#a29bfe");
                listView1.Items.Add(__list);
                
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            sayac++;
            SayacGuncelle();
        }

        private void SayacGuncelle()
        {
            File.WriteAllText("sayac.txt", sayac.ToString());
            textBox2.Text = sayac.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sayac--;
            
            if(sayac < 0 )
            {
                sayac = 0;
            }

            SayacGuncelle();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sayac = Convert.ToInt32(textBox2.Text);
            SayacGuncelle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "pre_hooks.pwn";
                saveFileDialog.Filter = "Tüm Dosyalar (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string prefix = textBox1.Text;
                    string dosyaYolu = saveFileDialog.FileName;
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Append, FileAccess.Write, FileShare.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, new UTF8Encoding()))
                        {
                            sw.WriteLine("#if !defined CHAIN_ORDER");
                            sw.WriteLine("    #define CHAIN_ORDER() 0");
                            sw.WriteLine("#endif");
                            sw.WriteLine("");
                            sw.WriteLine("#define CHAIN_HOOK(%0) forward @CO_%0();public @CO_%0(){return CHAIN_ORDER()+1;}");
                            sw.WriteLine("#define CHAIN_NEXT(%0) @CO_%0");
                            sw.WriteLine("");
                            sw.WriteLine("#define CHAIN_FORWARD:%0_%2(%1)=%3; \\");
                            sw.WriteLine("\tforward %0_%2(%1); \\");
                            sw.WriteLine("\tpublic %0_%2(%1) <_ALS : _ALS_x0, _ALS : _ALS_x1> { return (%3); } \\");
                            sw.WriteLine("\tpublic %0_%2(%1) <> { return (%3); }");
                            sw.WriteLine("");
                            sw.WriteLine("#define CHAIN_PUBLIC:%0(%1) %0(%1) <_ALS : _ALS_go>");
                            sw.WriteLine("");
                            sw.WriteLine($"CHAIN_HOOK({prefix}_{sayac})");
                            sw.WriteLine("#undef CHAIN_ORDER");
                            sw.WriteLine($"#define CHAIN_ORDER CHAIN_NEXT({prefix}_{sayac})");
                            sw.WriteLine($"static stock _{prefix}_IncludeStates() <_ALS : _ALS_x0, _ALS : _ALS_x1, _ALS : _ALS_x2, _ALS : _ALS_x3> {{}}");
                            sw.WriteLine($"static stock _{prefix}_IncludeStates() <_ALS : _ALS_go> {{}}");
                            sw.WriteLine("");
                            sw.WriteLine("public OnGameModeInit()");
                            sw.WriteLine("{");
                            sw.WriteLine("    state _ALS : _ALS_go;");
                            sw.WriteLine($"    return {prefix}_OnGameModeInit();");
                            sw.WriteLine("}");
                            sw.WriteLine("");
                            sw.WriteLine("#if defined _ALS_OnGameModeInit");
                            sw.WriteLine("    #undef OnGameModeInit");
                            sw.WriteLine("#else");
                            sw.WriteLine("    #define _ALS_OnGameModeInit");
                            sw.WriteLine("#endif");
                            sw.WriteLine($"#define OnGameModeInit(%0) CHAIN_PUBLIC:{prefix}_OnGameModeInit(%0)");
                            sw.WriteLine($"CHAIN_FORWARD:{prefix}_OnGameModeInit() = 1;");
                        }
                    }
                    MessageBox.Show("Pre Hooks dosyası oluşturuldu.");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Prefix
            string prefix = textBox1.Text;

            // Koplayanacak string
            string buffer = "";

            // Header
            buffer += $"CHAIN_HOOK({prefix}_{sayac})\n";
            buffer += $"#undef CHAIN_ORDER\n";
            buffer += $"#define CHAIN_ORDER CHAIN_NEXT({prefix}_{sayac})\n\n";

            foreach (ListViewItem p in listView1.Items)
            {
                if(p.Checked)
                {
                    foreach(var data in Callbacks.calldata)
                    {
                        if(data.CallbackName == p.Text)
                        {
                            buffer += $"public {data.CallbackName}({data.Parameter})\n";
                            buffer += $"{{\n";
                            buffer += $"\treturn {prefix}_{sayac}_{data.CallbackName}({data.Parameter});\n";
                            buffer += $"}}";
                            buffer += $"\n\n";
                        }
                    }
                }
            }

            foreach (ListViewItem p in listView1.Items)
            {
                if (p.Checked)
                {
                    foreach (var data in Callbacks.calldata)
                    {
                        if (data.CallbackName == p.Text)
                        {
                            buffer += "/*******************************************************************************/\n";
                            buffer += $"#if defined _ALS_{data.CallbackName}\n";
                            buffer += $"    #undef {data.CallbackName}\n";
                            buffer += $"#else\n";
                            buffer += $"    #define _ALS_{data.CallbackName}\n";
                            buffer += $"#endif\n";
                            buffer += $"#define {data.CallbackName}(%0) CHAIN_PUBLIC:{prefix}_{sayac}_{data.CallbackName}(%0)\n";
                            buffer += $"CHAIN_FORWARD:{prefix}_{sayac}_{data.CallbackName}({data.Parameter}) = {data.RetValue};\n";
                        }
                    }
                }
            }

            // Kopyala
            Clipboard.SetText(buffer);

            // Bilgi
            MessageBox.Show("Kodlar kopyalandı !");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem p in listView1.Items)
            {
                p.Checked = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem p in listView1.Items)
            {
                p.Checked = false;
            }
        }
    }
}
