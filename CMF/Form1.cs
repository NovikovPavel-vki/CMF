using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibMorseCode;

namespace CMF
{
    public partial class Form1 : Form
    {
        string language;
        string typeC;
        string textData;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                        string stringCode = null, //Хранит шифр
               resultString = ""; //Хранит результат
           // char bufString = '\0'; //строка буфер для посимвольного считывания шифра из файла
            bool isMessageExist = false; //Проверка на то, есть ли сообщение в исходном файле
            int countProbel = 0; //Количество пробелов
            textBox2.Clear();
            string bufString = textBox1.Text;

            MorseCode message = new MorseCode(language);

            int index = 0;
            if (typeC == "c".ToString()) 
            {
                while (index < bufString.Length)
                {
                    while (bufString[index] != ' ') //Считываем символы, пока не получим шифр
                    {
                        

                        if (bufString[index] == '\uffff') //Конец файла сообщения
                            break;

                        //Ведем подсчет пробелов для разделения слов в сообщении
                        if (bufString[index] != ' ')
                            countProbel = 0;
                      

                        //Запись кода
                        if (bufString[index] != ' ')
                            stringCode += bufString[index];

                        index++;
                        if (index >= bufString.Length)
                            break;
                    }

                    //Дешифрация кода
                    if (stringCode != null)
                        resultString += Convert.ToString(message.Decode(stringCode));

                    //Разделение слов между друг другом
                    if (countProbel == 2)
                        resultString += Convert.ToString(message.Decode(Convert.ToString(bufString[index])));

                    stringCode = null;

                    countProbel++;

                    index++;
                }

                

            }
            else
            {
                while (index < bufString.Length)
                {
                    //шифрование символа из сообщения
                    resultString += message.Code(bufString[index]);
                    index++;
                }
                
            }
                


            textBox2.Text = resultString.ToString();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }
            if (checkedListBox1.GetItemCheckState(0) == CheckState.Checked)
                language = "rus";
            if (checkedListBox1.GetItemCheckState(1) == CheckState.Checked)
                language = "en";
            
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox2.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    checkedListBox2.SetItemChecked(i, false);
                checkedListBox2.SetItemChecked(checkedListBox2.SelectedIndex, true);
            }

            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
                typeC = "c";
            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
                typeC = "l";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text;
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
             textData = textBox2.Text;
            Clipboard.SetData(DataFormats.Text, (Object)textData);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textData.ToString();
            Clipboard.SetData(DataFormats.Text, (Object)textData);
        }
    }
    
      
    }




