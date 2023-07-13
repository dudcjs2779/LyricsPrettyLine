using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LyricsPrettyLine
{
    public partial class LyricsPrettyLine : Form
    {
        List<Lyrics> lyricsList = new List<Lyrics>();
        int indexKey;
        int selectedIndex;

        public LyricsPrettyLine()
        {
            InitializeComponent();
            LyricsListView1.BeginUpdate();

            this.Refresh();
            LyricsListView1.View = System.Windows.Forms.View.Details; ;// 목록 형으로 보이기
            LyricsListView1.GridLines = true; // 그리드 라인을 보여준다.
            LyricsListView1.FullRowSelect = true;  // 선택은 행으로 하기.

            LyricsListView1.AllowDrop = true; // drag and drop 허용

            // drag and drop
            //this.ImgListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImgListView_DragDrop);
            //this.ImgListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImgListView_DragEnter);

            LyricsListView1.Columns.Add("#", 30);
            LyricsListView1.Columns.Add("Name", 150);
            LyricsListView1.Columns.Add("Path", 500);

            LyricsListView1.EndUpdate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog_init();
        }

        private void openFileDialog_init()
        {
            openFileDialog1.Title = "텍스트 선택";
            openFileDialog1.DefaultExt = "*";

            //openFileDialog1.Filter = "동영상 (*.mkv, *.mp4, *.wmv, *.mov) | *.mkv; *.mp4; *.wmv; *.mov;";
            //openFileDialog1.Filter = "실행파일 (*.exe) | *.exe;";
            openFileDialog1.Filter = "텍스트 (*.txt) | *.txt;";
            //openFileDialog1.Filter += "|이미지 (*.jpg, *.gif, *.png) | *.jpg; *.gif; *.png;";
            //openFileDialog1.Filter += "|음악 (*.mp3) | *.mp3;";
            openFileDialog1.Filter += "|모든 파일 (*.*) | *.*";

            openFileDialog1.Multiselect = true;                      //여러파일선택
            openFileDialog1.ReadOnlyChecked = false;                  //읽기전용으로 열것인지체크
            //openFileDialog1.ShowReadOnly = true;                     //읽기전용파일 보이기 
        }

        private void LyricsListView1_DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file or a bitmap, display the copy cursor.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; //포인터
            }
            else if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void LyricsListView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // 중복 아이템 리스트
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length >= 1)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        Lyrics lyrics = new Lyrics(indexKey, Path.GetFileName(files[i]), files[i], File.ReadAllText(files[i]));
                        lyricsList.Add(lyrics);

                        ListViewItem listViewItem = new ListViewItem(new string[] { indexKey.ToString(), Path.GetFileName(files[i]), files[i] }, indexKey);
                        LyricsListView1.Items.Add(listViewItem);

                        indexKey++;
                    }
                    selectedIndex = indexKey - 1;
                }
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            //LyricsListView1.Items.Clear();
            //lyricsList = new List<Lyrics>();


            //openFileDialog1.InitialDirectory = OpenFilePath;         //초기경로
            openFileDialog1.RestoreDirectory = true;                 //현재 경로가 이전 경로로 복원되는지 여부          
            openFileDialog1.FileName = ""; //기본값 파일명

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string[] saLvwItem = new string[3];

                foreach (String filePath in openFileDialog1.FileNames)
                {
                    Lyrics lyrics = new Lyrics(indexKey, Path.GetFileName(filePath), filePath, File.ReadAllText(filePath));
                    lyricsList.Add(lyrics);

                    ListViewItem listViewItem = new ListViewItem(new string[] { indexKey.ToString(), Path.GetFileName(filePath), filePath }, indexKey);
                    LyricsListView1.Items.Add(listViewItem);
                    indexKey++;
                }
                selectedIndex = indexKey - 1;
            }
            return;
        }

        private void OperateBtn_Click(object sender, EventArgs e)
        {
            foreach (var lyrics in lyricsList)
            {
                //int count = ContainsJapanese(lyrics.text);
                //string newText = RemoveNewLines(lyrics.text);

                string newText = lyrics.text;
                StringBuilder stb_newText = new StringBuilder();
                StringBuilder stb_line = new StringBuilder();

                List<string> lineList = new List<string>();



                char[] newLineChars = { '\n', '\r' };

                for (int i = 0; i < newText.Length; i++)
                {
                    if ((newText[i] == '\n' || newText[i] == '\r') && (i + 1 < newText.Length)
                        && (newText[i + 1] == '\n' || newText[i + 1] == '\r')
                        || i == newText.Length - 1)
                    {
                        Console.WriteLine(i + "문장: " + stb_line.ToString());

                        if (stb_line.Length > 0)
                        {
                            lineList.Add(stb_line.ToString());
                            stb_newText.Append(stb_line.ToString());
                            stb_line.Clear();
                        }
                    }
                    else
                    {
                        stb_line.Append(newText[i]);
                    }
                }

                int groupCount = 0;
                string result = "";
                for (int i = 0; i < lineList.Count; i++)
                {
                    if (CountOfJp(lineList[i]) > 0)
                    {
                        result += lineList[i];
                        groupCount = 2;
                    }
                    else if (groupCount == 2)
                    {
                        result += lineList[i];
                        groupCount--;
                    }
                    else if (groupCount == 1)
                    {
                        result += lineList[i] + "\n";
                        groupCount--;
                    }
                    else
                    {
                        result += lineList[i];
                    }
                }

                Console.WriteLine(result);
                //newText = stb_newText.ToString();
                newText = result;

                string newPath = Path.GetFileNameWithoutExtension(lyrics.path);
                newPath += "_PrettyLine" + Path.GetExtension(lyrics.path);
                newPath = Path.Combine(Path.GetDirectoryName(lyrics.path), newPath);
                File.WriteAllText(newPath, newText);
            }

        }

        bool IsJapaneseCharacter(char ch)
        {
            // 일본어 유니코드 범위를 확인
            return (ch >= '\u3040' && ch <= '\u309F') ||  // 히라가나
                   (ch >= '\u30A0' && ch <= '\u30FF') ||  // 가타카나
                   (ch >= '\u4E00' && ch <= '\u9FFF');    // CJK 통합 한자
        }

        int CountOfJp(string text)
        {
            int count = 0;
            foreach (char ch in text)
            {
                if (IsJapaneseCharacter(ch))
                {
                    count++;
                }
            }

            return count;
        }

        bool IsKoreanCharacter(char ch)
        {
            // 한글 유니코드 범위를 확인
            return ch >= '\uAC00' && ch <= '\uD7AF';
        }

        bool IsEnglishCharacter(char ch)
        {
            // 영어 알파벳 범위를 확인
            return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
        }

        int CountOfKorEn(string text)
        {
            int count = 0;
            foreach (char ch in text)
            {
                if (IsKoreanCharacter(ch) || IsEnglishCharacter(ch))
                {
                    count++;
                }
            }

            return count;
        }

        bool IsNewLineCharacter(char ch)
        {
            return ch == '\n' || ch == '\r';
        }

        int ContainsJapanese(string text)
        {
            string pattern = @"\p{IsHiragana}|\p{IsKatakana}|\p{IsCJKUnifiedIdeographs}";

            // 정규식 패턴과 일치하는 부분 찾기
            MatchCollection matches = Regex.Matches(text, pattern);

            // 매치된 부분이 있는지 여부 반환
            return matches.Count;
        }

        bool ContainsKoreanOrEnglish(string text)
        {
            string koreanPattern = @"\p{IsHangul}+";
            string englishPattern = @"[a-zA-Z]+";

            // 한국어 또는 영어 검사
            bool containsKorean = Regex.IsMatch(text, koreanPattern);
            bool containsEnglish = Regex.IsMatch(text, englishPattern);

            // 한국어 또는 영어가 포함되어 있는지 여부 반환
            return containsKorean || containsEnglish;
        }


        void PrettyLine(string text)
        {
            string pattern = @"\p{IsHiragana}|\p{IsKatakana}|\p{IsCJKUnifiedIdeographs}";

            // 정규식 패턴과 일치하는 부분 찾기
            Match match = Regex.Match(text, pattern);
        }

        string RemoveNewLines(string text)
        {
            return text.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "aaaaaaaaaa\n\nbbbbbbbbbbbbb\n\nccccccccccccc";
            string[] lines = text.Split('\n');

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private void LyricsListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                selectedIndex = int.Parse(e.Item.SubItems[0].Text);
            }
            else
            {
                selectedIndex = LyricsListView1.Items.Count;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (LyricsListView1.Items.Count == 0) return;

            ListViewItem foundItem = null;
            foreach (ListViewItem item in LyricsListView1.Items)
            {
                if (item.SubItems[0].Text == selectedIndex.ToString())
                {
                    foundItem = item;
                    break;
                }
            }
            LyricsListView1.Items.Remove(foundItem);

            Lyrics foundLyrics = new Lyrics();
            foundLyrics = lyricsList.Find(x => x.index == selectedIndex);
            lyricsList.Remove(foundLyrics);

            selectedIndex--;

            if(LyricsListView1.Items.Count == 0)
            {
                indexKey = 0;
            }

            textBox1.Text = selectedIndex.ToString();
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            if (LyricsListView1.Items.Count == 0) return;

            LyricsListView1.Items.Clear();
            lyricsList.Clear();
            indexKey = 0;

            selectedIndex = 0;
        }

        private class Lyrics
        {
            public Lyrics()
            {
            }

            public Lyrics(int index, string name, string path, string text)
            {
                this.index = index;
                this.name = name;
                this.path = path;
                this.text = text;
            }

            public int index;
            public string name;
            public string path;
            public string text;
        }

        
    }
}
