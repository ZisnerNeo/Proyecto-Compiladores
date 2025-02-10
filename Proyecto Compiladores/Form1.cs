using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Compiladores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string infixRegex = txtInput.Text;
            try
            {
                string expandedRegex = ExpandCharacterRanges(infixRegex);
                string postfixRegex = ConvertToPostfix(AddExplicitConcatenation(expandedRegex));
                txtOutput.Text = postfixRegex;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private string ExpandCharacterRanges(string regex)
        {
            return Regex.Replace(regex, "\\[([a-zA-Z0-9])-([a-zA-Z0-9])\\]", match =>
            {
                char start = match.Groups[1].Value[0];
                char end = match.Groups[2].Value[0];
                StringBuilder expanded = new StringBuilder();
                for (char c = start; c <= end; c++)
                {
                    expanded.Append(c);
                    if (c < end) expanded.Append("|");
                }
                return "(" + expanded.ToString() + ")";
            });
        }

        private string AddExplicitConcatenation(string regex)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < regex.Length; i++)
            {
                char c = regex[i];
                output.Append(c);

                if (i < regex.Length - 1)
                {
                    char next = regex[i + 1];

                    if ((char.IsLetterOrDigit(c) || c == ')' || c == '*' || c == '?' || c == '+') &&
                        (char.IsLetterOrDigit(next) || next == '(' || next == '['))
                    {
                        output.Append("&");
                    }
                }
            }
            return output.ToString();
        }

        private string ConvertToPostfix(string infix)
        {
            Dictionary<char, int> precedence = new Dictionary<char, int>
            {
                {'|', 1},
                {'&', 2}, // Concatenación explícita
                {'*', 3},
                {'?', 3},
                {'+', 3}
            };

            Stack<char> operators = new Stack<char>();
            StringBuilder output = new StringBuilder();
            Stack<string> operandStack = new Stack<string>();

            for (int i = 0; i < infix.Length; i++)
            {
                char c = infix[i];

                if (char.IsLetterOrDigit(c))
                {
                    output.Append(c);
                }
                else if (c == '(')
                {
                    operators.Push(c);
                }
                else if (c == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        output.Append(operators.Pop());
                    }
                    operators.Pop();
                }
                else if (precedence.ContainsKey(c))
                {
                    while (operators.Count > 0 && precedence.ContainsKey(operators.Peek()) && precedence[operators.Peek()] >= precedence[c])
                    {
                        output.Append(operators.Pop());
                    }
                    operators.Push(c);
                }
            }

            while (operators.Count > 0)
            {
                output.Append(operators.Pop());
            }

            return output.ToString();
        }
    }
}

