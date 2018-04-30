﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamGenPoC
{
    public partial class Form1 : Form
    {
        public int size = 0;
        public int numMem = 0;
        public int numTem = 0;
        public List<string> nameList = new List<string>();
        public int set_count = 0;
        //Because we have an indefinite number of teams, sets are stored in arrays
        //array size will be defined by the used at runtime.
        public ListView[] views;
        public TabPage[] pages;


        public Form1()
        {
            InitializeComponent();
        }

        private void genBtn_Click(object sender, EventArgs e)
        {
            this.setGen();
        }

        private void nameBtn_Click(object sender, EventArgs e)
        {
            this.addName(this.toAddName.Text.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFile();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void generate(int numView)
        {
            views[numView].Clear();
            size = Convert.ToInt32(teamSize.Text.ToString());
            numTem = numMem / size;
            bool[] arr = new bool[numMem];
            int randNum;
            Random rnd = new Random();
            ListViewItem[] teams = new ListViewItem[numTem];
            for (int i = 0; i < numMem; i++)
            {
                arr[i] = true;
            }
            for (int i = 0; i < numTem; i++)
            {
                //ListViewItem team = new ListViewItem("Team", i);
                teams[i] = new ListViewItem("team", i);
                for (int j = 0; j < size; j++)
                {
                    while (true)
                    {
                        randNum = rnd.Next(0, numMem);
                        if (arr[randNum])
                        {
                            teams[i].SubItems.Add(nameList.ElementAt(randNum));
                            arr[randNum] = false;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i <= size; i++)
            {
                views[numView].Columns.Add("Column");
            }
            views[numView].Items.AddRange(teams);
            views[numView].AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void addName(string name)
        {
            //string name = toAddName.Text.ToString();
            nameList.Add(name);
            memberList.Text = memberList.Text + System.Environment.NewLine + name;
            numMem++;
        }

        private void openFile()
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    //nameList.AddRange(File.ReadAllLines(file));
                    foreach(string name in File.ReadAllLines(file))
                    {
                        this.addName(name);
                    }
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void tabInit()
        {
            //TabThing.RemoveAll();
            set_count = Convert.ToInt32(this.setCount.Text.ToString());
            pages = new TabPage[set_count];
            views = new ListView[set_count];
            for(int i = 0; i < set_count; i++)
            {
                views[i] = new ListView();
                pages[i] = new TabPage();
                TabThing.Controls.Add(pages[i]);
                pages[i].Controls.Add(views[i]);
                views[i].View = View.Details;
                pages[i].Text = "Tab";
            }
        }

        private void setGen()
        {
            this.tabInit();
            for(int i = 0; i < set_count; i++)
            {
                this.generate(i);
            }
        }
    }

    public class student
    {
        string name;
        List<string> beenWith;

        public student(string name)
        {
            this.name = name;
            beenWith = new List<string>();
        }

        public int hasBeen(List<string> team)
        {
            int count = 0;
            foreach(string r in beenWith.Intersect(team))
            {
                count++;
            }
            return count;
        }
    }
}
